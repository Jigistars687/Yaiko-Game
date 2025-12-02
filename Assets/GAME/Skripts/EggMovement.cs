using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EggMovement : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Параметры")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float k_KeyHoldTargetTime = 5;

    [Header("UI и камера")]
    [SerializeField] private GameObject _Setting_Canvas;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Camera _camera;

    [Header("Effects")]
    [SerializeField] private GameObject _SoundExplosion;
    [SerializeField] private GameObject Jump_Effect;
    [SerializeField] private GameObject Exploison;

    [Header("Skins")]
    [SerializeField] private GameObject _megaKnight;
    [SerializeField] private GameObject MorgenshternRight;
    [SerializeField] private GameObject MorgenshternLeft;
    [SerializeField] private GameObject BucketHelmet;

    private int jumpCount = 2;
    private float k_KeyHoldTime;
    private bool isKKeyHeld;
    private bool explosionActivated = false;
    private float explosionActivationTime = 0f;
    private bool meshRendererDisabled = false;
    private bool objectDisabled = false;
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

        if ((Input.GetKey(KeyCode.M) & Input.GetKeyDown(KeyCode.K)) || ((Input.GetKey(KeyCode.M) & Input.GetKeyDown(KeyCode.K))))
        {
            _megaKnight.SetActive(true);
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (!isKKeyHeld)
            {
                isKKeyHeld = true;
                k_KeyHoldTime = 0f;
            }
            k_KeyHoldTime += Time.deltaTime;
            if (k_KeyHoldTime >= k_KeyHoldTargetTime)
            {
                if (Exploison != null && !Exploison.activeSelf)
                {
                    _SoundExplosion.SetActive(true);
                    Exploison.SetActive(true);
                    var cinemachineBrain = _camera.GetComponent<CinemachineBrain>();
                    if (cinemachineBrain != null)
                        cinemachineBrain.enabled = false;
                    // Запоминаем время активации взрыва
                    explosionActivated = true;
                    explosionActivationTime = Time.time;
                    meshRendererDisabled = false;
                    objectDisabled = false;
                }
            }
        }
        else
        {
            isKKeyHeld = false;
            k_KeyHoldTime = 0f;
        }
    }


    private void HandleExplosionTimers()
    {
        if (explosionActivated)
        {
            float elapsed = Time.time - explosionActivationTime;
            if (!meshRendererDisabled && elapsed >= 2f)
            {
                var meshRenderer = gameObject.GetComponent<MeshRenderer>();
                var ColiderBucket = BucketHelmet.GetComponent<MeshCollider>();
                if (meshRenderer != null)
                    meshRenderer.enabled = false;
                meshRendererDisabled = true;
                MorgenshternLeft.SetActive(false);
                MorgenshternRight.SetActive(false);
                BucketHelmet.AddComponent<Rigidbody>();
                ColiderBucket.enabled = true;
            }
            if (!objectDisabled && elapsed >= 7f)
            {
                gameObject.SetActive(false);
                _SoundExplosion.SetActive(false);
                Exploison.SetActive(false);
                objectDisabled = true;
            }
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
        //Move();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        OnKeyDown();
        HandleExplosionTimers();
    }
    private void FixedUpdate()
    {
        Move();
    }
    void LateUpdate()
    {
        if (Jump_Effect != null)
        {
            Jump_Effect.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.up);
        }
    }
}
