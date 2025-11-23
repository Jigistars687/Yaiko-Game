using Unity.Cinemachine;
using UnityEngine;

public class EggMovement : MonoBehaviour
{

    private Rigidbody rb;
    private int jumpCount = 0;
    private int maxJumps = 2;
    private bool finished = false;


    [Header("Движение")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private GameObject Jump_Effect;

    [Header("Точки телепортации")]
    [SerializeField] private Transform _nest;
    [SerializeField] private Transform _Finish_Tag;
    [SerializeField] private Transform _Lava_TP;
    [SerializeField] private Transform _Finish;
    [SerializeField] private Transform _finishCameraTransform;

    [Header("UI и камера")]
    [SerializeField] private GameObject _Setting_Canvas;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Camera _camera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

    void Jump()
    {
        if (jumpCount < maxJumps && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            jumpCount++;
            if (jumpCount == maxJumps)
            {
                Jump_Effect.SetActive(true);
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Ground":
                jumpCount = 0;
                Jump_Effect.SetActive(false);
                break;
            case "nest":
                transform.position = _nest.position;
                rb.linearVelocity = Vector3.zero;
                break;
            case "Finish Tag":
                transform.position = _Finish_Tag.position;
                rb.linearVelocity = Vector3.zero;
                break;
            case "Lava":
                transform.position = _Lava_TP.position;
                rb.linearVelocity = Vector3.zero;
                break;
            case "Finish":
                finished = true;
                transform.position = _Finish.position;
                rb.linearVelocity = Vector3.zero;



                
                if (_camera != null)
                {
                    var cinemachineBrain = _camera.GetComponent<CinemachineBrain>();
                    if (cinemachineBrain != null)
                    {
                        cinemachineBrain.enabled = false;
                    }
                    _camera.transform.position = _finishCameraTransform.position;
                    _camera.transform.rotation = _finishCameraTransform.rotation;
                }
                break;
        }
    }

    void Update()
    {
        Move();
        Jump();
        OnKeyDown();
    }
    void LateUpdate()
    {
        Jump_Effect.transform.rotation = Quaternion.LookRotation(Vector3.down, Vector3.forward);
    }
}