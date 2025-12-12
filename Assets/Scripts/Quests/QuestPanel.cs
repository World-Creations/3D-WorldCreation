using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private ObjectiveDisplay _ObjectiveDisplayPrefab;
    [SerializeField] private QuestDisplay _QuestDisplayPrefab;
    [SerializeField] private Transform _QuestDisplayParent;
    private readonly Dictionary<string, QuestDisplay> _questMap = new();

    private void Start()
    {
        foreach (Quest quest in GameManager.Instance.Quests)
        {
            AddQuest(quest);
        }
        GameManager.Instance.OnQuestAdded += AddQuest;
        GameManager.Instance.OnQuestClosed += HideQuest;
    }

    public void AddQuest(Quest quest)
    {
        QuestDisplay questDisplay = Instantiate(_QuestDisplayPrefab, _QuestDisplayParent);
        questDisplay.Init(quest);
        _questMap.Add(quest.name, questDisplay);
        foreach (Objective objective in quest._objectives)
        {
            ObjectiveDisplay objectiveDisplay = Instantiate(_ObjectiveDisplayPrefab, questDisplay._objectivesDisplay);
            objectiveDisplay.Init(objective);
        }
    }

    public void HideQuest(Quest quest)
    {
        Destroy(_questMap[quest.name]._questDisplay);
    }
}
