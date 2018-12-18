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

            Dim uiSettings = Gala.Dolly.UI.Properties.Settings.Default
#Region "Temporary"
            ' TODO
            uiSettings.DebuggerLogLevel = Diagnostics.DebuggerLogLevel.Diagnostic
            uiSettings.DebuggerAlertLevel = Diagnostics.DebuggerLogLevel.Message
            uiSettings.Save()

            Dim imagingSettings = New AI.Imaging.ImagingSettings
            imagingSettings.Timeout = 2000
            imagingSettings.SuppressTimeout = True
            imagingSettings.ColorStatsSettings.StatisticalAnalysisTypes = Galatea.AI.Math.StatsTypes.Mean
            imagingSettings.MonochromeBlobFilterSettings.ContrastCorrectionFactor = 0
            imagingSettings.MonochromeBlobFilterSettings.AdaptiveSmoothingFactor = 0.25
            imagingSettings.MonochromeBlobFilterSettings.FrameWidth = 10
            imagingSettings.BlobPointSettings.LineSegmentThreshold = 20
            imagingSettings.BlobPointSettings.LineAngleThreshold = 10
            imagingSettings.BlobPointSettings.CurveAngleThreshold = 27

            imagingSettings.TemplateRecognitionSettings.ColorBrightnessThreshold = 5
            imagingSettings.TemplateRecognitionSettings.ColorSaturationThreshold = 5
            imagingSettings.TemplateRecognitionSettings.ShapeOblongThreshold = 1.75
            imagingSettings.TemplateRecognitionSettings.ShapeOblongRecognitionNormalization = True
            imagingSettings.TemplateRecognitionSettings.IdentifyShapeCertaintyMinimum = 65
            Properties.Settings.Default.ImagingSettings = imagingSettings

            Properties.Settings.Default.SpeechIsSilent = True
            Properties.Settings.Default.Save()
#End Region
            Diagnostics.DebuggerLogLevelSettings.Initialize(uiSettings.DebuggerLogLevel, uiSettings.DebuggerAlertLevel)

            Dim form1 As Gala.Dolly.UI.BaseForm = Forms().Form1
            form1.UIDebugger.FileLogger = New UI.Diagnostics.FileLogger()
            form1.UIDebugger.FileLogger.StartLogging(Properties.Settings.Default.LogFileName, System.IO.FileMode.Append)
            form1.UIDebugger.ShowAlerts = Gala.Dolly.UI.Properties.Settings.Default.DebuggerShowAlerts

            Gala.Dolly.Program.Startup()
            If Not Gala.Dolly.Program.Started Then Exit Sub

            SmartEngine.AI.LanguageModel.SpeechModule.StaySilent = Gala.Dolly.Properties.Settings.Default.SpeechIsSilent
            Program.Engine.Machine.SerialPortController.DisableWarning = Gala.Dolly.Properties.Settings.Default.SerialPortDisableWarning
        End Sub

        Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles MyBase.Shutdown

            MessageBox.Show("Bye!")
        End Sub

        Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException

            If TypeOf e.Exception Is TeaException Then

                Program.Engine.Debugger.HandleTeaException(e.Exception, Nothing)
            Else

                Program.Engine.Debugger.ThrowSystemException(e.Exception, Nothing)
            End If
        End Sub

        Public Sub ShutdownDumbass()

            'If Gala.Dolly.Program.Started Then

            '    Gala.Dolly.Program.Shutdown()
            'End If
        End Sub

    End Class
End Namespace
