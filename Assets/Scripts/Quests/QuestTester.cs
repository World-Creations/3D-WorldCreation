using System.Collections.Generic;
using UnityEngine;

public class QuestTester : MonoBehaviour
{
    private Objective objective1;
    private Objective objective2;
    private Objective objective3;
    private Quest quest1;

    private Objective objective4;
    private Quest quest2;

    private void Start()
    {
        objective1 = new Objective("test", "Test Objective 1: {0}/{1}", 10);
        GameManager.Instance.AddObjective(objective1);
        objective2 = new Objective("test", "Test Objective 2: {0}/{1}", Random.Range(5, 10));
        GameManager.Instance.AddObjective(objective2);
        objective3 = new Objective("test", "The cool and quirky objective: {0}/{1}", Random.Range(10, 100));
        GameManager.Instance.AddObjective(objective3);
        quest1 = new Quest("First Quest", "Testing is kinda boring", new List<Objective>(){ objective1, objective2, objective3 });
        GameManager.Instance.AddQuest(quest1);

        objective4 = new Objective("test", "The lonely objective: {0}/{1}", Random.Range(25, 50));
        GameManager.Instance.AddObjective(objective4);
        quest2 = new Quest("Second Cooler Quest", "Please work first try :(", new List<Objective>() { objective4 });
        GameManager.Instance.AddQuest(quest2);

        objective4.OnComplete += HideTheBoi;
    }

    private void OnTriggerEnter(Collider other)
    {
        //appleObjective.AddProgress(1);
        GameManager.Instance.AddProgress("test", 1);
    }

    private void HideTheBoi()
    {
        GameManager.Instance.CloseQuest("Second Cooler Quest");
    }
}
