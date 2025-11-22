using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float sensitivity = 2f;

    [Header("Collision Settings")]
    // –адиус сферы дл€ проверки, чтобы камера не "ныр€ла" в узкие щели
    public float sphereCastRadius = 0.2f;
    // ћинимальное рассто€ние от цели, на котором камера может оказатьс€
    public float minDistance = 0.5f;
    // —лой(и), по которым мы хотим провер€ть преп€тстви€ дл€ камеры
    public LayerMask collisionLayers = ~0; // по умолчанию Ч все слои

    private float rotationY = 0f;
    private float rotationX = 0f;
    private bool finished = false;

    public Vector3 finishPosition = new Vector3(58.57f, 1.4f, -64.55f);
    public Quaternion finishRotation = Quaternion.Euler(0, 0, 0);

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationY = angles.x;
        rotationX = angles.y;
    }

    void Update()
    {
        if (!finished)
        {
            RotateCamera();
            UpdatePosition();
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -80f, 80f);
    }

    void UpdatePosition()
    {
        // 1) ¬ычисл€ем желаемую позицию камеры без учЄта коллизий
        Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
        Vector3 desiredOffset = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = target.position + desiredOffset;

        // 2) —мещаем точку старта SphereCast чуть вверх, чтобы не оказатьс€ "внутри" пола
        //    (например, если персонаж стоит ровно на земле, target.y == уровень пола).
        float rayStartHeight = 0.5f; // подберите под ваш персонаж (0.5 м над pivot'ом)
        Vector3 rayOrigin = target.position + Vector3.up * rayStartHeight;

        // 3) Ќаправление от смещЄнной точки к желаемой позиции
        Vector3 direction = (desiredPosition - rayOrigin).normalized;
        float maxDistance = distance + rayStartHeight;
        // ƒобавл€ем rayStartHeight, чтобы полностью покрыть рассто€ние от цели (нижней точки) до камеры.

        // ¬ыполн€ем SphereCast с радиусом sphereCastRadius.
        RaycastHit hit;
        float adjustedDistance = distance; // итоговое рассто€ние от target.position

        if (Physics.SphereCast(rayOrigin, sphereCastRadius, direction, out hit, maxDistance, collisionLayers))
        {
            // hit.distance Ч рассто€ние от rayOrigin до точки удара.
            // „тобы найти, на каком рассто€нии от target.position должна встать камера:
            float distanceFromTargetToHit = hit.distance - rayStartHeight;
            // ƒобавл€ем небольшой буфер (padding), чтобы камера не застревала Ђв упорї.
            float padding = 0.1f;
            adjustedDistance = Mathf.Clamp(distanceFromTargetToHit - padding, minDistance, distance);
        }

        // 4) ”станавливаем окончательное смещение камеры по вращению, но по укороченному рассто€нию
        Vector3 finalOffset = rotation * new Vector3(0, 0, -adjustedDistance);
        transform.position = target.position + finalOffset;
        transform.LookAt(target);
    }

    public void FinishCameraTransition()
    {
        finished = true;
        transform.position = finishPosition;
        transform.rotation = finishRotation;
    }
}
