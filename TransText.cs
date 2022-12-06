using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;
using Google.Cloud.Vision.V1;
using System.IO;

namespace JpVisionProjectForm1201
{
    internal class TransText
    {
        string credentialsString;
        TranslationClient client;
        public TransText(String jsonPath) {
            credentialsString = File.ReadAllText(jsonPath);
            client = new TranslationClientBuilder
            {
                JsonCredentials = credentialsString
            }.Build();

        }

        String[] sorceLang = new string[4] { "en", "ja", "ru", "zh" };

        public String DoTrans(String sourceText, int count)
        {
            var response = client.TranslateText(
                text: sourceText,
                targetLanguage: "ko"
                , sourceLanguage: sorceLang[count]
                );

            return response.TranslatedText;
        }
    }
}
