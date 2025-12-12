using UnityEngine;
using System;

public class Objective
{
    public Action OnComplete;
    public Action OnValueChange;

    public string Identifier { get; private set; }
    public bool IsComplete { get; private set; }
    public bool IsHidden {  get; private set; }
    public int MaxValue { get; private set; }
    public float CurrentValue { get; private set; }

    private readonly string _statusText;

    public Objective(string identifier, string statusText, int maxValue)
    {
        Identifier = identifier;
        _statusText = statusText;
        MaxValue = maxValue;
    }

    private void CheckCompletion()
    {
        if (CurrentValue >= MaxValue)
        {
            IsComplete = true;
            OnComplete?.Invoke();
        }
    }

    public void AddProgress(float value)
    {
        if (IsComplete) return;

        CurrentValue += value;
        if (CurrentValue > MaxValue)
        {
            CurrentValue = MaxValue;
        }
        OnValueChange?.Invoke();
        CheckCompletion();
    }

    public string GetStatusText()
    {
        return string.Format(_statusText, Math.Floor(CurrentValue), MaxValue);
    }
}
