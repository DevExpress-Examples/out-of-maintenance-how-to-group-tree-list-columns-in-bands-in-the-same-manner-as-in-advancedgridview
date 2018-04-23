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
Imports System.Windows.Forms
Imports DevExpress.XtraTreeList.Handler
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.ViewInfo
Imports DevExpress.Utils.Drawing.Helpers
Imports DevExpress.XtraTreeList.Columns

Namespace MyXtraTreeList
	Public Class MyColumnDraggingState
		Inherits TreeListHandler.ColumnDraggingState
		Private flastPosition As MyPositionInfo

		Private dropTargetHighlighted_Renamed As Boolean

		Public Sub New(ByVal handler As MyTreeListHandler)
			MyBase.New(handler)
			dropTargetHighlighted_Renamed = False
		End Sub

		Public Shadows ReadOnly Property TreeList() As MyTreeList
			Get
				Return TryCast(MyBase.TreeList, MyTreeList)
			End Get
		End Property

		Public Shadows ReadOnly Property ViewInfo() As MyTreeListViewInfo
			Get
				Return TryCast(MyBase.ViewInfo, MyTreeListViewInfo)
			End Get
		End Property

		Protected ReadOnly Property SourceInfo() As ColumnInfo
			Get
				Return Data.DownHitTest.ColumnInfo
			End Get
		End Property

		Protected ReadOnly Property SourceBandInfo() As MyTreeListBandInfo
			Get
				Return ColumnInfoToBandInfo(SourceInfo)
			End Get
		End Property

		Protected Overridable Property LastPosition() As MyPositionInfo
			Get
				Return flastPosition
			End Get
			Set(ByVal value As MyPositionInfo)
				flastPosition = value
			End Set
		End Property

		Protected Property DropTargetHighlighted() As Boolean
			Get
				Return dropTargetHighlighted_Renamed
			End Get
			Set(ByVal value As Boolean)
				dropTargetHighlighted_Renamed = value
			End Set
		End Property

		Protected Function ColumnInfoToBandInfo(ByVal ci As ColumnInfo) As MyTreeListBandInfo
			Dim bi As MyTreeListBandInfo = TryCast(ci, MyTreeListBandInfo)
			If (bi Is Nothing) AndAlso ViewInfo.BandLinks.ContainsKey(ci.Column) Then
				bi = ViewInfo.BandLinks(ci.Column)
			End If
			Return bi
		End Function

		Protected Overridable Function IsBandPositionValid(ByVal ci As ColumnInfo) As Boolean
			If ci Is Nothing OrElse ci.Column Is Nothing Then
				Return False
			End If
			Dim bi As MyTreeListBandInfo = ColumnInfoToBandInfo(ci)
			If SourceBandInfo IsNot Nothing Then
				If bi Is Nothing Then
					Return SourceBandInfo.Band.IsBrother(ci.Column)
				Else
					Return (SourceBandInfo.Band.IsBrother(bi.Band)) AndAlso (SourceBandInfo.Band IsNot bi.Band)
				End If
			Else
				If bi Is Nothing Then
					Return TreeList.Bands.IsBrotherColumns(SourceInfo.Column, ci.Column)
				Else
					Return bi.Band.IsBrother(SourceInfo.Column)
				End If
			End If
		End Function

		Protected Overrides Function GetDragEffect(ByVal ht As HitInfoType, ByVal pos As Integer, ByVal customizationZone As Boolean) As DragDropEffects
			If customizationZone Then
				Return DragDropEffects.None
			End If
            If ht = HitInfoType.CustomizationForm Then
                Return DragDropEffects.None
            End If
			If pos >= 0 Then
				Return DragDropEffects.Move
			End If
			Return (If(SourceInfo.Column.VisibleIndex = -1, DragDropEffects.None, DragDropEffects.Move))
		End Function

		Public Overrides Sub DoColumnDragging(ByVal p As Point, ByVal ht As HitInfoType)
			Dim pos As New MyPositionInfo()
			Dim rect As Rectangle
			Dim index As Integer = GetDragColumnInfo(p, ht, rect)
			Dim ci As ColumnInfo = ViewInfo.GetHitTest(p).ColumnInfo
			Dim r As Rectangle = If((rect.Width > 0 AndAlso rect.Height > 0), TreeList.RectangleToClient(rect), Rectangle.Empty)
			pos.Assign(index, r, (ci IsNot Nothing) AndAlso IsDragPositionValid(index, ht, ci), ci)
			If SourceBandInfo Is Nothing Then
				TreeList.MyRaiseDragObjectOver(New DragObjectOverEventArgs(SourceInfo.Column, pos))
			Else
				TreeList.MyRaiseDragObjectOver(New DragObjectOverEventArgs(SourceBandInfo.Band, pos))
			End If
			If CheckColumnOptions(SourceInfo.Column, index) Then
				If DropTargetHighlighted AndAlso (Not UseArrows) Then
					DrawReversibleFrame(Data.DragColumnRect)
				End If
				If pos.Valid Then
					DrawReversibleFrame(rect)
					UpdateColumnDragFrame(rect)
					DropTargetHighlighted = True
				Else
					DropTargetHighlighted = False
					UpdateColumnDragFrame(Rectangle.Empty)
				End If
				Data.DragColumnRect = rect
			End If
			LastPosition = pos
			Dim effect As DragDropEffects = GetDragEffect(ht, pos.Index, IsInCustomizationZone(p))
			Data.DragMaster.DoDrag(TreeList.PointToScreen(p), effect, False)
		End Sub

		Public Overrides Sub DoEndColumnDragging(ByVal p As Point, ByVal ht As HitInfoType)
			Data.DragMaster.EndDrag()
            If LastPosition IsNot Nothing AndAlso ht <> HitInfoType.CustomizationForm AndAlso (Not IsInCustomizationZone(p)) Then
                Dim pos As New MyPositionInfo()
                pos.Assign(LastPosition)
                If SourceBandInfo Is Nothing Then
                    TreeList.MyRaiseDragObjectDrop(New DragObjectDropEventArgs(SourceInfo.Column, pos))
                Else
                    TreeList.MyRaiseDragObjectDrop(New DragObjectDropEventArgs(SourceBandInfo.Band, pos))
                End If
                If LastPosition.Valid Then
                    If pos.Index >= 0 Then
                        Dim posBandInfo As MyTreeListBandInfo = ColumnInfoToBandInfo(pos.Info)
                        If SourceBandInfo Is Nothing Then
                            If posBandInfo Is Nothing Then
                                TreeList.Swap(pos.Info.Column, SourceInfo.Column)
                            Else
                                TreeList.Swap(posBandInfo.Band, SourceInfo.Column)
                            End If
                        Else
                            If posBandInfo Is Nothing Then
                                TreeList.Swap(SourceBandInfo.Band, pos.Info.Column)
                            Else
                                TreeList.Swap(SourceBandInfo.Band, posBandInfo.Band)
                            End If
                        End If
                    End If
                End If
            End If
			UpdateColumnDragFrame(Rectangle.Empty)
			SetState(Regular)
		End Sub

		Private Function GetDragColumnInfo(ByVal ptMouse As Point, ByVal ht As HitInfoType, <System.Runtime.InteropServices.Out()> ByRef colRect As Rectangle) As Integer
			colRect = Rectangle.Empty
            If ht = HitInfoType.CustomizationForm AndAlso SourceInfo.Column.OptionsColumn.AllowMoveToCustomizationForm Then
                Return -101
            End If
			Dim visibleColPanelBounds As New Rectangle(ViewInfo.ViewRects.ColumnPanel.Left + ViewInfo.ViewRects.IndicatorWidth, ViewInfo.ViewRects.ColumnPanel.Top, ViewInfo.ViewRects.ColumnPanel.Width - ViewInfo.ViewRects.IndicatorWidth, ViewInfo.ViewRects.ColumnPanel.Height + IncreasedColumnHeight)
			If (Not visibleColPanelBounds.Contains(ptMouse)) Then
				If ptMouse.X < (ViewInfo.ViewRects.Client.Left + ViewInfo.ViewRects.IndicatorWidth) OrElse ptMouse.X > ViewInfo.ViewRects.Client.Right - 2 Then
					Return -1
				End If
				Return -100
			End If
			Dim ci As ColumnInfo = ViewInfo.GetHitTest(ptMouse).ColumnInfo
            If ht = HitInfoType.FixedRightDiv Then
                ci = ViewInfo.ColumnsInfo(ViewInfo.FixedRightColumn)
            End If
            If ht = HitInfoType.FixedLeftDiv Then
                ci = ViewInfo.ColumnsInfo(ViewInfo.FindFixedLeftColumn())
            End If
			If ci Is Nothing Then
				Return -1
			End If
            If ((ci.Type <> ColumnInfo.ColumnInfoType.Column) AndAlso Not (TypeOf ci Is MyTreeListBandInfo)) OrElse ci.Type = ColumnInfo.ColumnInfoType.ColumnButton Then
                If ci.Type = ColumnInfo.ColumnInfoType.ColumnBlank AndAlso TreeList.VisibleColumns.Count = 0 Then
                    colRect = TreeList.RectangleToScreen(ci.Bounds)
                    Return 0
                End If
                Return -1
            End If
			If Data.DownHitTest.MouseDest = ci.Bounds Then
				Return -1
			End If
			Dim rect As Rectangle = Rectangle.Intersect(ci.Bounds, visibleColPanelBounds)
			Dim showIndicatorIndexOffset As Integer = (If(TreeList.OptionsView.ShowIndicator, 1, 0))
			Dim colIndex As Integer = ViewInfo.ColumnsInfo.Columns.IndexOf(ci) - showIndicatorIndexOffset
			Dim nextAfterPressed As Boolean = (ci.Column.VisibleIndex = SourceInfo.Column.VisibleIndex + 1)
			Dim lastVisbleLeft As Boolean = ci.Column Is ViewInfo.FixedLeftColumn
			Dim lastVisibleRight As Boolean = IsBeforeFixedRight(ci.Column)
			Dim lastVisible As Boolean = (rect.Right = visibleColPanelBounds.Right OrElse ci.Column.VisibleIndex = TreeList.VisibleColumns.Count - 1 OrElse lastVisbleLeft OrElse lastVisibleRight)
			If SourceInfo.Column.VisibleIndex = -1 Then
				nextAfterPressed = False
			End If
			If nextAfterPressed OrElse lastVisible Then
				If rect.Width < 6 Then
					Return -1
				End If
					Dim nextR As Rectangle = rect, nextR2 As Rectangle = rect
				nextR2.X = nextR.Right - 1
				nextR2.Width = 1
				nextR.X = nextR.Right - nextR.Width \ 2
                nextR.Width \= 2
				If lastVisible Then
					If NextRectContains(nextR, ptMouse) Then
						rect = If(UseArrows, nextR2, nextR)
						If colIndex >= 0 Then
							ci = TryCast(ViewInfo.ColumnsInfo.Columns(colIndex), ColumnInfo)
							colIndex += 1
						End If
					Else
						If nextAfterPressed Then
							Return -1
						End If
					End If
				Else
					If (Not NextRectContains(nextR, ptMouse)) Then
						Return -1
					End If
					colIndex += 1
					ci = TryCast(ViewInfo.ColumnsInfo.Columns(colIndex + showIndicatorIndexOffset), ColumnInfo)
					If colIndex - 1 < ViewInfo.ColumnsInfo.Columns.Count - 1 Then
						rect = Rectangle.Intersect(ci.Bounds, visibleColPanelBounds)
					Else
						rect = If(UseArrows, nextR2, nextR)
					End If
				End If
			End If
			colRect = TreeList.RectangleToScreen(rect)
			Dim columnsCount As Integer = TreeList.VisibleColumns.Count
			If ci.Type = ColumnInfo.ColumnInfoType.ColumnBlank OrElse colIndex > columnsCount - 1 Then
				Return columnsCount
			End If
			columnsCount = CalcFixedColumnsCount(FixedStyle.Left)
			If lastVisbleLeft AndAlso colIndex > columnsCount - 1 Then
				Return columnsCount
			End If
			columnsCount = TreeList.VisibleColumns.Count - CalcFixedColumnsCount(FixedStyle.Right)
			If lastVisibleRight AndAlso colIndex > columnsCount - 1 Then
				Return columnsCount
			End If
			If ci.Column Is Nothing Then
				Return -1
			End If
			Return ci.Column.VisibleIndex
		End Function

		Private Sub MoveDragColumnToCustomizationForm()
			If SourceBandInfo Is Nothing Then
				If SourceInfo.Column.VisibleIndex = -1 Then
					Return
				End If
				If SourceInfo.Column.OptionsColumn.AllowMoveToCustomizationForm Then
					SetDragColumnVisibleIndex(-1)
				End If
			Else
				SourceBandInfo.Band.Visible = False
			End If
		End Sub

		Private Sub SetDragColumnVisibleIndex(ByVal value As Integer)
			SourceInfo.Column.VisibleIndex = value
		End Sub

		Protected Function IsInCustomizationZone(ByVal pt As Point) As Boolean
			If pt.X < ViewInfo.ViewRects.ColumnPanel.Left OrElse pt.X > ViewInfo.ViewRects.ColumnPanel.Right Then
				Return False
			End If
			Return (pt.Y > ViewInfo.ViewRects.ColumnPanel.Bottom) AndAlso (pt.Y < TreeList.Bounds.Bottom)
		End Function

		Protected Sub DrawReversibleFrame(ByVal rect As Rectangle)
			If UseArrows Then
				Return
			End If
			If NativeVista.IsVista AndAlso (Not NativeVista.IsCompositionEnabled()) Then
				Return
			End If
			rect.Location = TreeList.PointToClient(rect.Location)
			DevExpress.XtraEditors.Drawing.SplitterLineHelper.Default.DrawReversibleFrame(TreeList.Handle, rect)
		End Sub

		Protected Function CalcFixedColumnsCount(ByVal fixedStyle As FixedStyle) As Integer
			Dim count As Integer = 0
			For Each column As TreeListColumn In TreeList.VisibleColumns
				If column.Fixed = fixedStyle Then
					count += 1
				End If
			Next column
			Return count
		End Function

		Protected Function IsBeforeFixedRight(ByVal column As TreeListColumn) As Boolean
			If column.Fixed <> FixedStyle.None Then
				Return False
			End If
			Dim index As Integer = column.VisibleIndex
'INSTANT VB TODO TASK: Assignments within expressions are not supported in VB.NET
'ORIGINAL LINE: if (++index > TreeList.VisibleColumns.Count - 1)
			If ++index > TreeList.VisibleColumns.Count - 1 Then
				Return False
			End If
            If TreeList.VisibleColumns(index).Equals(ViewInfo.FixedRightColumn) Then
                Return True
            End If
            Return False
		End Function

		Protected Function NextRectContains(ByVal bounds As Rectangle, ByVal pt As Point) As Boolean
			bounds.Height += IncreasedColumnHeight
			Return bounds.Contains(pt)
		End Function

		Protected Overridable Function IsDragPositionValid(ByVal index As Integer, ByVal ht As HitInfoType, ByVal ci As ColumnInfo) As Boolean
			If ci.Column IsNot Nothing Then
				Dim bi As MyTreeListBandInfo = ColumnInfoToBandInfo(ci)
                Dim inColumn As Boolean = ht = HitInfoType.Column
				If bi IsNot Nothing Then
                    Return (ht = HitInfoType.Column AndAlso bi.Band.ThisContainColumn(SourceInfo.Column) AndAlso bi.Band.ThisContainColumn(ci.Column))
				End If
				Return inColumn
			End If
			Return False
		End Function
	End Class
End Namespace