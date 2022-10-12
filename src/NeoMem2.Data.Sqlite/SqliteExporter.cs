namespace NeoMem2.Data.Sqlite
{
    using NeoMem2.Core;
    using NeoMem2.Core.Stores;

    using NLog;

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// An implementation of <see cref="IExporter"/> that exports to a sqlite database.
    /// </summary>
    public class SqliteExporter : ExporterBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void Export(NeoMemFile file)
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                Logger.Info($"Export started to: {this.ConnectionString}");

                string sqliteDatabasePath = this.ConnectionString;
                if (File.Exists(sqliteDatabasePath))
                {
#if DEBUG
                    File.Delete(sqliteDatabasePath);
#else
                    throw new InvalidOperationException($"sqlite export file already exists: {sqliteDatabasePath}");
#endif
                }

                string connectionString = string.Format("Data Source={0};", sqliteDatabasePath);

                var store = new SqliteStore(connectionString);

                store.CreateNewStore();
                Logger.Debug($"Created export file: {sqliteDatabasePath}");

                var notes = new List<Note>(file.AllNotes.GetNotes());

                foreach (var note in notes)
                {
                    note.Detach();
                }

                int notesComplete = 0;
                foreach (var note in notes)
                {
                    try
                    {
                        store.Save(note);
                        notesComplete++;
                        Logger.Debug($"Exported note {notesComplete}/{notes.Count}, ID: {note.Id}, title: {note.Name}");
                        OnProgressChanged(notesComplete, notes.Count, note.Name);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to export note {note.Id}", ex);
                    }
                }

                stopwatch.Stop();
                Logger.Info($"Export completed to: {connectionString} [{stopwatch.Elapsed}]");
            }
            catch (Exception ex)
            {
                Logger.Error($"Export failed, {ex}");
                throw;
            }
        }
    }
}
