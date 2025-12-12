using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ResponseNode : DialogueNodeBase
{
    [Input(typeConstraint = TypeConstraint.Strict)] public int entry;
    [Output(typeConstraint = TypeConstraint.Strict, connectionType = ConnectionType.Override)] public bool exit;

    public override string getDialogueType { get { return "Response"; } }
}