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
Imports DevExpress.XtraTreeList.Columns

Namespace MyXtraTreeList
	Public Class MyTreeListBand
		Private fTreeList As MyTreeList

		Private fParent As MyTreeListBand

		Private fChildren As MyTreeListBandCollection

		Private fPosition As Integer

		Private fLevel As Integer

		Private fName As String

		Private fBandColumn As TreeListColumn

		Private fColumns() As TreeListColumn

		Private fVisible As Boolean

		Public Sub New(ByVal TreeList As MyTreeList, ByVal parent As MyTreeListBand, ByVal position As Integer, ByVal width As Integer, ByVal name As String)
			fTreeList = TreeList
			fChildren = New MyTreeListBandCollection(Me, TreeList.Bands)
			fColumns = New TreeListColumn(width - 1){}
			fParent = parent
			fName = name
			fPosition = position
			fLevel = If((Me.Parent Is Nothing), 0, Me.Parent.Level + 1)
			fBandColumn = Nothing
			fVisible = True
		End Sub

		Public Shared Function IsValid(ByVal Band As MyTreeListBand) As Boolean
			If (Band Is Nothing) OrElse (Band.Position < 0) OrElse (Band.Width < 1) Then
				Return False
			End If
			If Band.Parent IsNot Nothing Then
				For Each Child As MyTreeListBand In Band.Parent.Children
					If Child IsNot Band Then 'Intersection check
						If Child.Position > Band.Position Then
							If Band.EndPosition >= Child.Position Then
								Return False
							End If
						Else
							If Child.EndPosition >= Band.Position Then
								Return False
							End If
						End If
					End If
				Next Child
			End If
			Return True
		End Function

		Public Overridable Property Position() As Integer
			Get
				Return fPosition
			End Get
			Protected Set(ByVal value As Integer)
				fPosition = value
			End Set
		End Property

		Public Overridable Property Level() As Integer
			Get
				Return fLevel
			End Get
			Protected Set(ByVal value As Integer)
				fLevel = value
			End Set
		End Property

		Public Overridable ReadOnly Property Width() As Integer
			Get
				Return fColumns.GetLength(0)
			End Get
		End Property

		Public ReadOnly Property EndPosition() As Integer
			Get
				Return Position + Width - 1
			End Get
		End Property

		Public ReadOnly Property TreeList() As MyTreeList
			Get
				Return fTreeList
			End Get
		End Property

		Public Overridable ReadOnly Property Parent() As MyTreeListBand
			Get
				Return fParent
			End Get
		End Property

		Public Overridable ReadOnly Property Children() As MyTreeListBandCollection
			Get
				Return fChildren
			End Get
		End Property

		Public Overridable ReadOnly Property Columns() As TreeListColumn()
			Get
				Return fColumns
			End Get
		End Property

		Public Overridable Property BandColumn() As TreeListColumn
			Get
				Return fBandColumn
			End Get
			Set(ByVal value As TreeListColumn)
				fBandColumn = value
				MinimizeColumnWidth(fBandColumn)
			End Set
		End Property

		Public Overridable Property Name() As String
			Get
				Return fName
			End Get
			Set(ByVal value As String)
				fName = value
			End Set
		End Property

		Public Property Visible() As Boolean
			Get
				Return fVisible
			End Get
			Set(ByVal value As Boolean)
				If fVisible <> value Then
					fVisible = value
					If BandColumn IsNot Nothing Then
						BandColumn.Visible = value
					End If
					For Each Child As MyTreeListBand In Children
						Child.Visible = value
					Next Child
					For Each ChildCol As TreeListColumn In Columns
						If ChildCol IsNot Nothing Then
							ChildCol.Visible = value
						End If
					Next ChildCol
					If Parent IsNot Nothing Then
						Parent.Children.UpdateColumns()
					End If
				End If
			End Set
		End Property

		Public Function IndexToColumnHandle(ByVal Index As Integer) As Integer
			If Parent Is Nothing Then
				Return Position + Index
			End If
			Return Parent.IndexToColumnHandle(Position) + Index
		End Function

		Public Function ColumnHandleToIndex(ByVal handle As Integer) As Integer
			If Parent Is Nothing Then
				Return handle - Position
			End If
			Return Parent.ColumnHandleToIndex(handle) - Position
		End Function

		Public Function ContainsHandle(ByVal Handle As Integer) As Boolean
			Return (Handle >= IndexToColumnHandle(0)) AndAlso (Handle < IndexToColumnHandle(Width))
		End Function

		Public Function ContainsColumn(ByVal Column As TreeListColumn) As Boolean
			If BandColumn Is Column Then
				Return True
			End If
			For Each Col As TreeListColumn In Columns
				If Col Is Column Then
					Return True
				End If
			Next Col
			For Each Child As MyTreeListBand In Children
				If Child.ContainsColumn(Column) Then
					Return True
				End If
			Next Child
			Return False
		End Function

		Public Function ThisContainColumn(ByVal Column As TreeListColumn) As Boolean
			If Column Is Nothing Then
				Return False
			End If
			For Each ChildCol As TreeListColumn In Columns
				If ChildCol Is Column Then
					Return True
				End If
			Next ChildCol
			Return False
		End Function

		Public Function CreateChild(ByVal Position As Integer, ByVal ChildWidth As Integer, ByVal ChildName As String) As MyTreeListBand
			Dim Res As New MyTreeListBand(fTreeList, Me, Position, ChildWidth, ChildName)
			Return Children.Add(Res)
		End Function

		Private Function ResizeColumnsArray(ByVal Width As Integer) As Boolean
			If Width > 0 Then
				Dim Tmp(Width - 1) As TreeListColumn
				fColumns.CopyTo(Tmp, 0)
				fColumns = Tmp
				Return True
			End If
			Return False
		End Function

		Public Function SetWidth(ByVal NewWidth As Integer) As Boolean
			If (Parent Is Nothing) AndAlso (NewWidth > Width) Then
				TreeList.ViewInfo.CalcColumnTotalWidth()
				Return ResizeColumnsArray(NewWidth)
			End If
			Return False
		End Function

		Public Sub SetColumn(ByVal Index As Integer, ByVal Column As TreeListColumn)
			If (Column IsNot Nothing) AndAlso (Index >= 0) AndAlso (Index < Width) Then
				Dim Handle As Integer = IndexToColumnHandle(Index)
				Dim Child As MyTreeListBand = Children.FindAtHandle(Handle)
				If Child Is Nothing Then
					Columns(Index) = Column
					Column.VisibleIndex = Handle
				Else
					Child.SetColumn(Child.ColumnHandleToIndex(Handle), Column)
					Columns(Index) = Nothing
				End If
			End If
		End Sub

		Public Function GetColumn(ByVal Index As Integer) As TreeListColumn
			If (Index >= 0) AndAlso (Index < Width) Then
				Dim Handle As Integer = IndexToColumnHandle(Index)
				Dim Child As MyTreeListBand = Children.FindAtHandle(Handle)
				If Child Is Nothing Then
					Return Columns(Index)
				Else
					Return Child.GetColumn(Child.ColumnHandleToIndex(Handle))
				End If
			End If
			Return Nothing
		End Function

		Public Overridable Function GetColumnIndex(ByVal column As TreeListColumn) As Integer
			Dim Res As Integer = -1
			For i As Integer = 0 To Width - 1
				If Columns(i) Is column Then
					Return i
				End If
			Next i
			For Each Child As MyTreeListBand In Children
				Res = Child.GetColumnIndex(column)
				If Res <> -1 Then
					Return Res + Child.Position
				End If
			Next Child
			Return -1
		End Function

		Public Overridable Function IsBrother(ByVal Band As MyTreeListBand) As Boolean
			If Band Is Nothing Then
				Return False
			End If
			Return Band.Parent Is Parent
		End Function

		Public Overridable Function IsBrother(ByVal Column As TreeListColumn) As Boolean
			If Parent Is Nothing Then
				Return False
			End If
			Return Parent.ThisContainColumn(Column)
		End Function

		Public Overridable Function TryToSwap(ByVal A As MyTreeListBand, ByVal B As MyTreeListBand) As Boolean
			If (Me IsNot A.Parent) OrElse (Me IsNot B.Parent) Then
				Return False
			End If
			Dim Start As Integer = Math.Min(B.Position, A.Position)
			Dim [End] As Integer = Math.Max(B.Position, A.Position)
			If A.Position < B.Position Then
				B.Position = A.Position
				A.Position = B.Position + B.Width
			Else
				A.Position = B.Position
				B.Position = A.Position + A.Width
			End If
			Children.UpdateColumns()
			Return True
		End Function

		Protected Overridable Function TryToSwapBandColumns(ByVal A As TreeListColumn, ByVal B As TreeListColumn) As Boolean
			Dim AIndex As Integer = -1
			Dim BIndex As Integer = -1
			For i As Integer = 0 To Width - 1
				If Columns(i) Is A Then
					AIndex = i
				End If
				If Columns(i) Is B Then
					BIndex = i
				End If
			Next i
			If (AIndex >= 0) AndAlso (BIndex >= 0) Then
				fColumns(AIndex) = B
				fColumns(BIndex) = A
				If A.VisibleIndex > B.VisibleIndex Then
					A.VisibleIndex = IndexToColumnHandle(BIndex)
					B.VisibleIndex = IndexToColumnHandle(AIndex)
				Else
					B.VisibleIndex = IndexToColumnHandle(AIndex)
					A.VisibleIndex = IndexToColumnHandle(BIndex)
				End If
				Return True
			End If
			Return False
		End Function

		Public Function TryToSwapColumns(ByVal A As TreeListColumn, ByVal B As TreeListColumn) As Boolean
			If (A Is Nothing) OrElse (B Is Nothing) Then
				Return False
			End If
			If TryToSwapBandColumns(A, B) Then
				Return True
			End If
			For Each Child As MyTreeListBand In Children
				If Child.TryToSwapColumns(A, B) Then
					Return True
				End If
			Next Child
			Return False
		End Function

		Public Function TryToSwapBandAndColumn(ByVal Band As MyTreeListBand, ByVal Column As TreeListColumn) As Boolean
			If (Band Is Nothing) OrElse (Column Is Nothing) OrElse (Band.Parent IsNot Me) Then
				Return False
			End If
			Dim ColumnPosition As Integer = -1
			For i As Integer = 0 To Width - 1
				If Columns(i) Is Column Then
					ColumnPosition = i
				End If
			Next i
			If ColumnPosition < 0 Then
				Return False
			End If
			Dim Start As Integer = Math.Min(ColumnPosition, Band.Position)
			Dim [End] As Integer = Math.Max(ColumnPosition, Band.Position)
			Dim NewColumnPosition As Integer
			If Band.Position < ColumnPosition Then
				NewColumnPosition = Band.Position
				Band.Position = NewColumnPosition + 1
			Else
				Band.Position = ColumnPosition
				NewColumnPosition = Band.Position + Band.Width
			End If
			Columns(NewColumnPosition) = Columns(ColumnPosition)
			Columns(ColumnPosition) = Nothing
			Children.UpdateColumns()
			Return True
		End Function

		Public Function HasColumns() As Boolean
			For Each ChildCol As TreeListColumn In Columns
				If ChildCol IsNot Nothing Then
					Return True
				End If
			Next ChildCol
			Return False
		End Function

		Public Function GetDepth() As Integer
			If Children.Count = 0 Then
				Return If((HasColumns()), 1, 0)
			End If
			Dim max As Integer = 0
			For Each Child As MyTreeListBand In Children
				Dim ChildDepth As Integer = Child.GetDepth()
				If ChildDepth > max Then
					max = ChildDepth
				End If
			Next Child
			Return max + 1
		End Function

		Public Shared Sub MinimizeColumnWidth(ByVal Column As TreeListColumn)
			If Column IsNot Nothing Then
				Column.MinWidth = 0
				Column.Width = 0
				Column.OptionsColumn.FixedWidth = True
			End If
		End Sub
	End Class
End Namespace
