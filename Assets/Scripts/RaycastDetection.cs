using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastDetection : MonoBehaviour
{
    public string targetTag = "Player";
    public GameObject impactPrefab;
    public bool shrinkOnImpact = true;
    public float impactDuration = 1.0f;

    private bool hasExecutedImpact = false;
    private GameUI gameUI;

    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !hasExecutedImpact)
        {
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                HandleImpact(hit.point);
                UpdateGameUI(hit.collider.gameObject);
            }
        }
    }

    private void HandleImpact(Vector3 impactPosition)
    {
        if (impactPrefab != null)
        {
            GameObject impactInstance = Instantiate(impactPrefab, impactPosition, Quaternion.identity);
            Destroy(impactInstance, impactDuration);
        }

        if (shrinkOnImpact)
        {
            transform.localScale *= 0.0f;
        }
        else
        {
            gameObject.SetActive(false);
        }

        hasExecutedImpact = true;
    }

    private void UpdateGameUI(GameObject hitObject)
    {
        if (hitObject.CompareTag("Crate") || hitObject.CompareTag("Crate1") || hitObject.CompareTag("Crate2") || hitObject.CompareTag("Crate3") || hitObject.CompareTag("Crate4"))
        {
            gameUI.CajaDestruida();
        }
        else if (hitObject.CompareTag("Box") || hitObject.CompareTag("Box1") || hitObject.CompareTag("Box2") || hitObject.CompareTag("Box3"))
        {
            gameUI.CajaMaderaDestruida();
        }
    }
}
