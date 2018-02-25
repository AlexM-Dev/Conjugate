using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Conjugate.Conjugation {
    class TenseReader {
        public static List<Tense> Get(string source) {
            var tenses = new List<Tense>();

            string blockRegex = "<td width=\\\"221\\\" [\\w\\W]+?>([\\w\\W]+?)<\\/td>";
            MatchCollection matches = Regex.Matches(source, blockRegex);

            foreach (Match m in matches) {
                Tense tense = new Tense();

                string content = m.Groups[1].Value.Trim('\n');

                string nameRegex = "<span class=\"arial-13-bleu\">(.*?)<\\/span>";
                tense.Name = Regex.Match(content, nameRegex).Groups[1].Value;

                string personsRegex = "(.*?)<span class=\"conjuguaison\">(.*?)<\\/span>";

                PersonList personList = new PersonList();

                foreach (Match person in Regex.Matches(content, personsRegex)) {
                    string conjugate = (person.Groups[1].Value + person.Groups[2].Value).Trim();
                    string formatted = conjugate.Trim().Remove(0, conjugate.IndexOf(' ') + 1);
                    
                    if (!standardReader(conjugate, formatted, ref personList)) {
                        subjunctiveReader(conjugate, ref personList);
                    }
                }
                tense.Person = personList;

                tenses.Add(tense);
            }

            return tenses;
        }
        private static bool standardReader(string conjugate, string formatted,
           ref PersonList personList) {

            bool completed = true;

            if (conjugate.StartsWith("je") || conjugate.StartsWith("j'"))
                personList.Je = Regex.Replace(formatted, "j'(.*?)", "(j') ");
            else if (conjugate.StartsWith("tu"))
                personList.Tu = formatted;
            else if (conjugate.StartsWith("ils"))
                personList.Ils = formatted;
            else if (conjugate.StartsWith("nous"))
                personList.Nous = formatted;
            else if (conjugate.StartsWith("vous"))
                personList.Vous = formatted;
            else if (conjugate.StartsWith("il"))
                personList.Il = formatted;
            else
                completed = false;

            return completed;
        }

        private static bool subjunctiveReader(string conjugate,
            ref PersonList personList) {
            bool completed = true;

            string[] replace1 = new string[] { "que ", "qu'" };
            string[] replace2 = new string[] { "j'", "tu ", "il ", "ils ", "nous ", "vous ", "je " };

            foreach (string s in replace1) {
                foreach (string s2 in replace2) {
                    string rS = s + s2;
                    
                    int matchCount = Regex.Matches(conjugate, "^" + rS).Count;

                    if (matchCount < 1) continue;

                    if ((s2 == "j'" || s2 == "je "))
                        personList.Je = conjugate;
                    else if (s2 == "tu ")
                        personList.Tu = conjugate;
                    else if (s2 == "il ")
                        personList.Il = conjugate;
                    else if (s2 == "ils ")
                        personList.Ils = conjugate;
                    else if (s2 == "nous ")
                        personList.Nous = conjugate;
                    else if (s2 == "vous ")
                        personList.Vous = conjugate;
                    else
                        completed = false;
                }
            }

            return completed;
        }
    }
}
