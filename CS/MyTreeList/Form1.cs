// Developer Express Code Central Example:
// How to create a custom TreeList that allows you to group tree list columns in bands in the same manner as in AdvancedGridView
// 
// This example demonstrates how to create a custom tree list with the capability
// to create bands in the same manner as in AdvancedGridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3581
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MyXtraTreeList {
    public partial class Form1 : Form {
        private DataTable DT;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            //myTreeList1.DataSource = null;
            //myTreeList1.Columns.Clear();
            //myTreeList1.Bands.Clear();
            //myTreeList1.RootBand.Children.Clear();
            DT = new DataTable();
            DT.Columns.Add("ID", typeof(int));
            DT.Columns.Add("ParentID", typeof(int));
            DT.Columns.Add("FirstName", typeof(string));
            DT.Columns.Add("LastName", typeof(string));
            DT.Columns.Add("Age", typeof(int));
            DT.Columns.Add("Position", typeof(string));
            DT.Columns.Add("Experience", typeof(int));
            DT.Rows.Add(1, 0, "Andy", "Smith", 45, "Director", 15);
            DT.Rows.Add(2, 1, "Elizabeth", "Woods", 30, "Manager", 9);
            DT.Rows.Add(3, 2, "Henry", "Hill", 28, "Employee", 4);
            DT.Rows.Add(4, 2, "Bobby", "Lopez", 24, "Employee", 2);
            DT.Rows.Add(5, 1, "Joe", "Martinez", 38, "Chief accountant", 13);
            DT.Rows.Add(6, 5, "Nora", "Gates", 22, "Accountant", 1);
            DT.Rows.Add(7, 0, "Derek", "Swanson", 58, "Director", 25);
            DT.Rows.Add(8, 7, "Ben", "Davidson", 22, "Employee", 3);
            myTreeList1.DataSource = DT;
            myTreeList1.ForceInitialize();
            gridControl1.DataSource = DT;
            myTreeList1.SetBandsWidth(3);
            MyTreeListBand BandPersInfo = myTreeList1.Bands.Add(0, 2, "Personal info");
            MyTreeListBand BandLastName = BandPersInfo.CreateChild(0, 2, "Last Name");
            MyTreeListBand BandWorkInfo = myTreeList1.Bands.Add(2, 1, "Work info");
            MyTreeListBand BandPosition = BandWorkInfo.CreateChild(0, 1, "Position");
            BandLastName.SetColumn(0, myTreeList1.Columns[0]);
            BandLastName.BandColumn = myTreeList1.Columns[1];
            BandLastName.SetColumn(1, myTreeList1.Columns[2]);
            BandPosition.BandColumn = myTreeList1.Columns[3];
            BandPosition.SetColumn(0, myTreeList1.Columns[4]);
            myTreeList1.ViewInfo.RC.NeedsRestore = true;
            myTreeList1.LayoutChanged();
            myTreeList1.BestFitColumns();
            myTreeList1.LayoutChanged();
        }
    }
}
