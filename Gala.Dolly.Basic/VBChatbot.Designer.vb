<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class VBChatbot
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Me.lblChatBotSelector = New System.Windows.Forms.Label()
        Me.radALICE = New System.Windows.Forms.RadioButton()
        Me.radELIZA = New System.Windows.Forms.RadioButton()
        Me.radDefault = New System.Windows.Forms.RadioButton()
        Me.txtDisplay = New System.Windows.Forms.TextBox()
        Me.pnlChatBotSelector = New System.Windows.Forms.Panel()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.resumeMicTimer = New System.Windows.Forms.Timer(Me.components)
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.pnlInput = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnMicOn = New System.Windows.Forms.Button()
        Me.btnMicOff = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TextToSpeech = New Gala.Dolly.UI.TTS()
        Me.cbChatbotOnly = New System.Windows.Forms.CheckBox()
        Me.pnlChatBotSelector.SuspendLayout()
        Me.pnlInput.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblChatBotSelector
        '
        Me.lblChatBotSelector.AutoSize = True
        Me.lblChatBotSelector.Location = New System.Drawing.Point(3, 5)
        Me.lblChatBotSelector.Name = "lblChatBotSelector"
        Me.lblChatBotSelector.Size = New System.Drawing.Size(74, 13)
        Me.lblChatBotSelector.TabIndex = 4
        Me.lblChatBotSelector.Text = "CHAT MODE:"
        '
        'radALICE
        '
        Me.radALICE.AutoSize = True
        Me.radALICE.Location = New System.Drawing.Point(80, 3)
        Me.radALICE.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.radALICE.Name = "radALICE"
        Me.radALICE.Size = New System.Drawing.Size(55, 17)
        Me.radALICE.TabIndex = 5
        Me.radALICE.Text = "ALICE"
        Me.radALICE.UseVisualStyleBackColor = True
        '
        'radELIZA
        '
        Me.radELIZA.AutoSize = True
        Me.radELIZA.Location = New System.Drawing.Point(135, 3)
        Me.radELIZA.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.radELIZA.Name = "radELIZA"
        Me.radELIZA.Size = New System.Drawing.Size(55, 17)
        Me.radELIZA.TabIndex = 6
        Me.radELIZA.Text = "ELIZA"
        Me.radELIZA.UseVisualStyleBackColor = True
        '
        'radDefault
        '
        Me.radDefault.AutoSize = True
        Me.radDefault.Checked = True
        Me.radDefault.Location = New System.Drawing.Point(190, 3)
        Me.radDefault.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.radDefault.Name = "radDefault"
        Me.radDefault.Size = New System.Drawing.Size(74, 17)
        Me.radDefault.TabIndex = 7
        Me.radDefault.TabStop = True
        Me.radDefault.Text = "DEFAULT"
        Me.radDefault.UseVisualStyleBackColor = True
        '
        'txtDisplay
        '
        Me.txtDisplay.BackColor = System.Drawing.SystemColors.Window
        Me.txtDisplay.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtDisplay.Location = New System.Drawing.Point(0, 0)
        Me.txtDisplay.Multiline = True
        Me.txtDisplay.Name = "txtDisplay"
        Me.txtDisplay.ReadOnly = True
        Me.txtDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDisplay.Size = New System.Drawing.Size(558, 177)
        Me.txtDisplay.TabIndex = 11
        Me.txtDisplay.TabStop = False
        '
        'pnlChatBotSelector
        '
        Me.pnlChatBotSelector.AutoSize = True
        Me.pnlChatBotSelector.Controls.Add(Me.cbChatbotOnly)
        Me.pnlChatBotSelector.Controls.Add(Me.lblChatBotSelector)
        Me.pnlChatBotSelector.Controls.Add(Me.radALICE)
        Me.pnlChatBotSelector.Controls.Add(Me.radELIZA)
        Me.pnlChatBotSelector.Controls.Add(Me.radDefault)
        Me.pnlChatBotSelector.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlChatBotSelector.Location = New System.Drawing.Point(0, 0)
        Me.pnlChatBotSelector.Name = "pnlChatBotSelector"
        Me.pnlChatBotSelector.Size = New System.Drawing.Size(800, 23)
        Me.pnlChatBotSelector.TabIndex = 10
        '
        'btnSend
        '
        Me.btnSend.Location = New System.Drawing.Point(4, 13)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(64, 23)
        Me.btnSend.TabIndex = 2
        Me.btnSend.Text = "Send"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'resumeMicTimer
        '
        '
        'txtInput
        '
        Me.txtInput.Location = New System.Drawing.Point(0, 15)
        Me.txtInput.Margin = New System.Windows.Forms.Padding(0)
        Me.txtInput.Multiline = True
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(458, 20)
        Me.txtInput.TabIndex = 1
        '
        'pnlInput
        '
        Me.pnlInput.AutoSize = True
        Me.pnlInput.Controls.Add(Me.Panel1)
        Me.pnlInput.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlInput.Location = New System.Drawing.Point(0, 177)
        Me.pnlInput.Name = "pnlInput"
        Me.pnlInput.Size = New System.Drawing.Size(558, 40)
        Me.pnlInput.TabIndex = 9
        Me.pnlInput.TabStop = True
        '
        'Panel1
        '
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.txtInput)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.MinimumSize = New System.Drawing.Size(0, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(558, 40)
        Me.Panel1.TabIndex = 13
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.btnMicOn)
        Me.Panel2.Controls.Add(Me.btnMicOff)
        Me.Panel2.Controls.Add(Me.btnSend)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(458, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(100, 40)
        Me.Panel2.TabIndex = 13
        '
        'btnMicOn
        '
        Me.btnMicOn.Location = New System.Drawing.Point(72, 7)
        Me.btnMicOn.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.btnMicOn.Name = "btnMicOn"
        Me.btnMicOn.Size = New System.Drawing.Size(28, 29)
        Me.btnMicOn.TabIndex = 34
        Me.btnMicOn.TabStop = False
        Me.btnMicOn.UseVisualStyleBackColor = True
        '
        'btnMicOff
        '
        Me.btnMicOff.Location = New System.Drawing.Point(72, 7)
        Me.btnMicOff.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.btnMicOff.Name = "btnMicOff"
        Me.btnMicOff.Size = New System.Drawing.Size(28, 29)
        Me.btnMicOff.TabIndex = 35
        Me.btnMicOff.TabStop = False
        Me.btnMicOff.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 23)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtDisplay)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlInput)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextToSpeech)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 217)
        Me.SplitContainer1.SplitterDistance = 558
        Me.SplitContainer1.TabIndex = 0
        '
        'TextToSpeech
        '
        Me.TextToSpeech.AutoSize = True
        Me.TextToSpeech.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TextToSpeech.Dock = System.Windows.Forms.DockStyle.Right
        Me.TextToSpeech.EyeCamColor = Gala.Dolly.UI.EyeCamColor.Grey
        Me.TextToSpeech.LEDColor = Gala.Dolly.UI.LEDColor.Green
        Me.TextToSpeech.Location = New System.Drawing.Point(0, 0)
        Me.TextToSpeech.Margin = New System.Windows.Forms.Padding(18, 3, 3, 3)
        Me.TextToSpeech.Name = "TextToSpeech"
        Me.TextToSpeech.Rate = 0
        Me.TextToSpeech.Size = New System.Drawing.Size(238, 217)
        Me.TextToSpeech.TabIndex = 11
        Me.TextToSpeech.Volume = 50
        '
        'cbChatbotOnly
        '
        Me.cbChatbotOnly.AutoSize = True
        Me.cbChatbotOnly.Location = New System.Drawing.Point(305, 3)
        Me.cbChatbotOnly.Name = "cbChatbotOnly"
        Me.cbChatbotOnly.Size = New System.Drawing.Size(87, 17)
        Me.cbChatbotOnly.TabIndex = 8
        Me.cbChatbotOnly.Text = "Chatbot Only"
        Me.cbChatbotOnly.UseVisualStyleBackColor = True
        '
        'VbChatbot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.pnlChatBotSelector)
        Me.MaximumSize = New System.Drawing.Size(800, 240)
        Me.MinimumSize = New System.Drawing.Size(800, 240)
        Me.Name = "VbChatbot"
        Me.Size = New System.Drawing.Size(800, 240)
        Me.pnlChatBotSelector.ResumeLayout(False)
        Me.pnlChatBotSelector.PerformLayout()
        Me.pnlInput.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblChatBotSelector As Label
    Friend WithEvents radALICE As RadioButton
    Friend WithEvents radELIZA As RadioButton
    Friend WithEvents radDefault As RadioButton
    Friend WithEvents txtDisplay As TextBox
    Friend WithEvents pnlChatBotSelector As Panel
    Friend WithEvents btnSend As Button
    Friend WithEvents resumeMicTimer As Timer
    Friend WithEvents txtInput As TextBox
    Friend WithEvents pnlInput As Panel
    Friend WithEvents ChatbotManager1 As Gala.Dolly.UI.ChatbotManager
    Friend WithEvents SplitContainer1 As SplitContainer
    Private WithEvents TextToSpeech As UI.TTS
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnMicOn As Button
    Friend WithEvents btnMicOff As Button
    Friend WithEvents cbChatbotOnly As CheckBox
End Class
