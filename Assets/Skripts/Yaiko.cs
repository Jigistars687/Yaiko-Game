using Unity.Cinemachine;
using UnityEngine;

public class EggMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Параметры")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private int maxJumps = 2;

    [Header("UI и камера")]
    [SerializeField] private GameObject _Setting_Canvas;
    [SerializeField] private Transform _cameraTransform;

    [Header("Jump Effect")]
    [SerializeField] private GameObject Jump_Effect;

    private int jumpCount = 0;
    private TELEPORT_SCRIPTS teleportScripts;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        teleportScripts = GetComponent<TELEPORT_SCRIPTS>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Move()
    {
        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = (forward * moveZ + right * moveX) * moveSpeed;

        rb.linearVelocity = new Vector3(moveDirection.x, rb.linearVelocity.y, moveDirection.z);
    }

    void Jump()
    {
        if (CanJump() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            IncrementJump();
        }
    }

    bool CanJump()
    {
        return jumpCount < maxJumps;
    }

    void IncrementJump()
    {
        jumpCount++;
        if (jumpCount == maxJumps && Jump_Effect != null)
        {
            Jump_Effect.SetActive(true);
        }
    }

    void ResetJumpCount()
    {
        jumpCount = 0;
        if (Jump_Effect != null)
            Jump_Effect.SetActive(false);
    }

    void OnKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0f;
            if (_Setting_Canvas != null)
            {
                _Setting_Canvas.SetActive(true);
            }
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Сброс прыжков только при столкновении с землей
        if (collision.gameObject.tag == "Ground")
        {
            ResetJumpCount();
        }

        // Телепорт только если есть TELEPORT_SCRIPTS и нужный тег
        if (teleportScripts != null)
        {
            teleportScripts.TryTeleport(collision);
        }
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        OnKeyDown();
    }

    void LateUpdate()
    {
        if (Jump_Effect != null)
        {
            Jump_Effect.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
        }
    }
}
