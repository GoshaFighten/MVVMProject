using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorer {
    public static class Extensions {
        public static void SetupGrid(this GridView view) {
            view.FocusRectStyle = DrawFocusRectStyle.None;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;
            view.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            view.OptionsView.ShowIndicator = false;
            view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            view.OptionsView.ShowGroupPanel = false;
        }

        public static void SetupIconColumn(this GridColumn col) {
            col.OptionsColumn.AllowSize = false;
            col.OptionsColumn.FixedWidth = true;
            col.OptionsColumn.ReadOnly = true;
            col.Visible = true;
            col.VisibleIndex = 0;
            col.Width = 23;
        }
    }
}
