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
Imports DevExpress.XtraTreeList.ViewInfo
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Columns
Imports DevExpress.Utils.Drawing

Namespace MyXtraTreeList
	Public Class MyTreeListViewInfo
		Inherits TreeListViewInfo
		Private fBandMaxlevel As Integer

		Private fBandHeight As Integer

		Private fRowBandHeight As Integer

		Private fRowLevelCount As Integer

		Private fColCount As Integer

		Protected Friend BandLinks As Dictionary(Of TreeListColumn, MyTreeListBandInfo)

		Protected ColumnPanelBandMap As BandMap

		Protected CellBandMap As BandMap

		Public Sub New(ByVal treeList As MyTreeList)
			MyBase.New(treeList)
			ColumnPanelBandMap = New BandMap()
			CellBandMap = New BandMap()
			BandLinks = New Dictionary(Of TreeListColumn, MyTreeListBandInfo)()
		End Sub

		Public Shadows ReadOnly Property TreeList() As MyTreeList
			Get
				Return TryCast(MyBase.TreeList, MyTreeList)
			End Get
		End Property

		Public Shadows ReadOnly Property RC() As MyResourceInfo
			Get
				Return TryCast(MyBase.RC, MyResourceInfo)
			End Get
		End Property

		Protected Overrides Function CreateResourceCache() As ResourceInfo
			Return New MyResourceInfo(Me)
		End Function

		Public Overridable Property BandMaxLevel() As Integer
			Get
				Return fBandMaxlevel
			End Get
			Protected Set(ByVal value As Integer)
				fBandMaxlevel = value
			End Set
		End Property

		Public Overridable Property BandHeight() As Integer
			Get
				Return fBandHeight
			End Get
			Protected Set(ByVal value As Integer)
				fBandHeight = value
			End Set
		End Property

		Public Overridable Property RowBandHeight() As Integer
			Get
				Return fRowBandHeight
			End Get
			Protected Set(ByVal value As Integer)
				fRowBandHeight = value
			End Set
		End Property

		Public Overridable Property RowLevelCount() As Integer
			Get
				Return fRowLevelCount
			End Get
			Protected Set(ByVal value As Integer)
				fRowLevelCount = value
			End Set
		End Property

		Public Overridable Property ColCount() As Integer
			Get
				Return fColCount
			End Get
			Protected Set(ByVal value As Integer)
				fColCount = value
			End Set
		End Property

		Protected Overridable Sub CalcMaxBandLevel()
			Dim max As Integer = 0
			Dim HasColumns As Boolean = False
			For Each B As MyTreeListBand In TreeList.Bands
				If B.Level > max Then
					max = B.Level
					HasColumns = B.HasColumns()
				End If
			Next B
			If HasColumns Then
				max += 1
			End If
			BandMaxLevel = max
		End Sub

		Protected Overrides Sub CalcColumnPanelHeight()
			MyBase.CalcColumnPanelHeight()
			BandHeight = ColumnPanelHeight
			If ColumnPanelHeight <> 0 Then
				CalcMaxBandLevel()
				RC.SetColumnPanelHeight(BandHeight * BandMaxLevel)
			End If
		End Sub

		Protected Overrides Sub CalcRowHeight()
			MyBase.CalcRowHeight()
			RowBandHeight = RC.RowHeight
			RC.SetRowHeight(RowBandHeight * CellBandMap.LevelCount)
		End Sub

		Protected Overridable Sub CalcBandsInfo()
			For Each B As MyTreeListBand In TreeList.Bands
				If (B.Level > 0) AndAlso B.Visible AndAlso B.BandColumn Is Nothing Then
					Dim bi As New MyTreeListBandInfo(B)
					bi.CalcBandInfo(Me)
					ColumnsInfo.Columns.Add(bi)
				End If
			Next B
		End Sub

		Public Overrides Sub CalcColumnsInfo()
			ColCount = 0
			BandLinks.Clear()
			MyBase.CalcColumnsInfo()
			CalcBandsInfo()
			ColumnPanelBandMap.CalcBandMap(TreeList.RootBand)
			CellBandMap.CalcCellBandMap(TreeList.RootBand)
		End Sub

		Public Overrides Sub CalcColumnInfo(ByVal ci As ColumnInfo, ByRef left As Integer, ByVal customization As Boolean)
			Dim offset As Integer = 0
			MyBase.CalcColumnInfo(ci, offset, customization)
            If ci.Type = ColumnInfo.ColumnInfoType.Column Then
                Dim ParentBand As MyTreeListBand = TreeList.Bands.GetColumnParentBand(ci.Column)
                If ParentBand Is Nothing Then
                    ci.Bounds = Rectangle.Empty
                    Return
                End If
                Dim bi As New MyTreeListBandInfo(ParentBand)
                bi.CalcBandInfo(Me)
                Dim VisiblePosition As Integer = ParentBand.GetColumnIndex(ci.Column)
                If ParentBand.BandColumn Is ci.Column Then
                    ci.Bounds = bi.Bounds
                    ci.CaptionRect = New Rectangle(left + 4, 0, bi.Bounds.Width, bi.Bounds.Height)
                    If BandLinks.ContainsKey(ci.Column) Then
                        BandLinks(ci.Column) = bi
                    Else
                        BandLinks.Add(ci.Column, bi)
                    End If
                Else
                    Dim X As Integer = bi.Bounds.X
                    For i As Integer = 0 To VisiblePosition - 1
                        Dim Col As TreeListColumn = ParentBand.GetColumn(i)
                        If (Col IsNot Nothing) AndAlso Col.Visible Then
                            X += Col.Width
                        End If
                    Next i
                    Dim Y As Integer = bi.Bounds.Bottom - 1
                    Dim Width As Integer = ci.Column.Width
                    Dim Height As Integer = BandHeight * (BandMaxLevel - ParentBand.Level)
                    ci.Bounds = New Rectangle(X, Y, Width, Height)
                    ci.CaptionRect = New Rectangle(left + 4, 0, Width, Height)
                    ColCount += 1
                    left += Width
                End If
            Else
                left += offset
            End If
			UpdateGlyphInfo(ci)
			ObjectPainter.CalcObjectBounds(GInfo.Graphics, TreeList.ElementsLookAndFeel.Painter.Header, ci)
		End Sub

		Protected Function GetColumnVisibleIndex(ByVal ColIndex As Integer) As Integer
			Dim VisibleColIndex As Integer = 0
			For i As Integer = 0 To ColIndex - 1
				Dim C As TreeListColumn = TreeList.RootBand.GetColumn(i)
				If (C IsNot Nothing) AndAlso (C.Visible) Then
					VisibleColIndex += 1
				End If
			Next i
			Return VisibleColIndex
		End Function

		Protected Overrides Sub CalcRowCellsInfo(ByVal ri As RowInfo, ByVal viewInfoList As System.Collections.ArrayList)
			MyBase.CalcRowCellsInfo(ri, viewInfoList)
			ri.Lines.Clear()
			For Each ci As CellInfo In ri.Cells
					Dim ColIndex, Level As Integer
				If CellBandMap.GetColumnStartPosition(ci.Column, ColIndex, Level) Then
					Dim X As Integer = ci.Bounds.X
					Dim Y As Integer = ci.Bounds.Y + RowBandHeight * Level
					Dim Height As Integer = RowBandHeight
					Dim Width As Integer = ci.Bounds.Width
					If (GetColumnVisibleIndex(ColIndex) = 0) AndAlso (ci.Column.VisibleIndex <> 0) Then
						Dim Offset As Integer = (ri.Level + 1) * RC.LevelWidth
						X += Offset
						Width -= Offset
					End If
					ci.EditorViewInfo.Bounds = New Rectangle(X, Y, Width, Height)
					UpdateEditorInfo(ci)
					UpdateCell(ci, ci.Column, ri.Node)
					ri.Lines.Add(New LineInfo(New Rectangle(X + Width, Y, 1, Height + 1), PaintAppearance.VertLine))
					ri.Lines.Add(New LineInfo(New Rectangle(X, Y + Height, Width + 1, 1), PaintAppearance.HorzLine))
				End If
			Next ci
		End Sub

		Protected Friend Overridable Function GetBandInfoByPoint(ByVal pt As Point) As MyTreeListBandInfo
			For Each ci As ColumnInfo In ColumnsInfo.Columns
				Dim bi As MyTreeListBandInfo = TryCast(ci, MyTreeListBandInfo)
				If (bi IsNot Nothing) AndAlso (bi.Bounds.Contains(pt)) Then
					Return bi
				End If
			Next ci
			Return Nothing
		End Function

		Public Overrides Function GetHitTest(ByVal pt As Point) As TreeListHitTest
			If ViewRects.ColumnPanel.Contains(pt) Then
				Dim bi As MyTreeListBandInfo = GetBandInfoByPoint(pt)
				If bi IsNot Nothing Then
					Dim ht As New TreeListHitTest()
					ht.ColumnInfo = bi
					ht.HitInfoType = HitInfoType.Column
					Return ht
				End If
			End If
			Return MyBase.GetHitTest(pt)
		End Function

		Protected Friend Shadows Function FindFixedLeftColumn() As TreeListColumn
			Return Me.FindFixedLeftColumn()
		End Function

		Public Overrides Function GetColumnLeftCoord(ByVal column As TreeListColumn) As Integer
			If column.VisibleIndex < 0 Then
				Return 0
			End If
			Dim res As Integer = 0
			If BandLinks.ContainsKey(column) Then
				Dim colWidth As Integer = 0
				For Each col As TreeListColumn In BandLinks(column).Band.Columns
					If col IsNot Nothing AndAlso col.Visible = True Then
						colWidth += col.VisibleWidth
					End If
				Next col
				column.Width = colWidth
				For Each bandLink As KeyValuePair(Of TreeListColumn, MyTreeListBandInfo) In BandLinks
					If bandLink.Key Is column Then
						Return res
					End If
					If bandLink.Value.Band.Level = BandLinks(column).Band.Level Then
						For Each col As TreeListColumn In bandLink.Value.Band.Columns
							If col.Visible = True Then
								res += col.VisibleWidth
							End If
						Next col
					End If
				Next bandLink
				Return res
			End If
			Return MyBase.GetColumnLeftCoord(column)
		End Function
	End Class
End Namespace
