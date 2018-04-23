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
Imports DevExpress.XtraTreeList.Handler

Namespace MyXtraTreeList
	Public Class MyTreeListHandler
		Inherits TreeListHandler
		Public Sub New(ByVal treeList As MyTreeList)
			MyBase.New(treeList)
		End Sub

		Protected Overrides Function CreateState(ByVal state As TreeListState) As TreeListControlState
			Select Case state
				Case TreeListState.ColumnDragging
					Return New MyColumnDraggingState(Me)
			End Select
			Return MyBase.CreateState(state)
		End Function
	End Class
End Namespace
