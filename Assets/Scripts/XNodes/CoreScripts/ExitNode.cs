using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using XNode;

public class ExitNode : CoreNodeBase {
    public string result;
    [Input(typeConstraint = TypeConstraint.Strict)] public bool entry;
}