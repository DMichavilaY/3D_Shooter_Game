using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerStartPosition;
    public Contador countdownTimer;

    private Transform[] resettableObjects;

    private void Start()
    {
        if (countdownTimer == null)
        {
            countdownTimer = FindObjectOfType<Contador>();
        }

        // Obtén y almacena las posiciones originales de todos los objetos reseteables
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        resettableObjects = new Transform[allObjects.Length];
        for (int i = 0; i < allObjects.Length; i++)
        {
            resettableObjects[i] = allObjects[i].transform;
        }
    }

    public void RestartGame()
    {
        // Reinicia la posición del jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && playerStartPosition != null)
        {
            player.transform.position = playerStartPosition.position;
        }

        // Restaura la posición original de todos los objetos reseteables
        foreach (Transform obj in resettableObjects)
        {
            ResetObjectPosition(obj);
        }
    }

    private void ResetObjectPosition(Transform obj)
    {
        // Verifica que el objeto y el componente ObjectReset no sean nulos
        if (obj != null && obj.TryGetComponent<ObjectReset>(out ObjectReset objectReset))
        {
            obj.position = objectReset.originalPosition;
        }
    }
}
