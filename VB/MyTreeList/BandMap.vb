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
	Public Class BandMap
		Protected Source(,) As BandMapCell

		Public Sub New()
			Source = New BandMapCell(,){}
		End Sub

		Public ReadOnly Property ColCount() As Integer
			Get
				Return Source.GetLength(0)
			End Get
		End Property

		Public ReadOnly Property LevelCount() As Integer
			Get
				Return Source.GetLength(1)
			End Get
		End Property

		Default Public ReadOnly Property Item(ByVal ColIndex As Integer, ByVal Level As Integer) As BandMapCell
			Get
				If (ColIndex < ColCount) AndAlso (Level < LevelCount) AndAlso (ColIndex >= 0) AndAlso (Level >= 0) Then
					Return Source(ColIndex, Level)
				End If
				Return Nothing
			End Get
		End Property

		Protected Sub FillSource(ByVal Band As MyTreeListBand, ByVal Offset As Integer, ByVal Level As Integer)
			Dim CellObj As Object = If((Band.BandColumn Is Nothing), CObj(Band), CObj(Band.BandColumn))
			For i As Integer = 0 To Band.Width - 1
				Source(Offset + i, Level) = New BandMapCell(CellObj, i = 0, i = (Band.Width - 1))
				If Band.Columns(i) IsNot Nothing Then
					Source(Offset + i, Level + 1) = New BandMapCell(Band.Columns(i), True, True)
				End If
			Next i
			For Each Child As MyTreeListBand In Band.Children
				FillSource(Child, Offset + Child.Position, Level + 1)
			Next Child
		End Sub

		Public Sub CalcBandMap(ByVal Band As MyTreeListBand)
			Source = New BandMapCell(Band.Width - 1, Band.GetDepth() - 1){}
			For i As Integer = 0 To Band.Width - 1
				If Band.Columns(i) IsNot Nothing Then
					Source(i, 0) = New BandMapCell(Band.Columns(i), True, True)
				End If
			Next i
			For Each Child As MyTreeListBand In Band.Children
				FillSource(Child, Child.Position, 0)
			Next Child
		End Sub

		Private Sub FillCellSource(ByVal Band As MyTreeListBand, ByVal Offset As Integer, ByVal Level As Integer)
			Dim NewLevel As Integer = If((Band.BandColumn Is Nothing), Level, Level + 1)
			For i As Integer = 0 To Band.Width - 1
				If Band.BandColumn IsNot Nothing Then
					Source(Offset + i, Level) = New BandMapCell(Band.BandColumn, i = 0, i = (Band.Width - 1))
				End If
				If Band.Columns(i) IsNot Nothing Then
					Source(Offset + i, NewLevel) = New BandMapCell(Band.Columns(i), True, True)
				End If
			Next i
			For Each Child As MyTreeListBand In Band.Children
				FillCellSource(Child, Offset + Child.Position, NewLevel)
			Next Child
		End Sub

		Public Sub CalcCellBandMap(ByVal Band As MyTreeListBand)
			Source = New BandMapCell(Band.Width - 1, Band.GetDepth() - 1){}
			For i As Integer = 0 To Band.Width - 1
				If Band.Columns(i) IsNot Nothing Then
					Source(i, 0) = New BandMapCell(Band.Columns(i), True, True)
				End If
			Next i
			For Each Child As MyTreeListBand In Band.Children
				FillCellSource(Child, Child.Position, 0)
			Next Child
			Dim EmptyLevelCount As Integer = 0
			For i As Integer = LevelCount - 1 To 0 Step -1
				Dim EmptyLevel As Boolean = True
				For j As Integer = 0 To ColCount - 1
					If Source(j, i) IsNot Nothing Then
						EmptyLevel = False
						Exit For
					End If
				Next j
				If EmptyLevel Then
					EmptyLevelCount += 1
				Else
					Exit For
				End If
			Next i
			If EmptyLevelCount > 0 Then
				Dim NewLevelCount As Integer = LevelCount - EmptyLevelCount
				Dim Temp(ColCount - 1, NewLevelCount - 1) As BandMapCell
				For i As Integer = 0 To ColCount - 1
					For j As Integer = 0 To NewLevelCount - 1
						Temp(i, j) = Source(i, j)
					Next j
				Next i
				Source = Temp
			End If
		End Sub

		Public Function GetColumnStartPosition(ByVal Column As TreeListColumn, <System.Runtime.InteropServices.Out()> ByRef ColIndex As Integer, <System.Runtime.InteropServices.Out()> ByRef Level As Integer) As Boolean
			ColIndex = -1
			Level = -1
			If Column Is Nothing Then
				Return False
			End If
			For i As Integer = 0 To ColCount - 1
				For j As Integer = 0 To LevelCount - 1
                    If Source(i, j) is Nothing  Then
                        Return False 
                    End If
					If (Source(i, j).Column Is Column) AndAlso (Source(i, j).LeftBorder) Then
						ColIndex = i
						Level = j
						Return True
					End If
				Next j
			Next i
			Return False
		End Function
	End Class

	Public Enum BandMapType
		Band
		Column
	End Enum


	Public Class BandMapCell
		Private fType As BandMapType

		Private fObject As Object

		Private fLeftBoreder As Boolean

		Private fRightBorder As Boolean

		Public Sub New(ByVal Obj As Object, ByVal Left As Boolean, ByVal Right As Boolean)
			If TypeOf Obj Is MyTreeListBand Then
				fType = BandMapType.Band
			End If
			If TypeOf Obj Is TreeListColumn Then
				fType = BandMapType.Column
			End If
			fObject = Obj
			fLeftBoreder = Left
			fRightBorder = Right
		End Sub

		Public ReadOnly Property Type() As BandMapType
			Get
				Return fType
			End Get
		End Property

		Public ReadOnly Property Band() As MyTreeListBand
			Get
				Return TryCast(fObject, MyTreeListBand)
			End Get
		End Property

		Public ReadOnly Property Column() As TreeListColumn
			Get
				Return TryCast(fObject, TreeListColumn)
			End Get
		End Property

		Public ReadOnly Property LeftBorder() As Boolean
			Get
				Return fLeftBoreder
			End Get
		End Property

		Public ReadOnly Property RightBorder() As Boolean
			Get
				Return fRightBorder
			End Get
		End Property
	End Class
End Namespace
