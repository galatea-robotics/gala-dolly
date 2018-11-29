using System;
using System.Windows.Forms;
using Gala.Dolly.UI;

namespace Gala.Dolly
{
    internal partial class MainForm : UI.BaseForm
    {
        internal MainForm()
        {
            InitializeComponent();
        }

        internal IConsole Console { get { return this.chatbotControl; } }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            // Load Chatbots
            chatbotControl.InitializeChatbots(Program.Engine.AI.LanguageModel.ChatbotManager);
            chatbotControl.Select();
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            //ResizeVideoCapture();
            ResizeChatBot();
        }
        
        /*
        private void ResizeVideoCapture()
        {
            // Get Image Display container size
            System.Drawing.Size containerSize = splitContainer.Panel1.ClientSize;

            // Get the maximum available Height and Width 
            int maxHeight = containerSize.Height - (visionCapture.cameraWindow.Margin.Top + visionCapture.cameraWindow.Margin.Bottom);
            int maxWidth = containerSize.Width - (visionCapture.cameraWindow.Margin.Left + visionCapture.cameraWindow.Margin.Right);

            // Determine the relative Width of the maximum Height
            int relativeWidth = maxHeight * 4 / 3;

            if (relativeWidth < maxWidth)
            {
                visionCapture.cameraWindow.Height = maxHeight;
                visionCapture.cameraWindow.Width = relativeWidth;
            }
            else
            {
                visionCapture.cameraWindow.Height = maxWidth * 3 / 4;
                visionCapture.cameraWindow.Width = maxWidth;
            }
        }
         */

        private void ResizeChatBot()
        {
            // ChatBot maximum size
            int maxHeight = chatbotControl.MaximumSize.Height;
            maxHeight += chatbotControl.Margin.Top + chatbotControl.Margin.Bottom;
            maxHeight += splitContainer.Margin.Top + splitContainer.Margin.Bottom;
            maxHeight += splitContainer.Panel2.Margin.Top + splitContainer.Panel1.Margin.Bottom;
            maxHeight += splitContainer.Panel2.Padding.Top + splitContainer.Panel2.Padding.Bottom;

            if (splitContainer.Height - splitContainer.SplitterDistance > maxHeight)
            {
                splitContainer.SplitterDistance = splitContainer.Height - maxHeight;
            }
        }

        /*
        private void TemplateRecognition_TemplateLoaded(object sender, System.EventArgs e)
        {
            chatbotControl.Select();
        } 
         */
    }
}