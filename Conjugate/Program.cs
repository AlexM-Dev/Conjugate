using Conjugate.Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace Conjugate {
    class Program {
        static HttpClient client = new HttpClient();
        static void Main(string[] args) {
            // Create the browser client.
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows" +
                " NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/63.0.3239.132 Safari/537.36");

            // Get all possible translations.
            var t = translations("eat");

            // Write the translation(s) down.
            string sep = "\n => ";
            string indent = "    => ";
            foreach (DictionaryValue value in t) {
                Console.WriteLine(value.ToString());

                var tenses = conjugations(value.Word);

                var pTense = tenses[0]; // Gets the present tense.

                Console.WriteLine(indent + "Tense: " + pTense.Name);
                Console.WriteLine(indent + "Je: " + pTense.Person.Je);
                Console.WriteLine(indent + "Tu: " + pTense.Person.Tu);
                Console.WriteLine(indent + "Il: " + pTense.Person.Il);
                Console.WriteLine(indent + "Nous: " + pTense.Person.Nous);
                Console.WriteLine(indent + "Vous: " + pTense.Person.Vous);
                Console.WriteLine(indent + "Ils: " + pTense.Person.Ils);
            }

            Console.ReadKey();
        }
        private static List<DictionaryValue> translations(string verb) {
            string store = "translations/"; // Path to store translations.
            if (!Directory.Exists(store))
                Directory.CreateDirectory(store);

            foreach (string file in Directory.GetFiles(store, "*.dict")) {
                List<DictionaryValue> entry = ObjectSerializer.FromFile
                    <List<DictionaryValue>>(file);
                if (entry.Count > 0 && entry[0].EnglishWord.Equals(verb,
                    StringComparison.InvariantCultureIgnoreCase)) {
                    return entry;
                }
            }

            List<DictionaryValue> translations =
                new WordReference(client).GetENFR_Translations(verb);
            ObjectSerializer.ToFile(translations, store + verb + ".dict");

            return translations;
        }
        private static List<Conjugation.Tense> conjugations(string infinitive) {
            string store = "conjugations/";
            if (!Directory.Exists(store))
                Directory.CreateDirectory(store);

            foreach (string file in Directory.GetFiles(store, "*.conj")) {
                var entry =
                    ObjectSerializer.FromFile<List<Conjugation.Tense>>(file);
                if (entry.Count > 0 && entry[0].Infinitive.Equals(infinitive,
                    StringComparison.InvariantCultureIgnoreCase)) {
                    return entry;
                }
            }

            var conjugations =
                new Conjugation.ConjugationFR(client).GetConjugations(infinitive);
            ObjectSerializer.ToFile(conjugations, store + infinitive + ".conj");

            return conjugations;
        }
    }
}
