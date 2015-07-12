using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FolderExplorer.ViewModels;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Base;
using FolderExplorer.Models;
using DevExpress.XtraGrid.Views.Grid;

namespace FolderExplorer.Views {
    public partial class FoldersView : DevExpress.XtraEditors.XtraUserControl {
        public FoldersView() {
            InitializeComponent();
            SetupGrid();
            var fluentAPI = mvvmContext1.OfType<FoldersViewModel>();
            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, "FocusedRowObjectChanged").SetBinding(x => x.CurrentFile, args => args.Row as File, (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            fluentAPI.WithEvent<RowClickEventArgs>(gridView1, "RowClick").EventToCommand(x => x.Open(null), x => x.CurrentFile, args => (args.Clicks == 2) && (args.Button == MouseButtons.Left));
            fluentAPI.WithEvent<KeyEventArgs>(gridView1, "KeyUp").EventToCommand(x => x.Open(null), x => x.CurrentFile, args => args.KeyCode == Keys.Enter);
            fluentAPI.WithEvent<KeyEventArgs>(gridView1, "KeyUp").EventToCommand(x => x.Return(null), x => x.ParentFolder, args => args.KeyCode == Keys.Back);
            fluentAPI.SetBinding(gridControl1, gc => gc.DataSource, vm => vm.Files);
        }

        private void SetupGrid() {
            gridView1.OptionsBehavior.Editable = false;
        }
    }
}
