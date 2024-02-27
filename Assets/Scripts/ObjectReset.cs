using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    [HideInInspector]
    public Vector3 originalPosition;

    private void Start()
    {
        // Almacena la posición original al inicio
        originalPosition = transform.position;
    }
}
