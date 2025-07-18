using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // jogador
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f); // z = -10 para a câmera 2D

    [SerializeField] private float smoothSpeed = 5f; // suavidade opcional

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }
}