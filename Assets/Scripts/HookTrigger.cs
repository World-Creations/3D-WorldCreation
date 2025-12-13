using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    public System.Action<Collider> OnHookTriggered;

    private void OnTriggerEnter(Collider other)
    {
        OnHookTriggered?.Invoke(other);
    }
}
