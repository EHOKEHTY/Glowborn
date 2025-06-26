using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Цель")]
    public Transform target;

    [Header("Смещение")]
    public Vector3 baseOffset = new Vector3(0f, 1.5f, -10f); // Смещение относительно цели

    [Header("Параметры сглаживания")]
    public float followSpeed = 5f;            // Общая скорость перемещения камеры
    public float verticalSmoothTime = 0.15f;  // Сглаживание по Y
    public float anticipationDistance = 2f;   // Насколько далеко камера "опережает" цель
    public float anticipationSpeed = 2f;      // Насколько быстро камера опережает движение

    private Vector3 velocity = Vector3.zero;
    private Vector3 currentAnticipation = Vector3.zero;

    private Vector3 lastTargetPosition;

    void Start()
    {
        if (target != null)
            lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetMovement = target.position - lastTargetPosition;
        Vector3 anticipation = targetMovement.normalized * anticipationDistance;

        // Плавно изменяем текущий "забег вперёд"
        currentAnticipation = Vector3.Lerp(currentAnticipation, anticipation, Time.deltaTime * anticipationSpeed);

        // Желаемая позиция: цель + базовое смещение + упреждение (только по X)
        Vector3 desiredPosition = target.position + baseOffset + new Vector3(currentAnticipation.x, 0f, 0f);

        // Сглаживание вертикальной оси отдельно
        float smoothY = Mathf.SmoothDamp(transform.position.y, desiredPosition.y, ref velocity.y, verticalSmoothTime);

        // Сглаживание позиции по X и Z
        float smoothX = Mathf.Lerp(transform.position.x, desiredPosition.x, Time.deltaTime * followSpeed);
        float smoothZ = Mathf.Lerp(transform.position.z, desiredPosition.z, Time.deltaTime * followSpeed);

        transform.position = new Vector3(smoothX, smoothY, smoothZ);

        lastTargetPosition = target.position;
    }
}
