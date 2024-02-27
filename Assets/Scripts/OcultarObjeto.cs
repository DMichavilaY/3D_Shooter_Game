using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class OcultarObjeto : MonoBehaviour
{
    public GameObject weapon1; // Asigna el objeto de la primera arma desde el Inspector
    public GameObject weapon2; // Asigna el objeto de la segunda arma desde el Inspector

    private bool isWeapon1Visible = false;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Character.Aim.started += _ => ToggleWeaponVisibility();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Función que se ejecutará al presionar el botón de Aim
    private void ToggleWeaponVisibility()
    {
        isWeapon1Visible = !isWeapon1Visible;
        UpdateWeaponVisibility();
    }

    // Actualiza la visibilidad de las armas
    private void UpdateWeaponVisibility()
    {
        weapon1.SetActive(isWeapon1Visible);
        weapon2.SetActive(!isWeapon1Visible);

        Debug.Log("Weapon1 visibility: " + (isWeapon1Visible ? "Visible" : "Not visible"));
        Debug.Log("Weapon2 visibility: " + (!isWeapon1Visible ? "Visible" : "Not visible"));
    }
}