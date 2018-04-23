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
Imports DevExpress.XtraTreeList.ViewInfo

Namespace MyXtraTreeList
	Public Class MyResourceInfo
		Inherits ResourceInfo
		Public Sub New(ByVal viewInfo As MyTreeListViewInfo)
			MyBase.New(viewInfo)
		End Sub

		Public Shadows Sub SetColumnPanelHeight(ByVal value As Integer)
			MyBase.SetColumnPanelHeight(value)
		End Sub

		Public Shadows Sub SetRowHeight(ByVal value As Integer)
			MyBase.SetRowHeight(value)
		End Sub
	End Class
End Namespace
