' Developer Express Code Central Example:
' How to create a custom TreeList that allows you to group tree list columns in bands in the same manner as in AdvancedGridView
' 
' This example demonstrates how to create a custom tree list with the capability
' to create bands in the same manner as in AdvancedGridView.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3581


Imports Microsoft.VisualBasic
Imports System
Namespace MyXtraTreeList
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.myTreeList1 = New MyXtraTreeList.MyTreeList()
			Me.gridControl1 = New DevExpress.XtraGrid.GridControl()
			Me.advBandedGridView1 = New DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView()
			Me.gridBand1 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
			Me.GCLastName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
			Me.GCFirstName = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
			Me.GCAge = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
			Me.gridBand2 = New DevExpress.XtraGrid.Views.BandedGrid.GridBand()
			Me.GCPosition = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
			Me.GCExperience = New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.myTreeList1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.advBandedGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' myTreeList1
			' 
			Me.myTreeList1.Location = New System.Drawing.Point(12, 12)
			Me.myTreeList1.LookAndFeel.SkinName = "Blue"
			Me.myTreeList1.LookAndFeel.UseDefaultLookAndFeel = False
			Me.myTreeList1.Name = "myTreeList1"
			Me.myTreeList1.OptionsPrint.UsePrintStyles = True
			Me.myTreeList1.OptionsView.AutoWidth = False
			Me.myTreeList1.Size = New System.Drawing.Size(442, 238)
			Me.myTreeList1.TabIndex = 2
			' 
			' gridControl1
			' 
			Me.gridControl1.Location = New System.Drawing.Point(460, 12)
			Me.gridControl1.LookAndFeel.SkinName = "Blue"
			Me.gridControl1.LookAndFeel.UseDefaultLookAndFeel = False
			Me.gridControl1.MainView = Me.advBandedGridView1
			Me.gridControl1.Name = "gridControl1"
			Me.gridControl1.Size = New System.Drawing.Size(376, 238)
			Me.gridControl1.TabIndex = 3
			Me.gridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.advBandedGridView1, Me.gridView1})
			' 
			' advBandedGridView1
			' 
			Me.advBandedGridView1.Bands.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.GridBand() { Me.gridBand1, Me.gridBand2})
			Me.advBandedGridView1.Columns.AddRange(New DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn() { Me.GCFirstName, Me.GCLastName, Me.GCAge, Me.GCPosition, Me.GCExperience})
			Me.advBandedGridView1.GridControl = Me.gridControl1
			Me.advBandedGridView1.Name = "advBandedGridView1"
			' 
			' gridBand1
			' 
			Me.gridBand1.Caption = "Personal Info"
			Me.gridBand1.Columns.Add(Me.GCLastName)
			Me.gridBand1.Columns.Add(Me.GCFirstName)
			Me.gridBand1.Columns.Add(Me.GCAge)
			Me.gridBand1.Name = "gridBand1"
			Me.gridBand1.Width = 194
			' 
			' GCLastName
			' 
			Me.GCLastName.Caption = "Last Name"
			Me.GCLastName.FieldName = "LastName"
			Me.GCLastName.Name = "GCLastName"
			Me.GCLastName.Visible = True
			Me.GCLastName.Width = 106
			' 
			' GCFirstName
			' 
			Me.GCFirstName.Caption = "First Name"
			Me.GCFirstName.FieldName = "FirstName"
			Me.GCFirstName.Name = "GCFirstName"
			Me.GCFirstName.RowIndex = 1
			Me.GCFirstName.Visible = True
			Me.GCFirstName.Width = 101
			' 
			' GCAge
			' 
			Me.GCAge.Caption = "Age"
			Me.GCAge.FieldName = "Age"
			Me.GCAge.Name = "GCAge"
			Me.GCAge.RowIndex = 1
			Me.GCAge.Visible = True
			Me.GCAge.Width = 93
			' 
			' gridBand2
			' 
			Me.gridBand2.Caption = "Work Info"
			Me.gridBand2.Columns.Add(Me.GCPosition)
			Me.gridBand2.Columns.Add(Me.GCExperience)
			Me.gridBand2.Name = "gridBand2"
			Me.gridBand2.Width = 75
			' 
			' GCPosition
			' 
			Me.GCPosition.Caption = "Position"
			Me.GCPosition.FieldName = "Position"
			Me.GCPosition.Name = "GCPosition"
			Me.GCPosition.Visible = True
			' 
			' GCExperience
			' 
			Me.GCExperience.Caption = "Experience"
			Me.GCExperience.FieldName = "Experience"
			Me.GCExperience.Name = "GCExperience"
			Me.GCExperience.RowIndex = 1
			Me.GCExperience.Visible = True
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me.gridControl1
			Me.gridView1.Name = "gridView1"
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(848, 265)
			Me.Controls.Add(Me.gridControl1)
			Me.Controls.Add(Me.myTreeList1)
			Me.Name = "Form1"
			Me.Text = "Form1"
'			Me.Load += New System.EventHandler(Me.Form1_Load);
			CType(Me.myTreeList1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.advBandedGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private myTreeList1 As MyTreeList
		Private gridControl1 As DevExpress.XtraGrid.GridControl
		Private advBandedGridView1 As DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView
		Private gridView1 As DevExpress.XtraGrid.Views.Grid.GridView
		Private GCFirstName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
		Private GCLastName As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
		Private GCAge As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
		Private GCPosition As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
		Private GCExperience As DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn
		Private gridBand1 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
		Private gridBand2 As DevExpress.XtraGrid.Views.BandedGrid.GridBand
	End Class
End Namespace

