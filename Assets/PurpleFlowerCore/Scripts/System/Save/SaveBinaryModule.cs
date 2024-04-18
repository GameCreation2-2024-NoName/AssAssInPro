using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace PurpleFlowerCore.Save
{
#if PFC_SAVE_BINARY
    public class SaveBinaryModule
    {
        private byte Key => 231;
        public void Save(string fileName, object data)
        {
            CheckFile(fileName);
            using MemoryStream ms = new();
            BinaryFormatter bf = new();
            bf.Serialize(ms,data);
            var bytes = ms.GetBuffer();
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= Key;
            }
            File.WriteAllBytes(GetPath(fileName),bytes);
                
            ms.Close();
        }

        public T Load<T>(string fileName) where T: class
        {
            if (!CheckFile(fileName)) return null;
            var bytes = File.ReadAllBytes(GetPath(fileName));
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= Key;
            }
            using MemoryStream ms = new(bytes);
            BinaryFormatter bf = new();
            var data = bf.Deserialize(ms) as T;
            ms.Close();
            return data;
        }

        public object Load(string fileName)
        {
            if (!CheckFile(fileName)) return null;
            var bytes = File.ReadAllBytes(GetPath(fileName));
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] ^= Key;
            }
            using MemoryStream ms = new(bytes);
            BinaryFormatter bf = new();
            var data = bf.Deserialize(ms);
            ms.Close();
            return data;
        }

        private string GetPath(string fileName)
        {
            return Application.persistentDataPath + "/Binary" + $"/{fileName}.pfcs";
        }
        
        private bool CheckFile(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Binary/"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Binary/");
            return File.Exists(GetPath(fileName));
        }
    }
#endif
}