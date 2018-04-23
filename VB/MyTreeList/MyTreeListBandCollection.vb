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
Imports System.Collections
Imports DevExpress.XtraTreeList.Columns

Namespace MyXtraTreeList
	Public Class MyTreeListBandCollection
		Inherits CollectionBase
		Private fRootBand As MyTreeListBand

		Private fTreeListBands As MyTreeListBandCollection

		Public Sub New(ByVal RootBand As MyTreeListBand, ByVal TreeListBands As MyTreeListBandCollection)
			fRootBand = RootBand
			fTreeListBands = TreeListBands
		End Sub

		Public Sub New(ByVal treeList As MyTreeList)
			fRootBand = New MyTreeListBand(treeList, Nothing, 0, treeList.Columns.Count, String.Empty)
			List.Add(fRootBand)
			fTreeListBands = Nothing
		End Sub

		Default Public Overridable ReadOnly Property Item(ByVal index As Integer) As MyTreeListBand
			Get
				Return CType(List(index), MyTreeListBand)
			End Get
		End Property

		Protected Friend Overridable Function Add(ByVal Band As MyTreeListBand) As MyTreeListBand
			If MyTreeListBand.IsValid(Band) Then
				If fTreeListBands IsNot Nothing Then
					For Each Child As MyTreeListBand In Band.Children
						fTreeListBands.Add(Child)
					Next Child
				End If
				If Band.Parent IsNot Nothing Then
					For i As Integer = Band.Position To Band.EndPosition
						Band.Parent.SetColumn(i, Band.Parent.Columns(i))
					Next i
				End If
				Return Me(List.Add(Band))
			End If
			Return Nothing
		End Function

		Public Overridable Function Add(ByVal Index As Integer, ByVal Width As Integer, ByVal Name As String) As MyTreeListBand
			Dim Res As MyTreeListBand = fRootBand.CreateChild(Index, Width, Name)
			Return Add(Res)
		End Function

		Public Overridable Sub Remove(ByVal Band As MyTreeListBand)
			For Each Child As MyTreeListBand In Band.Children
				Remove(Child)
			Next Child
			Band.Children.Clear()
			If Band.Parent IsNot Nothing Then
				Band.Parent.Children.Remove(Band)
				For i As Integer = 0 To Band.Width - 1
					Band.Parent.SetColumn(i + Band.Position, Band.Columns(i))
				Next i
			End If
			Remove(Band)
		End Sub

		Public Overridable Function Contains(ByVal Band As MyTreeListBand) As Boolean
			Return List.Contains(Band)
		End Function

		Public Overridable Shadows Sub Clear()
			List.Clear()
		End Sub

		Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
			MyBase.OnInsert(index, value)
			If (fTreeListBands IsNot Nothing) AndAlso (value IsNot Nothing) AndAlso (Not fTreeListBands.Contains(TryCast(value, MyTreeListBand))) Then
				fTreeListBands.Add(TryCast(value, MyTreeListBand))
			End If
		End Sub

		Protected Overrides Sub OnRemove(ByVal index As Integer, ByVal value As Object)
			MyBase.OnRemove(index, value)
			If fTreeListBands IsNot Nothing Then
				fTreeListBands.Remove(TryCast(value, MyTreeListBand))
			End If
		End Sub

		Public Overridable Function FindAtHandle(ByVal Handle As Integer) As MyTreeListBand
			For Each Band As MyTreeListBand In Me
				If Band.ContainsHandle(Handle) Then
					Return Band
				End If
			Next Band
			Return Nothing
		End Function

		Public Overridable Function IsBrotherColumns(ByVal ColA As TreeListColumn, ByVal ColB As TreeListColumn) As Boolean
			For Each Band As MyTreeListBand In Me
				If Band.ThisContainColumn(ColA) AndAlso Band.ThisContainColumn(ColB) Then
					Return True
				End If
			Next Band
			Return False
		End Function

		Protected Friend Sub UpdateColumns()
			For i As Integer = 0 To fRootBand.Width - 1
				Dim Col As TreeListColumn = fRootBand.GetColumn(i)
				If Col IsNot Nothing Then
					Col.VisibleIndex = If((Col.Visible), fRootBand.IndexToColumnHandle(i), -1)
				End If
			Next i
		End Sub

		Public Function GetColumnParentBand(ByVal Column As TreeListColumn) As MyTreeListBand
			If Column Is Nothing Then
				Return Nothing
			End If
			For Each Child As MyTreeListBand In Me
				If Child.BandColumn Is Column Then
					Return Child
				End If
				For Each Col As TreeListColumn In Child.Columns
					If Col Is Column Then
						Return Child
					End If
				Next Col
			Next Child
			Return Nothing
		End Function
	End Class
End Namespace