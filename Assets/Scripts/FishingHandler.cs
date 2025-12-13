using System;
using UnityEngine;

public class FishingHandler : MonoBehaviour
{
    [SerializeField] FishGraph graph;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject reelMinigamePrefab;

    public static FishingHandler Instance;
    public Action FishCaught;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    public FishNode CatchFish()
    {
        int totalWeight = 0;

        // Sum all weights
        foreach (FishNode fish in graph.nodes)
            totalWeight += fish.chance;

        // Pick random number in the total weight range
        int roll = UnityEngine.Random.Range(0, totalWeight);

        // Find the fish whose range contains the roll
        int current = 0;
        foreach (FishNode fish in graph.nodes)
        {
            current += fish.chance;

            if (roll < current)
                return fish;
        }

        // Should never happen
        return null;
    }

    public void ReelMinigame(FishNode fish)
    {
        GameObject minigame = Instantiate(reelMinigamePrefab, canvas.transform);
        //minigame.transform.position = Vector3.zero;
        minigame.GetComponent<CircleControlMinigame>().StartMinigame(fish);
    }

    public void CatchResult(FishNode fish, bool caught)
    {
        if (caught)
        {
            GameManager.Instance.AddProgress("CatchFish", 1);
            GameManager.Instance.AddProgress("Catch" + fish.GetName(), 1);
            // TODO: Add fish to inventory or directly deposit money
            DialogueHandler.Instance.NotificationMessage("<color=green>You caught a " + fish.GetName() + "!</color>", fish.sprite);
        }
        else
        {
            DialogueHandler.Instance.NotificationMessage("<color=red>The " + fish.GetName() + " got away...</color>");
        }
        FishCaught?.Invoke();
    }
}
