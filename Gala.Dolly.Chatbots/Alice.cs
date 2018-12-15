using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using AIMLBot.Utils;

namespace Gala.Dolly.Chatbots
{
    using Galatea.Runtime.Services;
    using Galatea.Globalization;
#if !NETFX_CORE
    using Properties;
#endif
    /// <summary>
    /// A ChatBot interface running the ALICE artificial intelligence algorithm.
    /// </summary>
    internal class Alice : Chatbot
    {
#if !NETFX_CORE
        /// <summary>
        /// Initializes a new instance of the <see>Galatea.ChatBots.Alice</see> class.
        /// </summary>
        public Alice(Galatea.AI.Abstract.IUser user, string chatbotName) :
            this(user, chatbotName, Settings.Default.ChatbotAliceConfigFolder, Settings.Default.ChatbotResourcesFolder)
        {
        }
#endif
        /// <summary>
        /// Initializes a new instance of the <see>Galatea.ChatBots.Alice</see> class.
        /// </summary>
        public Alice(Galatea.AI.Abstract.IUser user, string chatbotName, string chatbotAliceConfigFolder, string chatbotResourcesFolder) : base(chatbotName)
        {
            userName = user.Name;
            aimlBot = new AIMLBot.Bot();

            // Validate Folders
            ValidateFolders(chatbotAliceConfigFolder, chatbotResourcesFolder);

            // Initialize AIMLBot2.5 Properties
            aimlBot.LoadSettings(chatbotAliceConfigFolder);
            aimlBot.PathToAIML = Path.Combine(chatbotResourcesFolder, "alice");

            aimlBot.LoadAIMLFromFiles();
            aimlBot.DefaultPredicates.UpdateSetting("name", userName);

            // Initialize Chat runtime
            aimlUser = new AIMLBot.User(userName, aimlBot);
        }

        private static void ValidateFolders(string chatbotAliceConfigFolder, string chatbotResourcesFolder)
        {
            if (!Directory.Exists(chatbotAliceConfigFolder))
                throw new FileNotFoundException(
                    string.Format(CultureInfo.CurrentCulture,
                        ChatbotResources.ChatbotAliceConfigFolder_Not_Found,
                        chatbotAliceConfigFolder));

            if (!Directory.Exists(chatbotResourcesFolder))
                throw new FileNotFoundException(
                    string.Format(CultureInfo.CurrentCulture,
                        ChatbotResources.ChatbotResourcesFolder_Not_Found,
                        chatbotResourcesFolder));
        }

        public override string Greeting { get { return ChatbotResources.ChatbotAliceGreeting; } }

        /// <summary>
        /// An artificial intelligence method that responds to a text input based on the ALICE algorithm.
        /// </summary>
        /// <param name="input"> The text input to process. </param>
        /// <returns> The text response. </returns>
        protected override string GetResponse(string input)
        {
            AIMLBot.Request aimlRequest = new AIMLBot.Request(input, aimlUser, aimlBot, true);
            return aimlBot.Chat(aimlRequest).Output;
        }

        public override string FriendlyName
        {
            get { return base.FriendlyName; }
            set
            {
                aimlBot.GlobalSettings.UpdateSetting("name", value);
                base.FriendlyName = value;
            }
        }

        /*
        private static string GetChatbotAliceConfigFolder()
        {
            return Properties.Settings.Default.ChatbotAliceConfigFolder;
        }
        private static string GetChatbotResourcesFolder()
        {
            return Properties.Settings.Default.ChatbotResourcesFolder;
        }
         */

        private readonly string userName;
        private readonly AIMLBot.Bot aimlBot;
        private readonly AIMLBot.User aimlUser;
    }
}
