Imports Galatea
Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Sub Application_Startup(sender As Object, e As EventArgs) Handles MyBase.Startup

            Dim form1 As Gala.Dolly.UI.BaseForm = Forms().Form1
            form1.UIDebugger.FileLogger = New UI.Diagnostics.FileLogger()
            form1.UIDebugger.FileLogger.StartLogging(Properties.Settings.Default.LogFileName, System.IO.FileMode.Append)

            Gala.Dolly.Program.Startup()

            My.Forms.Form1.UIDebugger.ShowAlerts = Gala.Dolly.UI.Properties.Settings.Default.DebuggerShowAlerts
            Program.Engine.Machine.SerialPortController.DisableWarning = Gala.Dolly.Properties.Settings.Default.SerialPortDisableWarning
        End Sub

        Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles MyBase.Shutdown

            Gala.Dolly.UI.Properties.Settings.Default.DebuggerShowAlerts = My.Forms.Form1.UIDebugger.ShowAlerts
            Gala.Dolly.Properties.Settings.Default.SerialPortDisableWarning = Program.Engine.Machine.SerialPortController.DisableWarning
            Gala.Dolly.Program.Shutdown()
        End Sub

        Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException

            If TypeOf e.Exception Is TeaException Then

                Program.Engine.Debugger.HandleTeaException(e.Exception)
            Else

                Program.Engine.Debugger.ThrowSystemException(e.Exception)
            End If
        End Sub

        Public Sub ShutdownDumbass()

            'If Gala.Dolly.Program.Started Then

            '    Gala.Dolly.Program.Shutdown()
            'End If
        End Sub

    End Class
End Namespace
