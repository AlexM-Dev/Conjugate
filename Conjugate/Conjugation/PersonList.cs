using System.Runtime.Serialization;

namespace Conjugate.Conjugation {
    class PersonList {
        public string Je { get; set; }
        public string Tu { get; set; }
        public string Il { get; set; } // or Elle.
        public string Nous { get; set; }
        public string Vous { get; set; }
        public string Ils { get; set; } // or Elles.

        public PersonList(string je, string tu, string il, string nous, string vous, string ils) {
            this.Je = je;
            this.Tu = tu;
            this.Il = il;
            this.Nous = nous;
            this.Vous = vous;
            this.Ils = ils;
        }
        public PersonList() { }

        public override string ToString() {
            return "Je: " + Je + "\n" +
                "Tu: " + Tu + "\n" +
                "Il: " + Il + "\n" +
                "Nous: " + Nous + "\n" +
                "Vous: " + Vous + "\n" +
                "Ils: " + Ils;
        }
        /*
        * Serialization
        */
        public PersonList(SerializationInfo info, StreamingContext context) {
            Je = info.GetString("Je");
            Tu = info.GetString("Tu");
            Il = info.GetString("Il");
            Nous = info.GetString("Nous");
            Vous = info.GetString("Vous");
            Ils = info.GetString("Ils");
        }
        public void GetSerializationData(SerializationInfo info, StreamingContext context) {
            info.AddValue("Je", Je);
            info.AddValue("Tu", Tu);
            info.AddValue("Il", Il);
            info.AddValue("Nous", Nous);
            info.AddValue("Vous", Vous);
            info.AddValue("Ils", Ils);
        }
    }
}
