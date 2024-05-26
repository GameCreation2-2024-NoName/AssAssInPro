using System;
using System.Linq;
using Fungus;
using UnityEngine;

namespace Hmxs.Toolkit.Plugins.Fungus.FungusTools
{
    public static class FlowchartManager
    {
        private static Flowchart _defaultFlowchart;
        
        /// <summary>
        /// Find GameObject named "Flowchart" in hierarchy and Return it's Flowchart Component.
        /// You should use it when you only have one default Flowchart in hierarchy.
        /// </summary>
        /// <returns>Flowchart Instance</returns>
        public static Flowchart GetFlowchart()
        {
            if (_defaultFlowchart != null) return _defaultFlowchart;
            
            var flowchartObj = GameObject.Find("Flowchart");
            if (flowchartObj == null)
                throw new Exception("FlowchartManager: Can't Find 'Flowchart' GameObject.");
            
            var flowchart = flowchartObj.GetComponent<Flowchart>();
            if (flowchart == null)
                throw new Exception("FlowchartManager: Can't Find Flowchart Component on 'Flowchart' GameObject.");
            
            _defaultFlowchart = flowchart;
            return flowchart;
        }

        /// <summary>
        /// Find GameObject with given name and Return it's Flowchart Component.
        /// </summary>
        /// <param name="flowchartObjName">GameObject name who has Flowchart Component</param>
        /// <returns>Flowchart Instance</returns>
        public static Flowchart GetFlowchart(string flowchartObjName)
        {
            var flowchartObj = GameObject.Find(flowchartObjName);
            if (flowchartObj == null)
                throw new Exception($"FlowchartManager: Can't Find '{flowchartObjName}' GameObject.");
            
            var flowchart = flowchartObj.GetComponent<Flowchart>();
            if (flowchart == null)
                throw new Exception($"FlowchartManager: Can't Find Flowchart Component on '{flowchartObjName}' GameObject.");
            
            return flowchart;
        }

        /// <summary>
        /// Execute Block in Default Flowchart
        /// </summary>
        /// <param name="blockName">BlockName</param>
        public static void ExecuteBlock(string blockName)
        {
            var flowchart = GetFlowchart();
            ExecuteBlock(flowchart, blockName);
        }

        /// <summary>
        /// Execute Block in Flowchart with given name
        /// </summary>
        /// <param name="flowchartObjName">FlowchartName</param>
        /// <param name="blockName">BlockName</param>
        public static void ExecuteBlock(string flowchartObjName, string blockName)
        {
            var flowchart = GetFlowchart(flowchartObjName);
            ExecuteBlock(flowchart, blockName);
        }

        /// <summary>
        /// Execute Block in Flowchart
        /// </summary>
        /// <param name="flowchart">Flowchart</param>
        /// <param name="blockName">BlockName</param>
        public static void ExecuteBlock(Flowchart flowchart, string blockName)
        {
            if (flowchart == null)
            {
                Debug.LogWarning($"FlowchartManager: {flowchart.GetName()} is Null");
                return;
            }

            if (flowchart.HasBlock(blockName))
            {
                if (flowchart.GetExecutingBlocks().Any(block => block.BlockName == blockName))
                    return;
                flowchart.ExecuteBlock(blockName);
            }
            else
                Debug.LogWarning($"FlowchartManager: Can't Find Block named '{blockName}'");
        }
    }
}