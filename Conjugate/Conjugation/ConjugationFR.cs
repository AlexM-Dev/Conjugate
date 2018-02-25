using System.Collections.Generic;
using System.Net.Http;

namespace Conjugate.Conjugation {
    class ConjugationFR {
        HttpClient client;
        public ConjugationFR(HttpClient client) {
            this.client = client;
        }
        public List<Tense> GetConjugations(string verb) {
            string stripVerb = EncodingUtilities.RemoveDiacritics(verb);
            var bytes = client.GetAsync("http://www.conjugation-fr.com" +
                "/conjugate.php?verb=" + stripVerb).Result.Content.
                ReadAsByteArrayAsync().Result;

            return TenseReader.Get(EncodingUtilities.
                ISO_8859_1.GetString(bytes));
        }
    }
}
