using System.IO;
using System;
using UnityEngine;

namespace PurpleFlowerCore.Save
{
#if PFC_SAVE_JSON
    public class SaveJsonModule
    {
        public void Save(string fileName, object data)
        {
            CheckFile(fileName);
            var jsonStr = JsonUtility.ToJson(data);
            File.WriteAllText(GetPath(fileName), jsonStr);
        }

        public T Load<T>(string fileName) where T: class
        {
            if (!CheckFile(fileName)) return null;
            var jsonStr = File.ReadAllText(GetPath(fileName));
            var data = JsonUtility.FromJson<T>(jsonStr);
            return data;
        }

        public object Load(string fileName, Type type)
        {
            if (!CheckFile(fileName)) return null;
            var jsonStr = File.ReadAllText(GetPath(fileName));
            var data = JsonUtility.FromJson(jsonStr, type);
            return data;
        }

        private string GetPath(string fileName)
        {
            return Application.persistentDataPath + "/Json" + $"/{fileName}.json";
        }
        
        private bool CheckFile(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/Json/"))
                Directory.CreateDirectory(Application.persistentDataPath + "/Json/");
            return File.Exists(GetPath(fileName));
        }
    }
#endif
}
