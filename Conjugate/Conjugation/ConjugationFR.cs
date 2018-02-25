using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Conjugate.Conjugation {
    class ConjugationFR {
        HttpClient client;
        public ConjugationFR(HttpClient client) {
            this.client = client;
        }
        public List<Tense> GetConjugations(string verb) {
            string source = client.GetAsync("http://www.conjugation-fr.com" +
                "/conjugate.php?verb=" + verb).Result.Content.
                ReadAsStringAsync().Result;

            return TenseReader.Get(source);
        }
    }
}
