namespace Conjugate.Dictionary {
    class DictionaryValue {
        public string Word { get; set; }
        public string Definition { get; set; }
        public DictionaryValue(string word, string def) {
            this.Word = word;
            this.Definition = def;
        }

        public override string ToString() {
            return Word + " (" + Definition + ")";
        }
    }
}
