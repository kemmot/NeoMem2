// <copyright file="GcMonitor.cs" company="No Company">
// No company. All rights reserved.
// </copyright>

namespace NeoMem2.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading;

    using NeoMem2.Utils.IO;

    using NLog;

    /// <summary>
    /// A class that manages a thread for monitoring the garbage collector.
    /// </summary>
    public class GcMonitor : DisposableBase
    {
        /// <summary>
        /// The logger to use in this class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The counter names to generate custom properties for.
        /// </summary>
        private static readonly string[] CounterNames = { "# Bytes in all Heaps", "# GC Handles" };

        /// <summary>
        /// The generation specific counter names to generate custom properties for.
        /// </summary>
        private static readonly string[] GenerationCounterNames = { "# Gen {0} Collections", "Gen {0} heap size" };

        /// <summary>
        /// The counters used for generating details.
        /// </summary>
        private readonly List<PerformanceCounter> counters = new List<PerformanceCounter>();

        /// <summary>
        /// A synchronization object.
        /// </summary>
        private readonly object countersSyncRoot = new object();

        /// <summary>
        /// The generation specific counters used for generating details.
        /// </summary>
        private readonly Dictionary<int, List<PerformanceCounter>> generationCounters = new Dictionary<int, List<PerformanceCounter>>();
        
        /// <summary>
        /// The thread used for monitoring the garbage collector.
        /// </summary>
        private readonly Thread garbageCollectorMonitorThread;

        /// <summary>
        /// The generation specific details.
        /// </summary>
        private readonly Dictionary<int, GcMonitorGenerationDetails> generationDetails = new Dictionary<int, GcMonitorGenerationDetails>();

        /// <summary>
        /// An event used for sending a signal to the monitoring thread to stop.
        /// </summary>
        private readonly ManualResetEventSlim stoppingEvent = new ManualResetEventSlim(false);

        /// <summary>
        /// The backing field for the <see cref="EnableMemoryMonitoring"/> property.
        /// </summary>
        private bool enableMemoryMonitoring;

        /// <summary>
        /// Initializes a new instance of the <see cref="GcMonitor" /> class.
        /// </summary>
        public GcMonitor()
        {
            this.PollFrequency = TimeSpan.FromSeconds(10);

            this.garbageCollectorMonitorThread = new Thread(this.ThreadGcMonitor)
            {
                Name = "GC Monitor",
                IsBackground = true
            };
        }

        /// <summary>
        /// Fired when the garbage collector status changes.
        /// </summary>
        public event EventHandler<GcMonitorEventArgs> GcStatusChanged;
        
        /// <summary>
        /// Gets or sets a value indicating whether memory monitoring is enabled.
        /// </summary>
        public bool EnableMemoryMonitoring
        {
            get
            {
                return this.enableMemoryMonitoring;
            }

            set
            {
                if (value != this.enableMemoryMonitoring)
                {
                    this.enableMemoryMonitoring = value;
                    if (this.enableMemoryMonitoring)
                    {
                        this.CreateCounters();
                    }
                    else
                    {
                        this.RemoveCounters();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the frequency at which the monitoring thread should poll for garbage collector details.
        /// </summary>
        public TimeSpan PollFrequency { get; set; }
        
        /// <summary>
        /// Starts monitoring the garbage collector.
        /// </summary>
        public void Start()
        {
            this.garbageCollectorMonitorThread.Start();
        }

        /// <summary>
        /// Raises the <see cref="GcStatusChanged"/> event.
        /// </summary>
        /// <param name="e">The event arguments to use.</param>
        protected virtual void OnGcStatusChanged(GcMonitorEventArgs e)
        {
            if (this.GcStatusChanged != null)
            {
                this.GcStatusChanged(this, e);
            }
        }

        /// <summary>
        /// Cleans up the managed resources of the object.
        /// </summary>
        protected override void DisposeManagedResources()
        {
            this.EnableMemoryMonitoring = false;

            this.stoppingEvent.Set();

            if (!this.garbageCollectorMonitorThread.Join(200))
            {
                this.garbageCollectorMonitorThread.Abort();
            }

            this.stoppingEvent.Dispose();
            base.DisposeManagedResources();
        }

        /// <summary>
        /// Creates the performance counters used by this object.
        /// </summary>
        private void CreateCounters()
        {
            lock (this.countersSyncRoot)
            {
                this.RemoveCounters();

                foreach (string counterName in CounterNames)
                {
                    PerformanceCounter counter = new PerformanceCounter(".NET CLR Memory", counterName, "NeoMem2.Gui");
                    counter.NextValue();
                    this.counters.Add(counter);
                }

                for (int generation = 0; generation < GC.MaxGeneration; generation++)
                {
                    List<PerformanceCounter> localGenerationCounters;
                    if (!this.generationCounters.TryGetValue(generation, out localGenerationCounters))
                    {
                        localGenerationCounters = new List<PerformanceCounter>();
                        this.generationCounters[generation] = localGenerationCounters;
                    }

                    foreach (string counterNameFormat in GenerationCounterNames)
                    {
                        string counterName = string.Format(CultureInfo.InvariantCulture, counterNameFormat, generation);
                        PerformanceCounter counter = new PerformanceCounter(".NET CLR Memory", counterName, "NeoMem2.Gui");
                        counter.NextValue();

                        localGenerationCounters.Add(counter);
                    }
                }
            }
        }

        /// <summary>
        /// Removes and disposes the performance counters used by this object.
        /// </summary>
        private void RemoveCounters()
        {
            lock (this.countersSyncRoot)
            {
                foreach (PerformanceCounter counter in this.counters)
                {
                    counter.Dispose();
                }

                this.counters.Clear();

                foreach (List<PerformanceCounter> localCounters in this.generationCounters.Values)
                {
                    foreach (PerformanceCounter counter in localCounters)
                    {
                        counter.Dispose();
                    }
                }

                this.generationCounters.Clear();
            }
        }

        /// <summary>
        /// The method for the main monitoring thread.
        /// </summary>
        private void ThreadGcMonitor()
        {
            Logger.Info("Thread started");

            bool stop = false;
            do
            {
                try
                {
                    this.GenerateStatusChange();
                }
                catch (ThreadAbortException)
                {
                    Thread.ResetAbort();
                    stop = true;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                if (!stop)
                {
                    stop = this.stoppingEvent.Wait(this.PollFrequency);
                }
            }
            while (!stop);

            Logger.Info("Thread stopped");
        }
        
        /// <summary>
        /// Inspects the garbage collector and generates an event.
        /// </summary>
        private void GenerateStatusChange()
        {
            GcMonitorEventArgs args = new GcMonitorEventArgs
            {
                TotalMemory = DataSize.FromBytes(GC.GetTotalMemory(false)),
                TimeGenerated = DateTime.Now
            };

            lock (this.countersSyncRoot)
            {
                foreach (PerformanceCounter counter in this.counters)
                {
                    args.CustomProperties[counter.CounterName] = counter.NextValue();
                }

                for (int generation = 0; generation <= GC.MaxGeneration; generation++)
                {
                    GcMonitorGenerationDetails localGenerationDetails;
                    if (!this.generationDetails.TryGetValue(generation, out localGenerationDetails))
                    {
                        localGenerationDetails = new GcMonitorGenerationDetails { GenerationId = generation, LastCollectionTime = DateTime.Now };
                        this.generationDetails[generation] = localGenerationDetails;
                    }

                    int newCount = GC.CollectionCount(generation);
                    if (localGenerationDetails.CollectionCount != newCount)
                    {
                        localGenerationDetails.LastCollectionTime = DateTime.Now;
                        localGenerationDetails.CollectionCount = newCount;
                    }

                    List<PerformanceCounter> localGenerationCounters;
                    if (this.generationCounters.TryGetValue(generation, out localGenerationCounters))
                    {
                        foreach (PerformanceCounter generationCounter in localGenerationCounters)
                        {
                            localGenerationDetails.CustomProperties[generationCounter.CounterName] = generationCounter.NextValue();
                        }
                    }

                    args.Generations.Add(generation, localGenerationDetails);
                }
            }

            this.OnGcStatusChanged(args);
        }
    }
}
