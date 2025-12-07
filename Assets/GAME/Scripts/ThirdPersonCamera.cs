//using UnityEngine;

//public class ThirdPersonCamera : MonoBehaviour
//{
//    public Transform target;
//    public float distance = 5f;
//    public float sensitivity = 2f;

//    [Header("Collision Settings")]
//    public float sphereCastRadius = 0.2f;
//    public float minDistance = 0.5f;
//    public LayerMask collisionLayers = ~0;

//    private float rotationY = 0f;
//    private float rotationX = 0f;
//    private bool finished = false;

//    public Vector3 finishPosition = new Vector3(58.57f, 1.4f, -64.55f);
//    public Quaternion finishRotation = Quaternion.Euler(0, 0, 0);

//    void Start()
//    {
//        Vector3 angles = transform.eulerAngles;
//        rotationY = angles.x;
//        rotationX = angles.y;
//    }

//    void Update()
//    {
//        if (!finished)
//        {
//            RotateCamera();
//            UpdatePosition();
//        }
//    }

//    void RotateCamera()
//    {
//        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
//        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

//        rotationX += mouseX;
//        rotationY -= mouseY;
//        rotationY = Mathf.Clamp(rotationY, -80f, 80f);
//    }

//    void UpdatePosition()
//    {

//        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
//        Vector3 desiredOffset = rotation * new Vector3(0, 0, -distance);
//        Vector3 desiredPosition = target.position + desiredOffset;
//        float rayStartHeight = 0.5f;
//        Vector3 rayOrigin = target.position + Vector3.up * rayStartHeight;

//        Vector3 direction = (desiredPosition - rayOrigin).normalized;
//        float maxDistance = distance + rayStartHeight;

//        RaycastHit hit;
//        float adjustedDistance = distance;

//        if (Physics.SphereCast(rayOrigin, sphereCastRadius, direction, out hit, maxDistance, collisionLayers))
//        {
//            float distanceFromTargetToHit = hit.distance - rayStartHeight;
//            float padding = 0.1f;
//            adjustedDistance = Mathf.Clamp(distanceFromTargetToHit - padding, minDistance, distance);
//        }
//        Vector3 finalOffset = rotation * new Vector3(0, 0, -adjustedDistance);
//        transform.position = target.position + finalOffset;
//        transform.LookAt(target);
//    }

//    public void FinishCameraTransition()
//    {
//        finished = true;
//        transform.position = finishPosition;
//        transform.rotation = finishRotation;
//    }
//}
