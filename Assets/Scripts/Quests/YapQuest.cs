using System.Collections.Generic;
using UnityEngine;

public class YapTester : MonoBehaviour
{
    [SerializeField] DialogueGraph graph;

    private Objective yapYapYap;
    private Quest yappingSession;

    private void OnTriggerEnter(Collider other)
    {
        //appleObjective.AddProgress(1);
        DialogueHandler.Instance.StartConversation(graph, ConversationEnd);
    }

    private void ConversationEnd(string result)
    {
        switch (result)
        {
            case "Accepted":
                yapYapYap = new Objective("YapYapYap", "Yap a bit: {0}/{1}", Random.Range(5, 10));
                GameManager.Instance.AddObjective(yapYapYap);
                yappingSession = new Quest("Yap Session", "Sometimes you just gotta yap a awhile!", new List<Objective>() { yapYapYap });
                GameManager.Instance.AddQuest(yappingSession);
                break;
            case "Completed":
                GameManager.Instance.CloseQuest(yappingSession.name);
                break;
        }
    }
}
