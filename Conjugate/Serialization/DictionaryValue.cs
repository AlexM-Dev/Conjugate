using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Conjugate.Serialization {
    class DictionaryValue {
        public static void ToFile(List<Dictionary.DictionaryValue> value, string file) {
            using (Stream stream = File.Open(file + ".dict", FileMode.Create)) {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, value);
                stream.Close();
            }
        }

        public static List<Dictionary.DictionaryValue> FromFile(string file) {
            List<Dictionary.DictionaryValue> value = null;
            
            using (Stream stream = File.Open(file + ".dict", FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                value = (List<Dictionary.DictionaryValue>)formatter.Deserialize(stream);

                stream.Close();
            }

            return value;
        }
    }
}
