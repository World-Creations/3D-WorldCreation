using System;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public Action OnComplete;

    public string name { get; private set; }
    public string description { get; private set; }

    public readonly List<Objective> _objectives;
    public bool isComplete = false;

    public Quest(string questName, string questDescription, List<Objective> objectives)
    {
        name = questName;
        description = questDescription;
        _objectives = objectives;
    }

    public void CheckCompletion()
    {
        if (isComplete) return;
        foreach (Objective objective in _objectives)
        {
            if (!objective.IsComplete) return;
        }
        isComplete = true;
        OnComplete?.Invoke();
        return;
    }
}
