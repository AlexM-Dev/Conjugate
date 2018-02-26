using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Conjugate {
    class ObjectSerializer {
        public static void ToFile<T>(T value, string file) {
            using (Stream stream = File.Open(file, FileMode.Create)) {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(stream, value);
                stream.Close();
            }
        }

        public static T FromFile<T>(string file) {
            T value;

            using (Stream stream = File.Open(file, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                value = (T)formatter.Deserialize(stream);

                stream.Close();
            }

            return value;
        }
    }
}
