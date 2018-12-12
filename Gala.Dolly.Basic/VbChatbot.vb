Imports Galatea.Speech
Imports Gala.Dolly.UI
Imports Galatea
Imports Galatea.Runtime
Imports Gala.Dolly.Chatbots.Properties
Imports SpeechLib

<CLSCompliant(False)>
Public Class VbChatbot
    Inherits System.Windows.Forms.UserControl
    Implements Galatea.IProvider, IConsole

    Protected WithEvents tts5 As SpeechLib.SpVoice
    Protected speakFlags As SpeechLib.SpeechVoiceSpeakFlags
    Protected phonemes As Galatea.Speech.PhonemeCollection

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        btnMicOn.Image = Properties.Resources.mic_on
        btnMicOff.Image = Properties.Resources.mic_off
    End Sub

    Protected Sub VbChatbot_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If DesignMode Then Exit Sub

        ' Initialize TTS
        tts5 = New SpeechLib.SpVoice()

        speakFlags = SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync _
            Or SpeechLib.SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak _
            Or SpeechLib.SpeechVoiceSpeakFlags.SVSFIsXML

        phonemes = Galatea.Speech.Phonemes.GetPhonemesSapi5

        'Dim substitutions = New System.Collections.Specialized.StringCollection()
        ''substitutions.Add("I ,eye ")
        'substitutions.Add(".,.  ")
        'substitutions.Add("Ayuh,If you say so")
        'substitutionsList = Speech.Substitutions.FromSettings(substitutions)

        ' TODO:  Fix this shit
        'substitutionsList = Speech.Substitutions.FromSettings(Properties.Settings.Default.ChatbotSubstitutions)

        If Not (Program.Engine Is Nothing) Then

            ' Initialize Runtime
            ChatbotManager1 = Gala.Dolly.Chatbots.ChatbotManager.GetChatbots(Program.Engine.User)

            TextToSpeech.Rate = Gala.Dolly.Properties.Settings.Default.TtsRate
            TextToSpeech.Volume = Gala.Dolly.Properties.Settings.Default.TtsVolume

            tts5.Rate = TextToSpeech.Rate
            tts5.Volume = TextToSpeech.Volume

            Dim voices As ISpeechObjectTokens
            voices = tts5.GetVoices()
            Dim voiceName = voices.Item(1).GetDescription()     ' Microsoft Zira Mobile
            tts5.Voice = voices.Item(1)

            cbChatbotOnly.Checked = Properties.Settings.Default.LanguageModeChatbotOnly
        End If

        TextToSpeech.SetLEDsOff()

        ' Start Speech Recognition
        StartMic()

        ' Set Chatbot to Alice
        radALICE.Checked = True
    End Sub

    Protected Sub VbChatbot_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

        ' Save Settings
        Properties.Settings.Default.LanguageModeChatbotOnly = cbChatbotOnly.Checked
    End Sub

    Protected Sub cbChatbotOnly_CheckedChanged(sender As Object, e As EventArgs) Handles cbChatbotOnly.CheckedChanged

        If Not (Program.Engine Is Nothing) Then
            Program.Engine.AI.LanguageModel.Enabled = Not cbChatbotOnly.Checked
        End If
    End Sub

    Protected Sub GetResponse()

        ' Get and display input 
        inputText = txtInput.Text.Trim()

        ' Get Response
        If Not String.IsNullOrEmpty(inputText) Then

            Dim msg As String = String.Format(Settings.Default.ChatbotMessageFormat,
                                              Program.Engine.User.Name.ToUpper,
                                              inputText)

            txtDisplay.AppendText(msg & vbCrLf)

            ' Save input to short term UI History
            history.Add(inputText)
            historyLine += 1

            ' Parse Commands
            If inputText.Length > 6 Then
                If inputText.Substring(0, 6) = "SPEAK:" Then responseText = inputText.Substring(6)
            End If

            If String.IsNullOrEmpty(responseText) Then

                ' Get AI response to input
                responseText = Program.Engine.ExecutiveFunctions.GetResponse(Program.Engine.AI.LanguageModel, inputText, Program.Engine.User.FriendlyName)
            End If

            ' Display response
            SendResponse()
        End If
    End Sub

    Protected Sub SendResponse()

        If responseText.Contains("No match found") Then Exit Sub

        'LunaPOC.SerialInterface.Wait(240)     ' Don't talk over the Human!

        Dim msg As String = String.Format(Chatbots.Properties.Settings.Default.ChatbotMessageFormat,
                                          Program.Engine.AI.LanguageModel.ChatbotManager.Current.FriendlyName.ToUpper(),
                                          responseText)

        txtDisplay.AppendText(msg & vbCrLf)


        '' Substitute words for non-native English Speaking voice bots
        'globalizedResponseText = responseText

        'For Each substitutionPair In substitutionsList
        '    globalizedResponseText = globalizedResponseText.Replace(substitutionPair.ValueToReplace, substitutionPair.ReplaceWithValue)
        'Next

        ' Pause the microphone before speaking
        PauseMic()

        ' ''LunaPOC.SerialInterface.Wait(240)     ' Don't talk over the Human!
        ''timerAction = "speak"
        ''resumeMicTimer.Interval = Program.Engine.Machine.SerialPortController.WaitInterval
        ''resumeMicTimer.Start()

        ' just speak the text with the given flags
        tts5.Speak(responseText, speakFlags)

        ' Set new Response
        responseText = Nothing
    End Sub

    Public Property ChatBotButtonsVisible As Boolean
        Get
            Return radALICE.Visible And radELIZA.Visible And radDefault.Visible
        End Get
        Set(value As Boolean)
            radALICE.Visible = value
            radELIZA.Visible = value
            radDefault.Visible = value
            lblChatBotSelector.Visible = value
        End Set
    End Property

    Public ReadOnly Property ProviderId As String Implements IProvider.ProviderId
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public ReadOnly Property ProviderName As String Implements IProvider.ProviderName
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property Inputting() As Boolean
        Get
            Return _inputting
        End Get
        Set(ByVal value As Boolean)
            _inputting = value
        End Set
    End Property


    Private _isSilent As Boolean
    Private _inputting As Boolean

    Public Sub SendResponse(response As String) Implements IConsole.SendResponse

        responseText = response
        SendResponse()
    End Sub

    Public Property IsSilent As Boolean Implements IConsole.IsSilent
        Get
            Return _isSilent
        End Get
        Set(value As Boolean)
            _isSilent = value
        End Set
    End Property

#Region "Private"

    Private Sub VbChatbot_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize

        txtInput.Width = pnlInput.ClientSize.Width - Panel2.ClientSize.Width
    End Sub

    Private Sub radALICE_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radALICE.CheckedChanged
        If Not radALICE.Checked Then Exit Sub

        ' Set Engine ChatBot to Alice
        Dim alice As IChatbot = ChatbotManager1("Alice")
        responseText = Galatea.Globalization.ChatbotResources.ChatBotAliceGreeting
        SendResponse()

        ' Disable the Default 
        radDefault.Enabled = False

        ' Focus Input
        txtInput.Focus()
    End Sub

    Private Sub radELIZA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radELIZA.CheckedChanged
        If Not radELIZA.Checked Then Exit Sub

        ' Set Engine ChatBot to Eliza
        Program.Engine.AI.LanguageModel.ChatbotManager.Current = ChatbotManager1("Eliza")
        responseText = Galatea.Globalization.ChatbotResources.ChatBotElizaGreeting
        SendResponse()

        ' Disable the Default 
        radDefault.Enabled = False

        ' Focus Input
        txtInput.Focus()
    End Sub

    Private Sub txtInput_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtInput.KeyDown
        ' No Data
        If history.Count = 0 Then Exit Sub

        If (e.KeyCode = Keys.Up) Then  ' Key Up

            ' Go to the last line entered on initial Key Up
            If (historyLine = 0) Then historyLine = history.Count

            ' Go to the Previous Line
            historyLine -= 1

            ' Set the input
            txtInput.Text = history(historyLine)
            'txtInput.Select(txtInput.Text.Length - 1, 1)

        ElseIf (e.KeyCode = Keys.Down) Then     ' Key Down

            '  Go to the first line entered on initial Key Down
            If (historyLine = history.Count) Then historyLine = -1

            ' Go to the Next Line
            historyLine += 1

            Try

                ' Set the input
                txtInput.Text = history(historyLine)
                'txtInput.Select(txtInput.Text.Length - 1, 1)

            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtInput_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtInput.KeyPress

        If e.KeyChar = Chr(13) Then GetResponse()
    End Sub

    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSend.Click

        GetResponse()
    End Sub

    Private Sub btnMicOn_Click(sender As Object, e As EventArgs) Handles btnMicOn.Click

        StartMic()
    End Sub

    Private Sub btnMicOff_Click(sender As Object, e As EventArgs) Handles btnMicOff.Click

        StopMic()
    End Sub

#End Region

#Region "Text to Speech"

    Private Sub tts5_Word(ByVal StreamNumber As Integer, ByVal StreamPosition As Object, ByVal CharacterPosition As Integer, ByVal Length As Integer) Handles tts5.Word
        'txtInput.Text = responseText
        'txtInput.Select(CharacterPosition, Length)

        ''Debug.Write(" WORD:" & txtInput.SelectedText & " ")
    End Sub

    Private Sub tts5_Viseme(ByVal StreamNumber As Integer, ByVal StreamPosition As Object, ByVal Duration As Integer, ByVal NextVisemeId As SpeechVisemeType, ByVal Feature As SpeechVisemeFeature, ByVal CurrentVisemeId As SpeechVisemeType) Handles tts5.Viseme
        Dim viseme As Short = CurrentVisemeId

        Dim mouthPosition = phonemes(viseme).MouthPosition

        Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Diagnostic, mouthPosition)
        TextToSpeech.SetMouthPosition(mouthPosition)

        'Debug.Write(" Viseme:" & CurrentVisemeId & " ")
        'Debug.Write(" Mouth Position:" & mouthPosition.ToString & " ")
    End Sub

    Private Sub tts5_EndStream(ByVal StreamNumber As Integer, ByVal StreamPosition As Object) Handles tts5.EndStream

        txtInput.Clear()

        ' Speech Harness for Errors
        With Program.Engine.Debugger

            If .Exception IsNot Nothing Then

                responseText = .ErrorMessage
                SendResponse()

                .ClearError()
            End If
        End With

        ' resume mic
        If Not speechRecognitionStopped Then ResumeMic()
    End Sub

    Private Sub TextToSpeech_RateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextToSpeech.RateChanged
        tts5.Rate = TextToSpeech.Rate
        Gala.Dolly.Properties.Settings.Default.TtsRate = TextToSpeech.Rate
        Gala.Dolly.Properties.Settings.Default.Save()
    End Sub

    Private Sub TextToSpeech_VolumeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextToSpeech.VolumeChanged
        tts5.Volume = TextToSpeech.Volume
        Gala.Dolly.Properties.Settings.Default.TtsVolume = TextToSpeech.Volume
        Gala.Dolly.Properties.Settings.Default.Save()
    End Sub

    Protected inputText As String
    Protected responseText As String
    Protected globalizedResponseText As String
    Protected history As List(Of String) = New List(Of String)
    Protected historyLine As Integer = -1
    'Protected substitutionsList As Galatea.Speech.Substitutions
#End Region

#Region "Speech Recognition"

    Protected WithEvents RecoContext As SpSharedRecoContext
    Protected dictationGrammar As ISpeechRecoGrammar
    Protected chatbotGrammar As ISpeechRecoGrammar
    Protected galateaGrammar As ISpeechRecoGrammar
    Protected chatbotGrammarRuleName As String = "Gala.Dolly.Chatbot"
    Protected speechRecognitionText As String
    Protected speechRecognitionStopped As Boolean = True
    Protected speechRecognitionPaused As Boolean = False

    ' http://www.dreamincode.net/forums/topic/34209-simple-speech-dictation-in-vbnet/

    Private Sub StartMic()

        'First check to see if reco has been loaded before. If not lets load it.
        If (RecoContext Is Nothing) Then

            If Not Program.Engine Is Nothing Then

                Try
                    RecoContext = New SpSharedRecoContextClass  'Create a new Reco Context Class

                    ' Dictation Grammar
                    dictationGrammar = RecoContext.CreateGrammar(1)      'Setup the Grammar
                    dictationGrammar.DictationLoad()                     'Load the Grammar

                    ' Chatbot Grammar (from file)
                    chatbotGrammar = RecoContext.CreateGrammar(2)
                    'chatbotGrammar.LoadChatbotRules(Program.Engine.Chatbot.FriendlyName, chatbotGrammarRuleName)
                    chatbotGrammar.CmdLoadFromFile(Properties.Settings.Default.SpeechRecogChatbotGrammarRuleFile)

                    ' Galatea Algorithm Grammar
                    galateaGrammar = RecoContext.CreateGrammar(3)
                    galateaGrammar.CmdLoadFromFile(Properties.Settings.Default.SpeechRecogEvaluateGrammarRuleFile)

                Catch ex As Exception

                    Program.BaseForm.UIDebugger.Log(Diagnostics.DebuggerLogLevel.Error, Gala.Dolly.Properties.Resources.SpeechRecognitionNotLoaded, True)
                    Exit Sub
                End Try
            End If
        End If

        ResumeMic()
        speechRecognitionStopped = False
    End Sub

    Private Sub PauseMic()

        If speechRecognitionStopped Or speechRecognitionPaused Then Exit Sub

        speechRecognitionPaused = True
        dictationGrammar.DictationSetState(SpeechRuleState.SGDSInactive) 'Turns off the Recognition. It will go dormant.
        chatbotGrammar.CmdSetRuleState("", SpeechRuleState.SGDSInactive)
        galateaGrammar.CmdSetRuleState("", SpeechRuleState.SGDSInactive)

        btnMicOn.Enabled = False
        btnMicOn.Visible = True
        btnMicOff.Visible = False
    End Sub

    Private Sub ResumeMic()

        speechRecognitionPaused = False
        dictationGrammar.DictationSetState(SpeechRuleState.SGDSActive)   'Turns on the Recognition. This is Vitally important
        chatbotGrammar.CmdSetRuleState("", SpeechRuleState.SGDSActive)
        galateaGrammar.CmdSetRuleState("", SpeechRuleState.SGDSActive)

        'This is so the user doesn't break the program by
        'trying to start the recognition after its already started.
        btnMicOn.Visible = False
        btnMicOff.Visible = True
    End Sub

    Private Sub StopMic()

        speechRecognitionStopped = True
        dictationGrammar.DictationSetState(SpeechRuleState.SGDSInactive) 'Turns off the Recognition. It will go dormant.
        chatbotGrammar.CmdSetRuleState("", SpeechRuleState.SGDSInactive)
        galateaGrammar.CmdSetRuleState("", SpeechRuleState.SGDSInactive)

        'Again This is so the user doesn't go breaking things accidentlykj
        btnMicOn.Enabled = True
        btnMicOn.Visible = True
        btnMicOff.Visible = False
    End Sub

    ' This function handles Recognition event from the reco context object.
    ' Recognition event is fired when the speech recognition engines recognizes
    ' a sequences of words.
    Private Sub RecoContext_Recognition(ByVal StreamNumber As Integer, ByVal StreamPosition As Object, ByVal RecognitionType As SpeechLib.SpeechRecognitionType, ByVal Result As SpeechLib.ISpeechRecoResult) Handles RecoContext.Recognition

        speechRecognitionText = Result.PhraseInfo.GetText

        Dim ruleId = Result.PhraseInfo.Rule.Id
        Dim ruleName = Result.PhraseInfo.Rule.Name

        ' Evaluate Rule Event
        If ruleId = Properties.Settings.Default.SpeechRecogEvaluateGrammarRuleID Then
            RaiseEvent SpeechRecognition(Me, EventArgs.Empty)
        End If

        '#Region "From Gala Dolly POC"  
        '' Display the text, even if it's crap
        'With Me.txtSpeechRecog
        '    .Visible = True
        '    .Focus()
        '    .Text = _strtext
        'End With

        '' Don't execute if it's crap
        'If _strtext = "Hi" Then Exit Sub

        ''UPGRADE_WARNING: Couldn't resolve default property of object StreamPosition. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        'Debugger.LogEvent("Speech Recognition: ### Recognition ### '" & _strtext & "', " & StreamNumber & ", " & StreamPosition)

        '' Determine the Rule Id and Command Type
        '_ruleid = Result.PhraseInfo.Rule.Id
        'Me.Execute()
        '#End Region

        ' Get ChatBot Response!!
        txtInput.Text = speechRecognitionText
        GetResponse()
    End Sub

    Friend Event SpeechRecognition As EventHandler
#End Region

End Class
