using System.Collections.Generic;
using PurpleFlowerCore.Base;
using PurpleFlowerCore.Pool;
using UnityEngine;

namespace PurpleFlowerCore
{
    public static class ProcessSystem
    {
        private static readonly Dictionary<string,Process> _processes = new();

        private static GameObjectPoolData _pool;

        private static GameObjectPoolData Pool
        {
            get
            {
                if (_pool is not null) return _pool;
                var root = new GameObject("Process")
                {
                    transform = { parent = PFCManager.Instance.transform }
                };
                _pool = new GameObjectPoolData(root.transform,Resources.Load<GameObject>("PFCRes/Process"));
                return _pool;
            }
        }
        
        public static Process CreateProcess(string processName,bool loop = false,params IProcessNode[] nodes)
        {
            if (_processes.ContainsKey(processName))
                DestroyProcess(processName);
            var theProcess = Pool.Pop().GetComponent<Process>();
            theProcess.Init(loop, nodes);
            theProcess.gameObject.name = processName;
            _processes.Add(processName,theProcess);
            return theProcess;
        }
        
        public static Process CreateProcess(string processName,bool loop = false,List<IProcessNode> nodes = null)
        {
            if (_processes.ContainsKey(processName))
                DestroyProcess(processName);
            var theProcess = Pool.Pop().GetComponent<Process>();
            nodes ??= new List<IProcessNode>();
            theProcess.Init(loop, nodes);
            theProcess.gameObject.name = processName;
            _processes.Add(processName,theProcess);
            return theProcess;
        }

        public static Process GetProcess(string processName)
        {
            return _processes.TryGetValue(processName, out var process) ? process : CreateProcess(processName);
        }
        
        public static void DestroyProcess(string processName)
        {
            if (!_processes.ContainsKey(processName))
            {
                PFCLog.Warning("不存在process:"+processName);
                return;
            }

            if (!_processes[processName])
            {
                _processes.Remove(processName);
            }
            var theGameObject = _processes[processName].gameObject;
            theGameObject.name = "process";
            Pool.Push(theGameObject);
            _processes.Remove(processName);
        }
    }
}