using UnityEngine;
using TMPro;

public class QuestDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    [SerializeField] public Transform _objectivesDisplay;

    public GameObject _questDisplay;

    private Quest _quest;
    public void Init(Quest quest)
    {
        _quest = quest;
        _titleText.text = quest.name;
        _descriptionText.text = quest.description;
        _questDisplay = gameObject;
        quest.OnComplete += OnQuestComplete;
        foreach (Objective objective in quest._objectives)
        {
            objective.OnComplete += quest.CheckCompletion;
        }
    }

    private void OnQuestComplete()
    {
        _titleText.text = $"<color=green>{_quest.name}</color>";
    }
}
