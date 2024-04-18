using System.IO;
using UnityEngine;
using PurpleFlowerCore;

namespace PurpleFlowerCore.Utility
{
    /// <summary>
    /// 请在Scripting Define Symbols中添加PFC_XLUA以使用功能
    /// </summary>
    public class LuaManager : SafeSingleton<LuaManager>
    {
#if PFC_XLUA
        private LuaEnv _luaEnv;
        private LuaEnv LuaEnv
        {
            get
            {
                if (_luaEnv is not null) return _luaEnv;
                _luaEnv = new LuaEnv();
                _luaEnv.AddLoader(MyCustomLoader);
                _luaEnv.AddLoader(MyCustomABLoader);
                return _luaEnv;
            }
        }

        public LuaTable Global => LuaEnv.Global;
        
        public void DoScript(string fileName)
        {
            LuaEnv.DoString($"require('{fileName}')");
        }

        public void DoString(string str)
        {
            LuaEnv.DoString(str);
        }

        public void Tick()
        {
            LuaEnv.Tick();
        }

        public void Dispose()
        {
            if (_luaEnv is null) return;
            _luaEnv.Dispose();
            _luaEnv = null;
        }
        
        private byte[] MyCustomLoader(ref string fileName)
        {
            var path = Application.dataPath + "/Scripts/Lua/" + fileName + ".lua";
            Debug.Log(path);
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            Debug.LogWarning("重定向失败"+path);
            return null;
        }

        /// <summary>
        /// 重定向加载AB包脚本
        /// </summary>
        private byte[] MyCustomABLoader(ref string fileName)
        {
            // var path = Application.streamingAssetsPath + "/lua";
            // AssetBundle ab = AssetBundle.LoadFromFile(path);
            // TextAsset tx = ab.LoadAsset<TextAsset>(fileName + ".lua");
            // return tx.bytes;
            var lua = ABManager.Instance.LoadResource<TextAsset>("lua", fileName + ".lua");
            if (lua is not null)
                return lua.bytes;
            Debug.LogWarning("重定向失败,文件名:"+fileName);
            return null;
        }
#endif
    }
}
