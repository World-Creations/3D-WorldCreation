using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XNode;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    #region Objective Handler
    public Action<Objective> OnObjectiveAdded;

    public List<Objective> Objectives { get; } = new();
    private readonly Dictionary<string, List<Objective>> _objectiveMap = new();

    public void AddObjective(Objective objective)
    {
        if (!string.IsNullOrEmpty(objective.Identifier))
        {
            if (!_objectiveMap.ContainsKey(objective.Identifier))
            {
                _objectiveMap.Add(objective.Identifier, new());
            }
            _objectiveMap[objective.Identifier].Add(objective);
        }

        OnObjectiveAdded?.Invoke(objective);
        Debug.Log("Objective Added");
    }

    public void AddProgress(string identifier, float value)
    {
        if (!_objectiveMap.ContainsKey(identifier)) return;
        foreach (var objective in _objectiveMap[identifier])
            objective.AddProgress(value);
        Debug.Log("Objective Progress Made");
    }
    #endregion

    #region Quest Handler
    public Action<Quest> OnQuestAdded;
    public Action<Quest> OnQuestClosed;

    public List<Quest> Quests = new();
    private readonly Dictionary<string, Quest> _questMap = new();

    public void AddQuest(Quest quest)
    {
        if (!string.IsNullOrEmpty(quest.name))
        {
            if (!_questMap.ContainsKey(quest.name))
            {
                Quests.Add(quest);
                _questMap.Add(quest.name, quest);
                OnQuestAdded?.Invoke(quest);
                Debug.Log("Quest Added");
            }
        }
    }

    public bool? IsQuestComplete(string name)
    {
        if (!_questMap.ContainsKey(name)) return null; // Quest not started: null
        return _questMap[name].isComplete;
    }

    public bool IsQuestClosed(string name)
    {
        return _questMap.ContainsKey(name) && _questMap[name].closed;
    }

    public void CloseQuest(string name)
    {
        if (!_questMap.ContainsKey(name)) return; // Can't close a quest that was never opened
        _questMap[name].closed = true;
        OnQuestClosed?.Invoke(_questMap[name]);
    }
    #endregion
}
