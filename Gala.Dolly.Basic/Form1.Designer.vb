<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits Gala.Dolly.UI.BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.visionCapture = New Gala.Dolly.UI.VisionCapture()
        Me.gbSerialInterface = New System.Windows.Forms.GroupBox()
        Me.serialInterface1 = New Gala.Dolly.UI.SerialInterface()
        Me.chatBotControl = New Gala.Dolly.Basic.VbChatbot()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.gbSerialInterface.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.splitContainer1.Name = "splitContainer1"
        Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.visionCapture)
        Me.splitContainer1.Panel1.Controls.Add(Me.gbSerialInterface)
        Me.splitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(3)
        Me.splitContainer1.Panel1MinSize = 0
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.AutoScroll = True
        Me.splitContainer1.Panel2.Controls.Add(Me.chatBotControl)
        Me.splitContainer1.Panel2.Padding = New System.Windows.Forms.Padding(6)
        Me.splitContainer1.Panel2MinSize = 120
        Me.splitContainer1.Size = New System.Drawing.Size(784, 537)
        Me.splitContainer1.SplitterDistance = 299
        Me.splitContainer1.SplitterWidth = 1
        Me.splitContainer1.TabIndex = 18
        '
        'visionCapture
        '
        Me.visionCapture.AutoSize = True
        Me.visionCapture.Dock = System.Windows.Forms.DockStyle.Fill
        Me.visionCapture.Location = New System.Drawing.Point(3, 3)
        Me.visionCapture.MinimumSize = New System.Drawing.Size(480, 210)
        Me.visionCapture.Name = "visionCapture"
        Me.visionCapture.Offset = New System.Drawing.Point(24, 64)
        Me.visionCapture.PanMax = 120
        Me.visionCapture.PanMin = 30
        Me.visionCapture.Size = New System.Drawing.Size(778, 210)
        Me.visionCapture.TabIndex = 3
        Me.visionCapture.TiltMax = 105
        Me.visionCapture.TiltMin = 45
        '
        'gbSerialInterface
        '
        Me.gbSerialInterface.Controls.Add(Me.serialInterface1)
        Me.gbSerialInterface.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbSerialInterface.Location = New System.Drawing.Point(3, 198)
        Me.gbSerialInterface.Margin = New System.Windows.Forms.Padding(0)
        Me.gbSerialInterface.Name = "gbSerialInterface"
        Me.gbSerialInterface.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.gbSerialInterface.Size = New System.Drawing.Size(778, 98)
        Me.gbSerialInterface.TabIndex = 4
        Me.gbSerialInterface.TabStop = False
        Me.gbSerialInterface.Text = "Serial Interface"
        '
        'serialInterface1
        '
        Me.serialInterface1.AutoSize = True
        Me.serialInterface1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.serialInterface1.Location = New System.Drawing.Point(0, 20)
        Me.serialInterface1.Margin = New System.Windows.Forms.Padding(0)
        Me.serialInterface1.MinimumSize = New System.Drawing.Size(320, 0)
        Me.serialInterface1.Name = "serialInterface1"
        Me.serialInterface1.Padding = New System.Windows.Forms.Padding(14, 7, 0, 7)
        Me.serialInterface1.Size = New System.Drawing.Size(320, 76)
        Me.serialInterface1.TabIndex = 2
        '
        'chatBotControl
        '
        Me.chatBotControl.AutoSize = True
        Me.chatBotControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.chatBotControl.ChatBotButtonsVisible = True
        Me.chatBotControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chatBotControl.Inputting = False
        Me.chatBotControl.Location = New System.Drawing.Point(6, 6)
        Me.chatBotControl.MaximumSize = New System.Drawing.Size(0, 200)
        Me.chatBotControl.MinimumSize = New System.Drawing.Size(0, 200)
        Me.chatBotControl.Name = "chatBotControl"
        Me.chatBotControl.Size = New System.Drawing.Size(772, 200)
        Me.chatBotControl.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.splitContainer1)
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Controls.SetChildIndex(Me.splitContainer1, 0)
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel1.PerformLayout()
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.Panel2.PerformLayout()
        Me.splitContainer1.ResumeLayout(False)
        Me.gbSerialInterface.ResumeLayout(False)
        Me.gbSerialInterface.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents splitContainer1 As SplitContainer
    Private WithEvents chatBotControl As VbChatbot
    Private WithEvents serialInterface1 As UI.SerialInterface
    Friend WithEvents gbSerialInterface As GroupBox
    Private WithEvents visionCapture As UI.VisionCapture
End Class
