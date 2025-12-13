using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ConditionalNode : CoreNodeBase {
    [TextArea] public string quest;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool uninitiated;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool incomplete;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool complete;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool closed;
    [Input(typeConstraint = TypeConstraint.Strict)] public bool entry;
}