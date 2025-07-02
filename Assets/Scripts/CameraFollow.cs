using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player Player;

    public Vector3 baseOffset = new Vector3(0f, 1.5f, -10f);

    public float followSpeed = 5f;
    public float verticalSmoothTime = 0.15f;
    public float anticipationDistance = 2f;
    public float anticipationSpeed = 2f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 currentAnticipation = Vector3.zero;

    private Vector3 lastTargetPosition;

    void Start()
    {
        Player = FindObjectOfType<Player>();
        if (Player != null)
            lastTargetPosition = Player.transform.position;
    }

    void LateUpdate()
    {
        if (Player == null) return;

        Vector3 targetMovement = Player.transform.position - lastTargetPosition;
        Vector3 anticipation = targetMovement.normalized * anticipationDistance;

        currentAnticipation = Vector3.Lerp(currentAnticipation, anticipation, Time.deltaTime * anticipationSpeed);

        Vector3 desiredPosition = Player.transform.position + baseOffset + new Vector3(currentAnticipation.x, 0f, 0f);

        float smoothY = Mathf.SmoothDamp(transform.position.y, desiredPosition.y, ref velocity.y, verticalSmoothTime);
        float smoothX = Mathf.Lerp(transform.position.x, desiredPosition.x, Time.deltaTime * followSpeed);
        float smoothZ = Mathf.Lerp(transform.position.z, desiredPosition.z, Time.deltaTime * followSpeed);

        transform.position = new Vector3(smoothX, smoothY, smoothZ);

        lastTargetPosition = Player.transform.position;
    }
}
