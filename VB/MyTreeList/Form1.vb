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
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows.Forms

Namespace MyXtraTreeList
	Partial Public Class Form1
		Inherits Form
		Private DT As DataTable

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			'myTreeList1.DataSource = null;
			'myTreeList1.Columns.Clear();
			'myTreeList1.Bands.Clear();
			'myTreeList1.RootBand.Children.Clear();
			DT = New DataTable()
			DT.Columns.Add("ID", GetType(Integer))
			DT.Columns.Add("ParentID", GetType(Integer))
			DT.Columns.Add("FirstName", GetType(String))
			DT.Columns.Add("LastName", GetType(String))
			DT.Columns.Add("Age", GetType(Integer))
			DT.Columns.Add("Position", GetType(String))
			DT.Columns.Add("Experience", GetType(Integer))
			DT.Rows.Add(1, 0, "Andy", "Smith", 45, "Director", 15)
			DT.Rows.Add(2, 1, "Elizabeth", "Woods", 30, "Manager", 9)
			DT.Rows.Add(3, 2, "Henry", "Hill", 28, "Employee", 4)
			DT.Rows.Add(4, 2, "Bobby", "Lopez", 24, "Employee", 2)
			DT.Rows.Add(5, 1, "Joe", "Martinez", 38, "Chief accountant", 13)
			DT.Rows.Add(6, 5, "Nora", "Gates", 22, "Accountant", 1)
			DT.Rows.Add(7, 0, "Derek", "Swanson", 58, "Director", 25)
			DT.Rows.Add(8, 7, "Ben", "Davidson", 22, "Employee", 3)
			myTreeList1.DataSource = DT
			myTreeList1.ForceInitialize()
			gridControl1.DataSource = DT
			myTreeList1.SetBandsWidth(3)
			Dim BandPersInfo As MyTreeListBand = myTreeList1.Bands.Add(0, 2, "Personal info")
			Dim BandLastName As MyTreeListBand = BandPersInfo.CreateChild(0, 2, "Last Name")
			Dim BandWorkInfo As MyTreeListBand = myTreeList1.Bands.Add(2, 1, "Work info")
			Dim BandPosition As MyTreeListBand = BandWorkInfo.CreateChild(0, 1, "Position")
			BandLastName.SetColumn(0, myTreeList1.Columns(0))
			BandLastName.BandColumn = myTreeList1.Columns(1)
			BandLastName.SetColumn(1, myTreeList1.Columns(2))
			BandPosition.BandColumn = myTreeList1.Columns(3)
			BandPosition.SetColumn(0, myTreeList1.Columns(4))
			myTreeList1.ViewInfo.RC.NeedsRestore = True
			myTreeList1.LayoutChanged()
			myTreeList1.BestFitColumns()
			myTreeList1.LayoutChanged()
		End Sub

		Private Sub myTreeList1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles myTreeList1.MouseMove

		End Sub
	End Class
End Namespace
