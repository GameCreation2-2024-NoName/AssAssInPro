#if PFC_SAVE_LITJSON
using System;
using System.IO;
using LitJson;
using UnityEngine;

namespace PurpleFlowerCore.Save
{
    /// <summary>
    /// 请在Scripting Define Symbols中添加PFC_LITJSON以使用功能
    /// </summary>
    public class SaveLitJsonModule
    {
        public void Save(string fileName, object data)
        {
            CheckFile(fileName);
            var jsonStr = JsonMapper.ToJson(data);
            File.WriteAllText(GetPath(fileName), jsonStr);
        }
        
        public T Load<T>(string fileName) where T: class
        {
            if (!CheckFile(fileName)) return null;
            var jsonStr = File.ReadAllText(GetPath(fileName));
            var data = JsonMapper.ToObject<T>(jsonStr);
            return data;
        }
        
        public object Load(string fileName, Type type)
        {
            if (!CheckFile(fileName)) return null;
            var jsonStr = File.ReadAllText(GetPath(fileName));
            var data = JsonMapper.ToObject(jsonStr, type);
            return data;
        }
        
        private string GetPath(string fileName)
        {
            return Application.persistentDataPath + "/LitJson" + $"/{fileName}.json";
        }
        
        private bool CheckFile(string fileName)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/LitJson/"))
                Directory.CreateDirectory(Application.persistentDataPath + "/LitJson/");
            return File.Exists(GetPath(fileName));
        }
    }
}

#endif