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
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.ViewInfo
Imports DevExpress.XtraTreeList.Handler
Imports DevExpress.LookAndFeel
Imports DevExpress.XtraTreeList.Menu
Imports DevExpress.Utils.Menu

Namespace MyXtraTreeList
	Public Class MyTreeList
		Inherits TreeList
		Private fRootBand As MyTreeListBand

		Public fBands As MyTreeListBandCollection

		Public Sub New()
			MyBase.New()
			Bands = New MyTreeListBandCollection(Me)
			fRootBand = Bands(0)
			AddHandler Me.PopupMenuShowing, AddressOf onPopupMenuShowing
		End Sub

		Public Sub New(ByVal ignore As Object)
			MyBase.New(ignore)
		End Sub

		Protected Friend ReadOnly Property RootBand() As MyTreeListBand
			Get
				Return fRootBand
			End Get
		End Property

		Public Property Bands() As MyTreeListBandCollection
			Get
				Return fBands
			End Get
			Set(ByVal value As MyTreeListBandCollection)
				If fBands IsNot value Then
					fBands = value
					fBands.UpdateColumns()
				End If
			End Set
		End Property

		Public Shadows ReadOnly Property ViewInfo() As MyTreeListViewInfo
			Get
				Return TryCast(MyBase.ViewInfo, MyTreeListViewInfo)
			End Get
		End Property

		Protected Overrides Function CreateViewInfo() As TreeListViewInfo
			Return New MyTreeListViewInfo(Me)
		End Function

		Protected Overrides Function CreateHandler() As TreeListHandler
			Return New MyTreeListHandler(Me)
		End Function

		Public Function Swap(ByVal BandA As MyTreeListBand, ByVal BandB As MyTreeListBand) As Boolean
			If BandA.Parent IsNot Nothing Then
				Return BandA.Parent.TryToSwap(BandA, BandB)
			End If
			Return False
		End Function

		Public Function Swap(ByVal Band As MyTreeListBand, ByVal Column As TreeListColumn) As Boolean
			If Band.Parent IsNot Nothing Then
				Return Band.Parent.TryToSwapBandAndColumn(Band, Column)
			End If
			Return False
		End Function

		Public Function Swap(ByVal ColumnA As TreeListColumn, ByVal ColumnB As TreeListColumn) As Boolean
			Return RootBand.TryToSwapColumns(ColumnA, ColumnB)
		End Function

		Friend Sub MyRaiseDragObjectOver(ByVal e As DragObjectOverEventArgs)
			RaiseDragObjectOver(e)
		End Sub

		Friend Sub MyRaiseDragObjectDrop(ByVal e As DragObjectDropEventArgs)
			RaiseDragObjectDrop(e)
		End Sub

		Public Sub SetBandsWidth(ByVal Width As Integer)
			RootBand.SetWidth(Width)
		End Sub

		Protected Friend Shadows ReadOnly Property ElementsLookAndFeel() As UserLookAndFeel
			Get
				Return MyBase.ElementsLookAndFeel
			End Get
		End Property

		Protected Overrides Sub ResizeColumn(ByVal index As Integer, ByVal byUnits As Integer, ByVal maxPossibleWidth As Integer)
			If ViewInfo.BandLinks.ContainsKey(VisibleColumns(index)) Then
				Dim childBands As MyTreeListBandCollection = ViewInfo.BandLinks(VisibleColumns(index)).Band.Children
				Dim childColumns() As TreeListColumn = ViewInfo.BandLinks(VisibleColumns(index)).Band.Columns
				Dim unitsPerChild As Integer = Convert.ToInt32( byUnits / (childBands.Count + childColumns.Length))
				For Each band As MyTreeListBand In childBands
					If band.Visible Then
						ResizeColumn(band.BandColumn.VisibleIndex, unitsPerChild, maxPossibleWidth)
					End If
				Next band
				For Each col As TreeListColumn In childColumns
					If col IsNot Nothing AndAlso col.Visible Then
						ResizeColumn(col.VisibleIndex, unitsPerChild, maxPossibleWidth)
					End If
				Next col
			Else
				MyBase.ResizeColumn(index, byUnits, maxPossibleWidth)
			End If
		End Sub

		Private Sub onPopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
			If e.Menu.MenuType = TreeListMenuType.Column Then
				For Each item As DXMenuItem In e.Menu.Items
					If item.Caption = "Column Chooser" Then
						item.Enabled = False
						Exit For
					End If
				Next item
			End If
		End Sub
	End Class
End Namespace