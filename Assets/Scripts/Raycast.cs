using UnityEngine;
using UnityEngine.InputSystem;

public class Raycast : MonoBehaviour
{
    public Transform raycastOrigin;
    public float shootForce = 10f;
    public AudioSource gunshotAudio;
    public GameObject explosionPrefab;  // Asigna el prefab de explosión desde el Inspector

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Shoot.performed += _ => Shoot();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit))
        {
            Debug.Log("Object detected: " + hit.collider.gameObject.name);

            // Verifica si el objeto impactado es un barril
            if (hit.collider.CompareTag("Barrel1"))
            {
                HandleBarrelHit(hit.collider.gameObject);
            }
            else
            {
                // Si no es un barril, realiza el comportamiento normal (añade fuerza, reproduce sonido, etc.)
                Rigidbody hitRigidbody = hit.collider.GetComponent<Rigidbody>();
                if (hitRigidbody != null)
                {
                    Vector3 shootDirection = hit.point - raycastOrigin.position;
                    hitRigidbody.AddForce(shootDirection.normalized * shootForce, ForceMode.Impulse);
                }
            }
        }
        else
        {
            Debug.Log("No object detected.");
        }

        // Reproduce el sonido de disparo
        if (gunshotAudio != null)
        {
            gunshotAudio.Play();
        }
    }

    private void HandleBarrelHit(GameObject barrel)
    {
        // Oculta el barril y muestra la explosión
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, barrel.transform.position, barrel.transform.rotation);
        }

        // Desactiva el barril
        barrel.SetActive(false);
    }
}