Imports System.ComponentModel
Imports Gala.Dolly.UI
Imports Galatea
Imports Galatea.Speech

<CLSCompliant(False)>
Public Class Form1
    Inherits Gala.Dolly.UI.BaseForm
    Implements Galatea.IProvider

    Private WithEvents languageModel As Galatea.AI.ILanguageAnalyzer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Debugging
        Me.Console = Me.chatBotControl
        Program.BaseForm = Me
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UIDebugger.SpeechMenuEnabled = False
        If Not Program.Started Or Program.Engine Is Nothing Then Exit Sub

        ' Runtime
        chatBotControl.Select()

        ' More stuff to do
        viewSerialInterfaceMenuItem = serialInterface1.viewSerialInterfaceMenuItem
        ViewSerialInterface_Click(Nothing, Nothing)

        languageModel = Program.Engine.AI.LanguageModel

        '' Handle Recognition Event
        'If Program.Started And Program.Engine IsNot Nothing Then

        '    Program.Engine.AI.RecognitionModel.r
        'End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

        If Not (Program.Engine Is Nothing) Then

            ' Save other settings
            Gala.Dolly.Properties.Settings.Default.SerialPortDisableWarning = Program.Engine.Machine.SerialPortController.DisableWarning
        End If

        ' Shut down Program and Galatea Runtime Engine
        Gala.Dolly.Program.Shutdown()

        ' Seriously quit the fucking thing
        Application.Exit()
    End Sub


    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize

        'ResizeTemplateRecognition()
        ResizeChatBot()
        'visionCapture.InitialResize()
        ' Position Controls
        gbSerialInterface.Top = splitContainer1.Panel1.ClientSize.Height - gbSerialInterface.Height
    End Sub

    'Private Sub ResizeTemplateRecognition()

    '    ' Get Image Display container size
    '    Dim containerSize As System.Drawing.Size = splitContainer1.Panel1.ClientSize

    '    ' Get the maximum available Height and Width 
    '    Dim maxHeight As Integer = containerSize.Height - (templateRecognition.display.Margin.Top + templateRecognition.display.Margin.Bottom)
    '    Dim maxWidth As Integer = containerSize.Width - (templateRecognition.display.Margin.Left + templateRecognition.display.Margin.Right)

    '    ' Determine the relative Width of the maximum Height
    '    Dim relativeWidth As Integer = CInt(maxHeight * 4 / 3)

    '    If (relativeWidth < maxWidth) Then

    '        templateRecognition.display.Height = maxHeight
    '        templateRecognition.display.Width = relativeWidth
    '    Else
    '        templateRecognition.display.Height = CInt(maxWidth * 3 / 4)
    '        templateRecognition.display.Width = maxWidth
    '    End If
    'End Sub

    Private Sub ResizeChatBot()

        Dim heightAdds = chatBotControl.Margin.Top + chatBotControl.Margin.Bottom +
                            splitContainer1.Margin.Top + splitContainer1.Margin.Bottom +
                            splitContainer1.Panel2.Margin.Top + splitContainer1.Panel1.Margin.Bottom +
                            splitContainer1.Panel2.Padding.Top + splitContainer1.Panel2.Padding.Bottom

        ' ChatBot maximum size
        Dim maxHeight = chatBotControl.MaximumSize.Height + heightAdds

        If (splitContainer1.Height - splitContainer1.SplitterDistance > maxHeight) Then
            splitContainer1.SplitterDistance = splitContainer1.Height - maxHeight
        End If

        ' ChatBot minimum size
        Dim minHeight = chatBotControl.MinimumSize.Height + heightAdds

        If (Not splitContainer1.Height = 0 And splitContainer1.Height - splitContainer1.SplitterDistance < minHeight) Then
            splitContainer1.SplitterDistance = splitContainer1.Height - minHeight
        End If
    End Sub


    Private Sub languageModel_Responding(sender As Object, e As Galatea.IO.RespondingEventArgs) Handles languageModel.Responding

        If visionCapture.StaticMode Then Exit Sub ' Only send for new images

        Dim image As Bitmap = visionCapture.GetLastFrame()
        Dim stream As Galatea.Imaging.IO.ImagingContextStream = Galatea.Imaging.IO.ImagingContextStream.FromImage(image)

        ' Send image capture to AI Task Manager
        Program.Engine.ExecutiveFunctions.StreamContext(Me, Program.Engine.Vision.ImageAnalyzer,
                                                        Galatea.IO.ContextType.Machine, Galatea.IO.InputType.Visual, stream,
                                                        image.GetType())
    End Sub


    Private Sub ViewSerialInterface_Click(ByVal sender As Object, ByVal e As EventArgs) Handles viewSerialInterfaceMenuItem.Click

        gbSerialInterface.Visible = viewSerialInterfaceMenuItem.Checked
    End Sub

    'Private Sub templateRecognition_TemplateLoaded(sender As Object, e As EventArgs)

    '    chatBotControl.Select()
    'End Sub

    Private WithEvents viewSerialInterfaceMenuItem As ToolStripMenuItem

    Public ReadOnly Property ProviderID As String Implements IProvider.ProviderID
        Get
            Return "Gala.Dolly.Basic.SpeechRecognition"
        End Get
    End Property

    Public ReadOnly Property ProviderName As String Implements IProvider.ProviderName
        Get
            Return "SpeechRecognition"
        End Get
    End Property
End Class
