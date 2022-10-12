using System;

using NeoMem2.Utils.Collections.Generic;

namespace NeoMem2.Gui.Models
{
    public class Navigator<T>
    {
        public event EventHandler CurrentChanged;

        private readonly ObservableStack<T> m_BackwardsNavigationStack = new ObservableStack<T>();
        private readonly ObservableStack<T> m_ForwardsNavigationStack = new ObservableStack<T>();
        private T m_Current;


        public int BackwardsStackCount { get { return m_BackwardsNavigationStack.Count; } }

        public T Current
        {
            get { return m_Current; }
            private set
            {
                m_Current = value;
                OnCurrentChanged(new EventArgs());
            }
        }

        public int ForwardsStackCount { get { return m_ForwardsNavigationStack.Count; } }


        public void SetCurrent(T item)
        {
            if (!ReferenceEquals(Current, null))
            {
                m_BackwardsNavigationStack.Push(Current);
            }

            m_BackwardsNavigationStack.Push(m_ForwardsNavigationStack.PopAll());

            Current = item;
        }

        public void NavigateForwards()
        {
            if (m_ForwardsNavigationStack.Count > 0)
            {
                m_BackwardsNavigationStack.Push(Current);
                Current = m_ForwardsNavigationStack.Pop();
            }
        }

        public void NavigateBackwards()
        {
            if (m_BackwardsNavigationStack.Count > 0)
            {
                if (!ReferenceEquals(Current, null))
                {
                    m_ForwardsNavigationStack.Push(Current);
                }

                Current = m_BackwardsNavigationStack.Pop();
            }
        }

        protected virtual void OnCurrentChanged(EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, e);
        }
    }
}
