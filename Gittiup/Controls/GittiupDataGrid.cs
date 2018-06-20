using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gittiup.Controls
{
    public class GittiupDataGrid : DataGrid
    {
        public GittiupDataGrid()
        {
            // Styles don't automatically apply to subclasses, so we have to force the issue
            Style = (Style)FindResource(typeof(DataGrid));

            SelectionMode = DataGridSelectionMode.Single;
            SelectionUnit = DataGridSelectionUnit.FullRow;

            // This is a hack to fix a bug in DataGrid where in many scenarios it fails to accurately calculate
            // the width of the columns and gives too much width to * columns.  This hack uses the LayoutUpdated
            // event to track for the first time when the column's width has changed from the default value of the
            // MinWidth.  At that point, we can reset the Width to zero and back, and finally the column widths
            // will be calculated accurately.
            void OnLayoutUpdated(object sender, EventArgs args)
            {
                foreach (DataGridColumn column in Columns.Where(x => x.Width.IsStar))
                {
                    if (column.ActualWidth != column.MinWidth)
                    {
                        DataGridLength width = column.Width;
                        column.Width = 0;
                        column.Width = width;
                        LayoutUpdated -= OnLayoutUpdated;
                    }
                }
            }

            LayoutUpdated += OnLayoutUpdated;
        }
    }
}