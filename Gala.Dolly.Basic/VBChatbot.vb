Imports System.Globalization
Imports Galatea.Speech
Imports Gala.Dolly.Chatbots.Properties
Imports Gala.Dolly.UI
Imports Galatea
Imports Galatea.Runtime
Imports SpeechLib

<CLSCompliant(False)>
Public Class VBChatbot
    Inherits System.Windows.Forms.UserControl
    Implements Galatea.IProvider, IConsole

    Protected WithEvents Tts5 As SpeechLib.SpVoice
    Private speakFlags As SpeechLib.SpeechVoiceSpeakFlags
    Private phonemes As Galatea.Speech.PhonemeCollection

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        btnMicOn.Image = Properties.Resources.mic_on
        btnMicOff.Image = Properties.Resources.mic_off

        ' IProvider
        Dim t = Me.GetType()
        _providerId = t.FullName
        _providerName = t.Name
    End Sub

    Protected Sub VBChatbot_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If DesignMode Then Exit Sub

        ' Initialize TTS
        Tts5 = New SpeechLib.SpVoice()

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

            Tts5.Rate = TextToSpeech.Rate
            Tts5.Volume = TextToSpeech.Volume

            Dim voices As ISpeechObjectTokens
            voices = Tts5.GetVoices()
            'Dim voiceName = voices.Item(1).GetDescription()     ' Microsoft Zira Mobile
            Tts5.Voice = voices.Item(1)

            cbChatbotOnly.Checked = Properties.Settings.Default.LanguageModeChatbotOnly
        End If

        TextToSpeech.SetLEDsOff()

        ' Start Speech Recognition
        StartMic()

        ' Set Chatbot to Alice
        radALICE.Checked = True
    End Sub

    Protected Sub VBChatbot_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed

        ' Save Settings
        Properties.Settings.Default.LanguageModeChatbotOnly = cbChatbotOnly.Checked
    End Sub

    Protected Sub CBChatbotOnly_CheckedChanged(sender As Object, e As EventArgs) Handles cbChatbotOnly.CheckedChanged

        If Not (Program.Engine Is Nothing) Then
            SmartEngine.AI.LanguageModel.Enabled = Not cbChatbotOnly.Checked
        End If
    End Sub

    Protected Sub GetResponse()

        ' Get and display input 
        inputText = txtInput.Text.Trim()

        ' Get Response
        If Not String.IsNullOrEmpty(inputText) Then

            Dim msg As String = String.Format(CultureInfo.CurrentCulture, Settings.Default.ChatbotMessageFormat,
                                              Program.Engine.User.Name.ToUpper(CultureInfo.CurrentCulture), inputText)

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
                responseText = Program.Engine.ExecutiveFunctions.GetResponse(SmartEngine.AI.LanguageModel, inputText, Program.Engine.User.FriendlyName)
            End If

            ' Display response
            SendResponse()
        End If
    End Sub

    Protected Sub SendResponse()

        If responseText.Contains("No match found") Then Exit Sub

        'LunaPOC.SerialInterface.Wait(240)     ' Don't talk over the Human!

        Dim msg As String = String.Format(CultureInfo.CurrentCulture, Settings.Default.ChatbotMessageFormat,
                                          SmartEngine.AI.LanguageModel.ChatbotManager.Current.FriendlyName.ToUpper(CultureInfo.CurrentCulture),
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
        Tts5.Speak(responseText, speakFlags)

        ' Set new Response
        responseText = Nothing
    End Sub

    Public Property ChatbotButtonsVisible As Boolean
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
            Return _providerId
        End Get
    End Property

    Public ReadOnly Property ProviderName As String Implements IProvider.ProviderName
        Get
            Return _providerName
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

    Private Sub RadALICE_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radALICE.CheckedChanged
        If Not radALICE.Checked Then Exit Sub

        ' Set Engine ChatBot to Alice
        'Dim alice As IChatbot = ChatbotManager1("Alice")
        responseText = Galatea.Globalization.ChatbotResources.ChatbotAliceGreeting
        SendResponse()

        ' Disable the Default 
        radDefault.Enabled = False

        ' Focus Input
        txtInput.Focus()
    End Sub

    Private Sub RadELIZA_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radELIZA.CheckedChanged
        If Not radELIZA.Checked Then Exit Sub

        ' Set Engine ChatBot to Eliza
        SmartEngine.AI.LanguageModel.ChatbotManager.Current = ChatbotManager1("Eliza")
        responseText = Galatea.Globalization.ChatbotResources.ChatbotElizaGreeting
        SendResponse()

        ' Disable the Default 
        radDefault.Enabled = False

        ' Focus Input
        txtInput.Focus()
    End Sub

    Private Sub TxtInput_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtInput.KeyDown
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

    Private Sub TxtInput_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtInput.KeyPress

        If e.KeyChar = Chr(13) Then GetResponse()
    End Sub

    Private Sub BtnSend_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSend.Click

        GetResponse()
    End Sub

    Private Sub BtnMicOn_Click(sender As Object, e As EventArgs) Handles btnMicOn.Click

        StartMic()
    End Sub

    Private Sub BtnMicOff_Click(sender As Object, e As EventArgs) Handles btnMicOff.Click

        StopMic()
    End Sub

#End Region

#Region "Text to Speech"

    Private Sub Tts5_Word(ByVal StreamNumber As Integer, ByVal StreamPosition As Object, ByVal CharacterPosition As Integer, ByVal Length As Integer) Handles Tts5.Word
        'txtInput.Text = responseText
        'txtInput.Select(CharacterPosition, Length)

        ''Debug.Write(" WORD:" & txtInput.SelectedText & " ")
    End Sub

    Private Sub Tts5_Viseme(ByVal StreamNumber As Integer, ByVal StreamPosition As Object, ByVal Duration As Integer, ByVal NextVisemeId As SpeechVisemeType, ByVal Feature As SpeechVisemeFeature, ByVal CurrentVisemeId As SpeechVisemeType) Handles Tts5.Viseme
        Dim viseme As Short = CurrentVisemeId

        Dim mouthPosition = phonemes(viseme).MouthPosition

        Program.Engine.Debugger.Log(Galatea.Diagnostics.DebuggerLogLevel.Diagnostic, mouthPosition)
        TextToSpeech.SetMouthPosition(mouthPosition)

        'Debug.Write(" Viseme:" & CurrentVisemeId & " ")
        'Debug.Write(" Mouth Position:" & mouthPosition.ToString & " ")
    End Sub

    Private Sub Tts5_EndStream(ByVal StreamNumber As Integer, ByVal StreamPosition As Object) Handles Tts5.EndStream

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
        Tts5.Rate = TextToSpeech.Rate
        Gala.Dolly.Properties.Settings.Default.TtsRate = TextToSpeech.Rate
        Gala.Dolly.Properties.Settings.Default.Save()
    End Sub

    Private Sub TextToSpeech_VolumeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextToSpeech.VolumeChanged
        Tts5.Volume = TextToSpeech.Volume
        Gala.Dolly.Properties.Settings.Default.TtsVolume = TextToSpeech.Volume
        Gala.Dolly.Properties.Settings.Default.Save()
    End Sub

    Private inputText As String
    Private responseText As String
    'Private globalizedResponseText As String
    Private history As List(Of String) = New List(Of String)
    Private historyLine As Integer = -1
    'Protected substitutionsList As Galatea.Speech.Substitutions
#End Region

#Region "Speech Recognition"

    Protected WithEvents RecognitionContext As SpSharedRecoContext
    Private dictationGrammar As ISpeechRecoGrammar
    Private chatbotGrammar As ISpeechRecoGrammar
    Private galateaGrammar As ISpeechRecoGrammar
    'Private chatbotGrammarRuleName As String = "Gala.Dolly.Chatbot"
    Private speechRecognitionText As String
    Private speechRecognitionStopped As Boolean = True
    Private speechRecognitionPaused As Boolean = False

    ' http://www.dreamincode.net/forums/topic/34209-simple-speech-dictation-in-vbnet/

    Private Sub StartMic()

        'First check to see if reco has been loaded before. If not lets load it.
        If (RecognitionContext Is Nothing) Then

            If Not Program.Engine Is Nothing Then

                Try
                    RecognitionContext = New SpSharedRecoContextClass  'Create a new Reco Context Class

                    ' Dictation Grammar
                    dictationGrammar = RecognitionContext.CreateGrammar(1)      'Setup the Grammar
                    dictationGrammar.DictationLoad()                     'Load the Grammar

                    ' Chatbot Grammar (from file)
                    chatbotGrammar = RecognitionContext.CreateGrammar(2)
                    'chatbotGrammar.LoadChatbotRules(Program.Engine.Chatbot.FriendlyName, chatbotGrammarRuleName)
                    chatbotGrammar.CmdLoadFromFile(Properties.Settings.Default.SpeechRecogChatbotGrammarRuleFile)

                    ' Galatea Algorithm Grammar
                    galateaGrammar = RecognitionContext.CreateGrammar(3)
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
    Private Sub RecoContext_Recognition(ByVal StreamNumber As Integer, ByVal StreamPosition As Object,
                                        ByVal RecognitionType As SpeechLib.SpeechRecognitionType,
                                        ByVal Result As SpeechLib.ISpeechRecoResult) Handles RecognitionContext.Recognition

        speechRecognitionText = Result.PhraseInfo.GetText

        Dim ruleId = Result.PhraseInfo.Rule.Id
        'Dim ruleName = Result.PhraseInfo.Rule.Name

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

    Private _isSilent As Boolean
    Private _inputting As Boolean
    Private ReadOnly _providerId, _providerName As String
End Class
