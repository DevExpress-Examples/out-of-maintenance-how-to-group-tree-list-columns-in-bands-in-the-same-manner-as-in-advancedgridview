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
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.ViewInfo

Namespace MyXtraTreeList
	Public Class MyPositionInfo
		Inherits PositionInfo
		Private fInfo As ColumnInfo

		Public Sub New()
			MyBase.New()
			fInfo = Nothing
		End Sub

        Public Overloads Sub Assign(ByVal pos As MyPositionInfo)
            MyBase.Assign(pos)
            fInfo = pos.Info
        End Sub

        Public Overloads Sub Assign(ByVal newIndex As Integer, ByVal newBounds As Rectangle, ByVal valid As Boolean, ByVal info As ColumnInfo)
            MyBase.Assign(newIndex, newBounds, valid)
            fInfo = info
        End Sub

        Public ReadOnly Property Info() As ColumnInfo
            Get
                Return fInfo
            End Get
        End Property
    End Class
End Namespace
