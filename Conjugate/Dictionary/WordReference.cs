using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;

namespace Conjugate.Dictionary {
    class WordReference {
        HttpClient client;
        public WordReference(HttpClient client) {
            this.client = client;
        }

        public List<DictionaryValue> GetENFR_Translations(string verb) {
            string wordReferenceSource = client.GetAsync(
                "http://www.wordreference.com/enfr/" + verb).Result
                .Content.ReadAsStringAsync().Result;

            //<td class='FrWrd' >([\w\W]+?)<\/td><td class='ToWrd' >([\w\W]+?)<\/td> gets list of conjugations.
            string conjugationPattern = "<td class='FrWrd' >([\\w\\W]+?)<\\/td><td>([\\w\\W]+?)<\\/td>" +
                "<td class='ToWrd' >([\\w\\W]+?)<\\/td>";

            var conjugations = Regex.Matches(wordReferenceSource, conjugationPattern);

            List<DictionaryValue> conjList = new List<DictionaryValue>();
            foreach (Match conjMatch in conjugations) {
                string definition = "";

                string fromRegex = "<strong>(" + verb + ")([\\w\\W]+)?<\\/strong>";
                string defRegex = "\\(([\\w\\W]+?)\\)";
                string toRegex = "FrVerbs.aspx\\?v=([\\w\\W]+?)\"";

                MatchCollection fromCollection = Regex.Matches(conjMatch.
                    Groups[1].Value, fromRegex);
                MatchCollection defCollection = Regex.Matches(conjMatch.
                    Groups[2].Value, defRegex);
                MatchCollection toCollection = Regex.Matches(conjMatch.
                    Groups[3].Value, toRegex);

                if (fromCollection.Count <= 0 || defCollection.Count <= 0 ||
                    toCollection.Count <= 0 || fromCollection[0].Groups.Count < 2 ||
                    defCollection[0].Groups.Count < 2 || toCollection[0].Groups.Count < 2)
                    continue;

                verb = fromCollection[0].Groups[1].Value;
                definition = defCollection[0].Groups[1].Value;
                string to = HttpUtility.UrlDecode(toCollection[0].Groups[1].Value);
                
                conjList.Add(new DictionaryValue(verb, to, definition));
            }

            return conjList;
        }
    }
}
