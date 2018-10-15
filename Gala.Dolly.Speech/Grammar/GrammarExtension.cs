using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpeechLib;

namespace Gala.Dolly.Speech
{
    using Galatea.AI.Abstract;
    using Galatea.Speech;

    static class GrammarExtension
    {
        /*
        private const string XML_GRAMMAR_ROOT_TAG = "<GRAMMAR LANGID=\"{0}\"><!--[CONTENT]--></GRAMMAR>";
        private const string XML_DEFINE_TAG = "<DEFINE><!--[CONTENT]--></DEFINE>";
        private const string XML_ID_TAG = "<ID NAME=\"{0}\" VAL=\"{1}\" />";
        private const string XML_RULE_TAG = "<RULE NAME=\"Greeting\" ID=\"{0}\" TOPLEVEL=\"{1}\"><!--[CONTENT]--></RULE>";
        private const string XML_LIST_TAG = "<LIST><!--[CONTENT]--></LIST>";
        private const string XML_PHRASE_TAG = "<PHRASE><!--[CONTENT]--></PHRASE>";
        private const string XML_OPTIONAL_TAG = "<OPT><!--[CONTENT]--></OPT>";

        public static void LoadChatbotRulesXml(this ISpeechRecoGrammar grammar)
        {
            //grammar.CmdLoadFromMemory
        }
         */

        //public static string ErrorText { get { return _errorText; } }

        public static void LoadChatbotRules(this ISpeechRecoGrammar grammar, string chatbotName, string ruleName)
        {
            // Greeting Rule
            ISpeechGrammarRule chatbotGreetingRule = grammar.Rules.Add(ruleName, SpeechRuleAttributes.SRATopLevel, 1);
            ISpeechGrammarRuleState chatbotRuleState = chatbotGreetingRule.AddState();
            chatbotGreetingRule.InitialState.AddWordTransition(chatbotRuleState, "Hi,Hello,Hey", ",");

            // Add Addressees
            ISpeechGrammarRule chatbotGreetingAddresseeRule = grammar.Rules.Add(ruleName + "." + "Addressee", SpeechRuleAttributes.SRADynamic);
            chatbotGreetingAddresseeRule.InitialState.AddWordTransition(null, "Robot", " ", SpeechGrammarWordType.SGLexical);
            chatbotGreetingAddresseeRule.InitialState.AddWordTransition(null, chatbotName, " ", SpeechGrammarWordType.SGLexical);
            chatbotRuleState.AddRuleTransition(null, chatbotGreetingAddresseeRule);

            //// Activate the top level Rule
            //grammar.CmdSetRuleState("Gala.Dolly.Chatbot.Greeting", SpeechRuleState.SGDSActive);

            // Commit
            string errorText;
            grammar.Rules.CommitAndSave(out errorText);

            //_errorText = string.IsNullOrEmpty(errorText) ? null : errorText;

            if(errorText != null)
                throw new TeaSpeechException(string.Format(Galatea.AI.Language.LanguageResources.GrammarRules_CommitAndSave_ErrorText_Format, errorText));
        }

        public static void LoadEvaluateRules(this ISpeechRecoGrammar grammar)
        {
            string[] evaluateTokens = Galatea.AI.Language.LanguageResources.Tokens_Evaluate.Split(',');
            string[] templateTypeTokens = GetTemplateTypeTokens();

            // Evaluate Rule
            ISpeechGrammarRule evaluateRule = grammar.Rules.Add("GalateaRules.Evaluate", SpeechRuleAttributes.SRATopLevel & SpeechRuleAttributes.SRADynamic, 1);
            ISpeechGrammarRuleState evaluateRuleState = evaluateRule.AddState();

            // Evaluate Tokens (e.g.: "What?")
            foreach (string token in evaluateTokens)
            {
                evaluateRule.InitialState.AddWordTransition(evaluateRuleState, token);
            }

            // Template Type Tokens
            ISpeechGrammarRule evaluateTemplateTypeRule = grammar.Rules.Add("GalateaRules.Evaluate.TemplateType", SpeechRuleAttributes.SRADynamic, 2);

            foreach(string token in templateTypeTokens)
            {
                evaluateTemplateTypeRule.InitialState.AddWordTransition(null, token, " ", SpeechGrammarWordType.SGLexical);
            }

            evaluateRuleState.AddRuleTransition(null, evaluateTemplateTypeRule);            
        }

        private static string[] GetTemplateTypeTokens()
        {
            string[] ttTokenInfo = Galatea.AI.Language.LanguageResources.Tokens_TemplateType.Split(',');
            string[] ttTokens = ttTokenInfo.Select(t => t.Split(':')[1]).ToArray();

            return ttTokens;
        }

        //private static string _errorText;
    }
}