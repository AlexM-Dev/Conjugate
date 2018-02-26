using System;
using System.Runtime.Serialization;

namespace Conjugate.Conjugation {
    [Serializable()]
    class Tense {
        public string Infinitive { get; set; }
        public string Name { get; set; }
        public PersonList Person { get; set; }
        public override string ToString() {
            return "Name: " + Name + "\n\n" + Person.ToString();
        }
        public Tense(string infinitive, string name, PersonList person) {
            this.Infinitive = infinitive;
            this.Name = name;
            this.Person = person;
        }
        public Tense() { }
        /*
        * Serialization
        */
        public Tense(SerializationInfo info, StreamingContext context) {
            Infinitive = info.GetString("Infinitive");
            Name = info.GetString("Name");
            Person = (PersonList)info.GetValue("Person", typeof(PersonList));
        }
        public void GetSerializationData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Infinitive", Infinitive);
            info.AddValue("Name", Name);
            info.AddValue("Person", Person);
        }
    }
}
