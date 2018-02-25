namespace Conjugate.Conjugation {
    class Tense {
        public string Name { get; set; }
        public PersonList Person { get; set; }
        public override string ToString() {
            return "Name: " + Name + "\n\n" + Person.ToString();
        }
        public Tense(string name, PersonList person) {
            this.Name = name;
            this.Person = person;
        }
        public Tense() { }
    }
}
