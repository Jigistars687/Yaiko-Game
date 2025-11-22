//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class camera : MonoBehaviour
//{
//    [Header("Ссылки")]
//    public Transform playerTransform;   // Переименовали поле, чтобы не было конфликта
//    public LayerMask obstacleMask;      // Слои, которые считаются препятствиями

//    [Header("Настройки камеры")]
//    public Vector3 offset = new Vector3(0f, 2f, -4f);
//    public float smoothSpeed = 10f;
//    public float minDistance = 0.3f;
//    public float sphereCastRadius = 0.2f;

//    private float desiredDistance;    // Базовая дистанция по Z (abs(offset.z))
//    private float currentDistance;    // Текущая дистанция (для коллизий)
//    private Transform pivot;            // “Пустышка”-pivot, привязанная к игроку

//    void Start()
//    {
//        if (playerTransform == null)
//        {
//            Debug.LogError("ThirdPersonCamera: в поле playerTransform не задан игрок (яйцо).");
//            enabled = false;
//            return;
//        }

//        // Инициализируем дистанцию
//        desiredDistance = Mathf.Abs(offset.z);
//        currentDistance = desiredDistance;

//        // Создаём pivot как дочерний к игроку
//        pivot = new GameObject("CameraPivot").transform;
//        pivot.SetParent(playerTransform);
//        pivot.localPosition = Vector3.zero;
//        pivot.localRotation = Quaternion.identity;

//        // Переносим камеру внутрь pivot и задаём стартовую позицию
//        transform.SetParent(pivot);
//        transform.localPosition = new Vector3(0f, offset.y, -desiredDistance);
//        transform.localRotation = Quaternion.identity;
//    }

//    void LateUpdate()
//    {
//        if (playerTransform == null)
//            return;

//        // 1) Здесь можно добавить управление поворотом pivot (через мышку):
//        // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//        // float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;
//        // pivot.Rotate(Vector3.up,   mouseX, Space.World);
//        // pivot.Rotate(Vector3.right, mouseY, Space.Self);

//        // 2) Вычисляем «желаемую» мировую позицию камеры без учёта коллизий:
//        Vector3 desiredCameraLocalPos = new Vector3(0f, offset.y, -desiredDistance);
//        Vector3 worldDesiredPos = pivot.TransformPoint(desiredCameraLocalPos);

//        // 3) Проверяем препятствия между «глазом» и желаемой позицией камеры
//        RaycastHit hit;
//        Vector3 fromPosition = playerTransform.position + Vector3.up * offset.y; // точка “глаза”
//        Vector3 direction = (worldDesiredPos - fromPosition).normalized;
//        float maxCheckDistance = desiredDistance + 0.1f;

//        if (Physics.SphereCast(fromPosition, sphereCastRadius, direction, out hit, maxCheckDistance, obstacleMask))
//        {
//            // Есть препятствие: вычисляем новую дистанцию чуть перед стеной
//            float hitDist = hit.distance - 0.05f;
//            currentDistance = Mathf.Clamp(hitDist, minDistance, desiredDistance);
//        }
//        else
//        {
//            // Препятствий нет, постепенно возвращаем камеру на исходную дистанцию
//            currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * smoothSpeed);
//        }

//        // 4) Устанавливаем новую локальную позицию камеры внутри pivot
//        Vector3 newCamLocalPos = new Vector3(0f, offset.y, -currentDistance);
//        transform.localPosition = newCamLocalPos;
//        transform.LookAt(playerTransform.position + Vector3.up * (offset.y * 0.5f));
//    }

//    /// <summary>
//    /// Вызывается из EggMovement при финише уровня, чтобы остановить
//    /// движение камеры (или запустить кастомный переход, если нужно).
//    /// </summary>
//    public void FinishCameraTransition()
//    {
//        // Отключаем LateUpdate, чтобы камера больше не двигалась
//        enabled = false;

//        // Здесь можно добавить любую анимацию/переход камеры:
//        // Например:
//        // transform.parent = null;
//        // Vector3 endPos = new Vector3(10f, 5f, -10f);
//        // Quaternion endRot = Quaternion.Euler(30f, 45f, 0f);
//        // StartCoroutine(MoveCameraTo(endPos, endRot, 2f));
//    }

//    // Пример корутины для плавного перемещения камеры (по желанию)
//    /*
//    private IEnumerator MoveCameraTo(Vector3 pos, Quaternion rot, float duration)
//    {
//        Vector3 startPos = transform.position;
//        Quaternion startRot = transform.rotation;
//        float elapsed = 0f;
//        while (elapsed < duration)
//        {
//            float t = elapsed / duration;
//            transform.position = Vector3.Lerp(startPos, pos, t);
//            transform.rotation = Quaternion.Slerp(startRot, rot, t);
//            elapsed += Time.deltaTime;
//            yield return null;
//        }
//        transform.position = pos;
//        transform.rotation = rot;
//    }
//    */
//}