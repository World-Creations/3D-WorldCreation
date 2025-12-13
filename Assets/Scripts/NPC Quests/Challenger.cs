using System.Collections.Generic;
using UnityEngine;

public class Challenger : MonoBehaviour
{
    [SerializeField] DialogueGraph graph;

    private Objective challengerObjective;
    private Quest challenger;

    private Objective whatObjective1;
    private Objective whatObjective2;
    private Quest whatChallenge;

    private Objective hardcoreObjective1;
    private Objective hardcoreObjective2;
    private Objective hardcoreObjective3;
    private Objective hardcoreObjective4;
    private Quest hardcore;

    private Objective finalObjective;
    private Quest final;

    private void OnTriggerEnter(Collider other)
    {
        DialogueHandler.Instance.StartConversation(graph, ConversationEnd);
    }

    private void ConversationEnd(string result)
    {
        switch (result)
        {
            case "Accepted":
                challengerObjective = new Objective("CatchDootler", "Catch a Dootler: {0}/{1}", 1);
                GameManager.Instance.AddObjective(challengerObjective);
                challenger = new Quest("Challenger", "Everyone loves a challenge.", new List<Objective>() { challengerObjective });
                GameManager.Instance.AddQuest(challenger);
                break;
            case "AcceptedAgain":
                GameManager.Instance.CloseQuest(challenger.name);
                whatObjective1 = new Objective("CatchDootler", "Catch a Dootler: {0}/{1}", 3);
                GameManager.Instance.AddObjective(whatObjective1);
                whatObjective2 = new Objective("CatchDootler (Doodled)", "Catch a Dootler (Doodled): {0}/{1}", 1);
                GameManager.Instance.AddObjective(whatObjective2);
                whatChallenge = new Quest("What Challenge?", "It's almost too easy.", new List<Objective>() { whatObjective1, whatObjective2 });
                GameManager.Instance.AddQuest(whatChallenge);
                break;
            case "Hardcore":
                GameManager.Instance.CloseQuest(whatChallenge.name);
                hardcoreObjective1 = new Objective("CatchLeadLooper", "Catch a LeadLooper: {0}/{1}", 10);
                GameManager.Instance.AddObjective(hardcoreObjective1);
                hardcoreObjective2 = new Objective("CatchLeadLooper (Albino)", "Catch a LeadLooper (Albino): {0}/{1}", 5);
                GameManager.Instance.AddObjective(hardcoreObjective2);
                hardcoreObjective3 = new Objective("CatchSockrates", "Catch a Sockrates: {0}/{1}", 3);
                GameManager.Instance.AddObjective(hardcoreObjective3);
                hardcoreObjective4 = new Objective("CatchSockrates (Freshwater)", "Catch a Sockrates (Freshwater): {0}/{1}", 1);
                GameManager.Instance.AddObjective(hardcoreObjective4);
                hardcore = new Quest("Hardcore Challenger", "The grind never stops.", new List<Objective>() { hardcoreObjective1, hardcoreObjective2, hardcoreObjective3, hardcoreObjective4 });
                GameManager.Instance.AddQuest(hardcore);
                break;
            case "Final":
                finalObjective = new Objective("CatchDopefish", "Catch a Dopefish: {0}/{1}", 1);
                GameManager.Instance.AddObjective(finalObjective);
                final = new Quest("Final Challenge", "You didn't ask for this.", new List<Objective>() { finalObjective });
                GameManager.Instance.AddQuest(final);
                break;
            case "FinalDone":
                break;
        }
    }
}
