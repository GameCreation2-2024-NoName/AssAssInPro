using System.Collections.Generic;
using UnityEngine;
namespace PurpleFlowerCore
{
    public class Process: MonoBehaviour
    {
        private List<IProcessNode> _nodes;
        private int _currentNodeIndex;
        private bool _isPause = true;
        private bool _loop;
        public Process Init(bool loop = false,params IProcessNode[] nodes)
        {
            Clear();
            _loop = loop;
            _nodes = new List<IProcessNode>(nodes);
            return this;
        }
        public Process Init(bool loop = false,List<IProcessNode> nodes = null)
        {
            Clear();
            _loop = loop;
            _nodes = nodes;
            return this;
        }

        public void Start_()
        {
            _currentNodeIndex = 0;
            ResetProcesses();
            _isPause = false;
        }

        public void Pause()
        {
            _isPause = true;
        }

        public void UnPause()
        {
            _isPause = false;
        }

        public Process Add(IProcessNode node)
        {
            _nodes.Add(node);
            return this;
        }

        public void Clear()
        {
            Pause();
            _currentNodeIndex = 0;
            _nodes?.Clear();
        }
        
        private void ResetProcesses()
        {
            foreach (var process in _nodes)
            {
                process.ReSet();
            }
        }

        private void Update()
        {
            if (_isPause) return;
            if(_currentNodeIndex>=_nodes.Count)
            {
                if (_loop)
                    Start_();
                else
                    Pause();
                return;
            }
            if (_nodes[_currentNodeIndex].Update(Time.deltaTime))
                _currentNodeIndex++;
            
        }
    }
}