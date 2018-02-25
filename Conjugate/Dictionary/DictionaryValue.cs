using System;
using System.Runtime.Serialization;

namespace Conjugate.Dictionary {
    [Serializable()]
    class DictionaryValue {
        public string EnglishWord { get; set; }
        public string Word { get; set; }
        public string Definition { get; set; }

        public DictionaryValue() { }
        public DictionaryValue(string eng, string word, string def) {
            this.EnglishWord = eng;
            this.Word = word;
            this.Definition = def;
        }
        public override string ToString() {
            return EnglishWord + ": " + Word + " (" + Definition + ")";
        }
        /*
         * Serialization
         */
        public DictionaryValue(SerializationInfo info, StreamingContext context) {
            EnglishWord = (string)info.GetValue("EnglishWord", typeof(string));
            Word = (string)info.GetValue("Word", typeof(string));
            Definition = (string)info.GetValue("Definition", typeof(string));
        }
        public void GetSerializationData(SerializationInfo info, StreamingContext context) {
            info.AddValue("EnglishWord", EnglishWord);
            info.AddValue("Word", Word);
            info.AddValue("Definition", Definition);
        }
    }
}
