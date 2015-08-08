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
using DevExpress.Utils.Helpers;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;

namespace FolderExplorer.Views {
    public partial class FoldersView : DevExpress.XtraEditors.XtraUserControl {
        public FoldersView() {
            InitializeComponent();
            gridView1.SetupGrid();
            colImage.SetupIconColumn();
            SetupMenu();
            MVVMContext.RegisterXtraDialogService();
            mvvmContext1.RegisterService(WindowedDocumentManagerService.CreateXtraFormService(this));
            MVVMContext.RegisterXtraFormWindowedDocumentManagerService();
            mvvmContext2.OfType<PropertiesViewModel>().SetBinding(barStaticItem1, bi => bi.Caption, x => x.Info);
            var fluentAPI = mvvmContext1.OfType<FoldersViewModel>();
            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, "FocusedRowObjectChanged").SetBinding(x => x.CurrentFile, args => args.Row as File, (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));
            fluentAPI.WithEvent<RowClickEventArgs>(gridView1, "RowClick").EventToCommand(x => x.Open(null), x => x.CurrentFile, args => (args.Clicks == 2) && (args.Button == MouseButtons.Left));
            fluentAPI.WithEvent<KeyEventArgs>(gridView1, "KeyUp").EventToCommand(x => x.Open(null), x => x.CurrentFile, args => args.KeyCode == Keys.Enter);
            fluentAPI.WithEvent<KeyEventArgs>(gridView1, "KeyUp").EventToCommand(x => x.Return(null), x => x.ParentFolder, args => args.KeyCode == Keys.Back);
            fluentAPI.SetBinding(gridControl1, gc => gc.DataSource, vm => vm.Files);
            fluentAPI.SetBinding(barEditItem1, bi => bi.EditValue, x => x.ExtensionFilter);
            fluentAPI.BindCommand(barButtonItem1, x => x.Return(null), x => x.ParentFolder);
            fluentAPI.BindCommand(barButtonItem2, x => x.Search());
            fluentAPI.BindCommand(barButtonItem3, x => x.ViewFile(null), x => x.CurrentFile);
        }

        private void SetupMenu() {
            repositoryItemComboBox1.Items.AddRange(FolderExplorer.Core.FilterItem.GetAllFilters());
            repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        }
    }
}
