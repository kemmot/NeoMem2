using System.Collections;
using System.Windows.Forms;

namespace NeoMem2.Gui
{
    public class ListViewSorter : IComparer
    {
        private int m_SortColumn;
        private bool m_ReverseSort;


        public void SetSortColumn(int column)
        {
            if (column == m_SortColumn) m_ReverseSort = !m_ReverseSort;
            else m_ReverseSort = false;

            m_SortColumn = column;
        }


        #region IComparer Members

        public int Compare(object x, object y)
        {
            ListViewItem itemX = (ListViewItem)x;
            ListViewItem itemY = (ListViewItem)y;

            string valueX = itemX.SubItems[m_SortColumn].Text;
            string valueY = itemY.SubItems[m_SortColumn].Text;

            int result = valueX.CompareTo(valueY);
            if (m_ReverseSort) result *= -1;
            return result;
        }

        #endregion
    }
}