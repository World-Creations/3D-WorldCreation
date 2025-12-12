using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ObjectiveNode : CoreNodeBase {
    [TextArea] public string identifier;
    public int addProgress;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool exit;
    [Input(typeConstraint = TypeConstraint.Strict)] public bool entry;
    public string getDialogueType { get { return "objective"; } }
}