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
using DevExpress.XtraGrid.Views.Base;
using FolderExplorer.Models;
using DevExpress.XtraGrid.Views.Grid;

namespace FolderExplorer.Views {
    [DevExpress.Utils.MVVM.UI.ViewType("Search")]
    public partial class SearchView : DevExpress.XtraEditors.XtraUserControl {
        public SearchView() {
            InitializeComponent();
            gridView1.SetupGrid();
            colImage.SetupIconColumn();
            var fluentAPI = mvvmContext1.OfType<SearchViewModel>();
            fluentAPI.SetBinding(textEdit1, te => te.EditValue, x => x.SearchText);
            fluentAPI.BindCommand(simpleButton1, x => x.Search(null), x => x.Root);
            fluentAPI.BindCancelCommand(simpleButton2, x => x.Search(null));
            fluentAPI.SetBinding(gridControl1, gc => gc.DataSource, x => x.Files);
            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, "FocusedRowObjectChanged").SetBinding(x => x.CurrentFile, args => args.Row as File, (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
        }

        private void SearchView_ParentChanged(object sender, EventArgs e) {
            if (this.Parent == null) {
                return;
            }
            this.Parent.Controls[0].Visible = false;
            this.Parent.Controls[1].Visible = false;
            this.Parent.Controls[2].Dock = DockStyle.Fill;
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e) {
            if ((e.Clicks == 2) && (e.Button == MouseButtons.Left)) {
                Submit();
            }
        }

        private void Submit() {
            this.FindForm().DialogResult = DialogResult.OK;
            this.FindForm().Close();
        }

        private void gridView1_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                Submit();
            }
        }
    }
}
