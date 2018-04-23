// Developer Express Code Central Example:
// How to create a custom TreeList that allows you to group tree list columns in bands in the same manner as in AdvancedGridView
// 
// This example demonstrates how to create a custom tree list with the capability
// to create bands in the same manner as in AdvancedGridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3581

namespace MyXtraTreeList
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myTreeList1 = new MyXtraTreeList.MyTreeList();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.GCLastName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.GCFirstName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.GCAge = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.GCPosition = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.GCExperience = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.myTreeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // myTreeList1
            // 
            this.myTreeList1.Location = new System.Drawing.Point(12, 12);
            this.myTreeList1.LookAndFeel.SkinName = "Blue";
            this.myTreeList1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.myTreeList1.Name = "myTreeList1";
            this.myTreeList1.OptionsPrint.UsePrintStyles = true;
            this.myTreeList1.OptionsView.AutoWidth = false;
            this.myTreeList1.Size = new System.Drawing.Size(442, 238);
            this.myTreeList1.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(460, 12);
            this.gridControl1.LookAndFeel.SkinName = "Blue";
            this.gridControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl1.MainView = this.advBandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(376, 238);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1,
            this.gridView1});
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2});
            this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.GCFirstName,
            this.GCLastName,
            this.GCAge,
            this.GCPosition,
            this.GCExperience});
            this.advBandedGridView1.GridControl = this.gridControl1;
            this.advBandedGridView1.Name = "advBandedGridView1";
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "Personal Info";
            this.gridBand1.Columns.Add(this.GCLastName);
            this.gridBand1.Columns.Add(this.GCFirstName);
            this.gridBand1.Columns.Add(this.GCAge);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 194;
            // 
            // GCLastName
            // 
            this.GCLastName.Caption = "Last Name";
            this.GCLastName.FieldName = "LastName";
            this.GCLastName.Name = "GCLastName";
            this.GCLastName.Visible = true;
            this.GCLastName.Width = 106;
            // 
            // GCFirstName
            // 
            this.GCFirstName.Caption = "First Name";
            this.GCFirstName.FieldName = "FirstName";
            this.GCFirstName.Name = "GCFirstName";
            this.GCFirstName.RowIndex = 1;
            this.GCFirstName.Visible = true;
            this.GCFirstName.Width = 101;
            // 
            // GCAge
            // 
            this.GCAge.Caption = "Age";
            this.GCAge.FieldName = "Age";
            this.GCAge.Name = "GCAge";
            this.GCAge.RowIndex = 1;
            this.GCAge.Visible = true;
            this.GCAge.Width = 93;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Work Info";
            this.gridBand2.Columns.Add(this.GCPosition);
            this.gridBand2.Columns.Add(this.GCExperience);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 75;
            // 
            // GCPosition
            // 
            this.GCPosition.Caption = "Position";
            this.GCPosition.FieldName = "Position";
            this.GCPosition.Name = "GCPosition";
            this.GCPosition.Visible = true;
            // 
            // GCExperience
            // 
            this.GCExperience.Caption = "Experience";
            this.GCExperience.FieldName = "Experience";
            this.GCExperience.Name = "GCExperience";
            this.GCExperience.RowIndex = 1;
            this.GCExperience.Visible = true;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 265);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.myTreeList1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myTreeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyTreeList myTreeList1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn GCFirstName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn GCLastName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn GCAge;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn GCPosition;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn GCExperience;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
    }
}

