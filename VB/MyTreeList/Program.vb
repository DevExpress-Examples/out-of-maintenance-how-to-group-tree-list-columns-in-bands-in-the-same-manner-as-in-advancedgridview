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
Imports System.Windows.Forms

Namespace MyXtraTreeList
	Friend NotInheritable Class Program
		''' <summary>
		''' The main entry point for the application.
		''' </summary>
		Private Sub New()
		End Sub
        <STAThread()> _
        Public Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New Form1())
        End Sub
	End Class
End Namespace
