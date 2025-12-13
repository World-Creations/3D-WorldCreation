using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CircleControlMinigame : MonoBehaviour
{
    [SerializeField] RectTransform controlPanel;
    [SerializeField] RectTransform fishBar;
    [SerializeField] TextMeshProUGUI progressText;
    [SerializeField] RectTransform progressBar;

    private float _control = 0.3f;
    private float _resilience = 50f;
    private float _progressSpeed = 10f;

    private float progress = 20f;
    private float velocity = 0f;

    private float lastMove;
    private float trueResilience;
    private float maxMove;
    private float moveTimeMin;
    private float moveTimeMax;

    private float moveGoal = 180f;
    private float moveTime = 1f;
    private float moveInitial = 180f;
    private float moveStart = 0f;

    private FishNode _fish;

    public void StartMinigame(FishNode fish)
    {
        _fish = fish;
        //_control = control;
        _resilience = fish.resilience;
        _progressSpeed = fish.progressModifier;

        if (_progressSpeed > 0f)
        {
            progressText.text = "+" + _progressSpeed + "% Progress Speed";
        }
        else if (_progressSpeed < 0f)
        {
            progressText.text = _progressSpeed + "% Progress Speed";
        }
        else
        {
            progressText.enabled = false;
        }

        lastMove = Time.time;
        trueResilience = Mathf.Max(0.2f, _resilience / 100);
        maxMove = 0.4f * Mathf.Clamp(trueResilience, 0.8f, 1.2f);
        moveTimeMin = 1.3f * trueResilience;
        moveTimeMax = 3.5f * trueResilience;
        controlPanel.GetComponent<Image>().fillAmount = _control;
        controlPanel.transform.rotation = Quaternion.Euler(0, 0, 180f + _control * 180f);
    }

    // Update is called once per frame
    void Update()
    {
        float moveAttempt = 2 * trueResilience + 3.1f * trueResilience * Random.value;
        if (Time.time - lastMove > moveAttempt)
        {
            lastMove = Time.time;
            float move = (Random.value * 2f - 1f) * maxMove * 360;
            float position = fishBar.transform.rotation.eulerAngles.z;
            moveInitial = position;
            moveGoal = position + move;
            moveTime = Mathf.Clamp(Random.value * (moveTimeMax - moveTimeMin) + moveTimeMin, 0.1f, 1.5f);
            moveStart = Time.time;
        }
        //fishBar.transform.localPosition = new Vector3(Mathf.Sin(Time.time) * controlBarSize, 0, 0); // Temp for positional testing
        fishBar.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(moveInitial, moveGoal, (Time.time-moveStart)/moveTime));

        if (Mouse.current.leftButton.isPressed)
        {
            velocity -= Time.deltaTime;
        }
        else
        {
            velocity += Time.deltaTime;
        }
        velocity = Mathf.Clamp(velocity, -.5f, .5f);
        float finalPosition = (controlPanel.transform.rotation.eulerAngles.z + velocity) % 360;
        controlPanel.transform.rotation = Quaternion.Euler(0f, 0f, finalPosition);

        float fishPosition = fishBar.transform.rotation.eulerAngles.z % 360;
        if ((finalPosition - _control * 360 <= fishPosition && finalPosition >= fishPosition) || (fishPosition <= finalPosition + 360 && fishPosition >= finalPosition - _control * 360 + 360))
        {
            progress += Time.deltaTime * 6.8f * (1f + _progressSpeed / 100f);
            controlPanel.transform.GetComponent<Image>().color = Color.white;
        }
        else
        {
            if (_progressSpeed > 0f)
            {
                progress -= Time.deltaTime * 6.8f / (1f + _progressSpeed / 100f);
            }
            else
            {
                progress -= Time.deltaTime * 6.8f * (1f - _progressSpeed / 100f);
            }
            controlPanel.transform.GetComponent<Image>().color = new Color(0.75f, 0.75f, 0.75f, 1f);
        }
        progressBar.localScale = new Vector3(Mathf.Clamp(progress/100f, 0f, 1f), 1f, 0f);
        if (progress <= 0 || progress >= 100)
        {
            FishingHandler.Instance.CatchResult(_fish, progress >= 100);
            Destroy(gameObject);
        }
    }
}
