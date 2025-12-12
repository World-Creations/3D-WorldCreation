using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using XNode;

public class QuestionNode : DialogueNodeBase
{
    [Input(typeConstraint = TypeConstraint.Strict)] public bool entry;
    [Output(typeConstraint = TypeConstraint.Strict, dynamicPortList = true, connectionType = ConnectionType.Override)] public int exit;
    public override string getDialogueType { get { return "Question"; } }
}