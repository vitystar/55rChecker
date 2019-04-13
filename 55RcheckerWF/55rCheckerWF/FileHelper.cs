using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace _55rCheckerWF
{
    public static class FileHelper
    {
        private static string path = System.Environment.CurrentDirectory;

        public static bool FileExits(string fileName) => File.Exists(path + @"\" + fileName);

        public static void SaveString(string str, string fileName)
        {
            using (FileStream fs = new FileStream(path + @"\" + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] data = Encoding.Default.GetBytes(str);
                fs.Write(data, 0, data.Length);
            }
        }

        public static void SaveObject<T>(T t, string fileName)
        {
            using (FileStream fs = new FileStream(path + @"\" + fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, t);
            }
        }

        public static T GetObject<T>(string fileName) where T : class, new()
        {
            using (FileStream fs = new FileStream(path + @"\" + fileName, FileMode.Open, FileAccess.ReadWrite))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(fs) as T;
            }
        }
    }
}
