using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
#if UNITY_EDITOR
using XNodeEditor;
#endif

[CreateAssetMenu]
public class FishGraph : NodeGraph {
}

#if UNITY_EDITOR
[CustomNodeGraphEditor(typeof(FishGraph))]
public class FishGraphEditor : NodeGraphEditor
{
    public override string GetNodeMenuName(Type type)
    {
        if (type == typeof(FishNode))
        {
            return base.GetNodeMenuName(type);
        }
        return null;
    }
}
#endif