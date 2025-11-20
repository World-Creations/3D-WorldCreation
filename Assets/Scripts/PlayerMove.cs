using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove: MonoBehaviour
{
    [SerializeField] GameObject feetCollider;

    public float speed, sensitivity, maxForce;
    public float jumpForce;

    private float lookRot;
    private Vector2 move, look;
    private Rigidbody rb;
    private Camera cam;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {

        Vector3 currVelocity = rb.linearVelocity;
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y) * speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 VelocityChange = targetVelocity - currVelocity;
        Vector3.ClampMagnitude(VelocityChange, maxForce);

        rb.AddForce(new Vector3(VelocityChange.x, 0, VelocityChange.z), ForceMode.VelocityChange);
    }

    private void LateUpdate()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(Vector3.up * look.x * sensitivity);

            lookRot += (-look.y * sensitivity);
            lookRot = Mathf.Clamp(lookRot, -90, 90);

            cam.transform.eulerAngles = new
                Vector3(lookRot, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        }
    }

    private bool IsGrounded()
    {
        Collider feet = feetCollider.GetComponent<Collider>();
        Collider[] hits = Physics.OverlapBox(
            feet.bounds.center,
            feet.bounds.extents,
            feet.transform.rotation
        );

        foreach (var hit in hits)
        {
            if (!hit.isTrigger && hit.gameObject.tag != "Player" && hit.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
                return true;
        }

        return false;
    }
}
