using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI cajasText;
    public TextMeshProUGUI cajasMaderaText;
    public GameObject escaleras;

    private int cajasRestantes = 5; // Puedes ajustar según tus necesidades
    private int cajasMaderaRestantes = 4; // Puedes ajustar según tus necesidades

    private void Start()
    {
        UpdateUI();
    }

    public void CajaDestruida()
    {
        cajasRestantes--;
        UpdateUI();
    }

    public void CajaMaderaDestruida()
    {
        cajasMaderaRestantes--;
        UpdateUI();
    }

    private void UpdateUI()
    {
        cajasText.text = "Quedan " + cajasRestantes + " cajas";
        cajasMaderaText.text = "Quedan " + cajasMaderaRestantes + " cajas de madera";

        if (cajasRestantes == 0 && cajasMaderaRestantes == 0)
        {
            // Activa las escaleras cuando se cumplan las condiciones
            escaleras.SetActive(true);
        }
    }
}
