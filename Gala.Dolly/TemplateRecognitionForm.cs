namespace Gala.Dolly
{
    public partial class TemplateRecognitionForm : UI.BaseForm
    {
        internal TemplateRecognitionForm()
        {
            InitializeComponent();
        }

        internal UI.IConsole Console
        {
            get { return this.chatbotControl; }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            ResizeTemplateRecognition();
            ResizeChatBot();

            // Load Chatbots
            chatbotControl.InitializeChatbots(Program.Engine.AI.LanguageModel.ChatbotManager);
            chatbotControl.Select();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            ResizeTemplateRecognition();
            ResizeChatBot();
        }

        private void ResizeTemplateRecognition()
        {
            // Get Image Display container size
            System.Drawing.Size containerSize = splitContainer1.Panel1.ClientSize;

            // Get the maximum available Height and Width 
            int maxHeight = containerSize.Height - (templateRecognition.display.Margin.Top + templateRecognition.display.Margin.Bottom);
            int maxWidth = containerSize.Width - (templateRecognition.display.Margin.Left + templateRecognition.display.Margin.Right);

            // Determine the relative Width of the maximum Height
            int relativeWidth = maxHeight * 4 / 3;

            if (relativeWidth < maxWidth)
            {
                templateRecognition.display.Height = maxHeight;
                templateRecognition.display.Width = relativeWidth;
            }
            else
            {
                templateRecognition.display.Height = maxWidth * 3 / 4;
                templateRecognition.display.Width = maxWidth;
            }
        }

        private void ResizeChatBot()
        {
            // ChatBot maximum size
            int maxHeight = chatbotControl.MaximumSize.Height;
            maxHeight += chatbotControl.Margin.Top + chatbotControl.Margin.Bottom;
            maxHeight += splitContainer1.Margin.Top + splitContainer1.Margin.Bottom;
            maxHeight += splitContainer1.Panel2.Margin.Top + splitContainer1.Panel1.Margin.Bottom;
            maxHeight += splitContainer1.Panel2.Padding.Top + splitContainer1.Panel2.Padding.Bottom;

            if (splitContainer1.Height - splitContainer1.SplitterDistance > maxHeight)
            {
                splitContainer1.SplitterDistance = splitContainer1.Height - maxHeight;
            }
        }

        private void templateRecognition_TemplateLoaded(object sender, System.EventArgs e)
        {
            chatbotControl.Select();
        }
    }
}