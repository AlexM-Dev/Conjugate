using Conjugate.Dictionary;
using System;
using System.Net.Http;

namespace Conjugate {
    class Program {
        static void Main(string[] args) {
            // Create the browser client.
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows" +
                " NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
                "Chrome/63.0.3239.132 Safari/537.36");

            // Create a wordreference instance.
            WordReference wordReference = new WordReference(client);
            var t = wordReference.GetENFR_Translations("eat");

            // Create a conjugationfr instance.
            var fr = new Conjugation.ConjugationFR(client);

            // Write the translation(s) down.
            string sep = "\n => ";
            string indent = "    => ";
            Console.Write(t.Item1 + sep);
            foreach (DictionaryValue value in t.Item2) {
                Console.WriteLine(value.ToString());

                var pTense = fr.GetConjugations(value.Word)[0];
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
    }
}
