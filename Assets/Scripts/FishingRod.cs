using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform rodTip;
    [SerializeField] private Transform hookTip;
    [SerializeField] private GameObject hook;

    private LineRenderer lineRenderer;
    private Rigidbody rigidbody;
    private bool casted = false;
    private bool hooked = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = hookTip.GetComponent<LineRenderer>();
        rigidbody = hook.GetComponent<Rigidbody>();
        hookTip.GetComponent<HookTrigger>().OnHookTriggered += FishBite;
        FishingHandler.Instance.FishCaught += FishResult;
    }

    // Update is called once per frame
    void Update()
    {
        if (hook.activeSelf)
        {
            lineRenderer.SetPosition(0, rodTip.position);
            lineRenderer.SetPosition(1, hookTip.position);
        }
    }

    public void CastRod()
    {
        if (hooked) return;
        if (casted)
        {
            casted = false;
            hook.SetActive(false);
            rigidbody.linearVelocity = Vector3.zero;
        }
        else
        {
            casted = true;
            hook.SetActive(true);
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            hook.transform.position = rodTip.position;
            rigidbody.AddForce(player.forward * 200 + Vector3.up * 20);
        }
    }

    private void FishBite(Collider other)
    {
        if (other.gameObject.layer != 4 || !casted || hooked) return; // Must be water
        hooked = true;

        rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        FishNode fish = FishingHandler.Instance.CatchFish();
        // TODO: Place Sprite in 3D world at hook location
        FishingHandler.Instance.ReelMinigame(fish);
    }

    private void FishResult()
    {
        casted = false;
        hooked = false;
        hook.SetActive(false);
        rigidbody.linearVelocity = Vector3.zero;
    }
}