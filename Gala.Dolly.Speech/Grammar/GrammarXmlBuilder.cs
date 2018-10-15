using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpeechLib;

namespace Gala.Dolly.Speech
{
    using Galatea.AI.Abstract;
    using Galatea.Speech;

    static class GrammarXmlBuilder
    {
        public static void GetChatbotGrammarXml()
        {
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
    }
}