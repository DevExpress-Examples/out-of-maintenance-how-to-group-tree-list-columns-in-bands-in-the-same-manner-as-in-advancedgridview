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
Imports System.Drawing
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.XtraTreeList.ViewInfo

Namespace MyXtraTreeList
	Public Class MyTreeListBandInfo
		Inherits ColumnInfo
		Private fBand As MyTreeListBand

		Public Sub New(ByVal band As MyTreeListBand)
		': base(new TreeListColumn())
			MyBase.New(band.BandColumn)
			fBand = band
			Type = ColumnInfoType.ColumnButton
		End Sub

		Public Overridable ReadOnly Property Band() As MyTreeListBand
			Get
				Return fBand
			End Get
		End Property

		Public Shadows ReadOnly Property TreeList() As MyTreeList
			Get
				Return Band.TreeList
			End Get
		End Property

		Public Overridable Sub CalcBandInfo(ByVal viewInfo As MyTreeListViewInfo)
			Dim TL As MyTreeList = viewInfo.TreeList
			Appearance.Assign(viewInfo.PaintAppearance.HeaderPanel)
			Dim VisiblePosition As Integer = Band.IndexToColumnHandle(0)
			Dim X As Integer = viewInfo.ViewRects.IndicatorBounds.Width + 1 - TL.LeftCoord
			For i As Integer = 0 To VisiblePosition - 1
				Dim Col As TreeListColumn = TreeList.RootBand.GetColumn(i)
				If (Col IsNot Nothing) AndAlso Col.Visible Then
					X += Col.Width
				End If
			Next i
			Dim Y As Integer = (Band.Level - 1) * viewInfo.BandHeight + viewInfo.ViewRects.ColumnPanel.Y
			Dim Width As Integer = 0
			For i As Integer = 0 To Band.Width - 1
				Dim Col As TreeListColumn = Band.GetColumn(i)
				If (Col IsNot Nothing) AndAlso Col.Visible Then
					Width += Col.Width
				End If
			Next i
			Dim Height As Integer = viewInfo.BandHeight + 1
			If (Band.Children.Count = 0) AndAlso ((Not Band.HasColumns())) Then
				Height *= viewInfo.BandMaxLevel - Band.Level
			End If
			Bounds = New Rectangle(X, Y, Width, Height)
			CaptionRect = New Rectangle(0, 0, Width, Height)
			Caption = Band.Name
		End Sub
	End Class
End Namespace
