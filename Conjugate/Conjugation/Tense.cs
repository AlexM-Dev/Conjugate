using System;
using System.Runtime.Serialization;

namespace Conjugate.Conjugation {
    [Serializable()]
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
        /*
        * Serialization
        */
        public Tense(SerializationInfo info, StreamingContext context) {
            Name = info.GetString("Name");
            Person = (PersonList)info.GetValue("Person", typeof(PersonList));
        }
        public void GetSerializationData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Name", Name);
            info.AddValue("Person", Person);
        }
    }
}
