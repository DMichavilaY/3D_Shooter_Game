using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Contador : MonoBehaviour
{
    public float totalTime = 60f;
    private float currentTime;

    public TextMeshProUGUI countdownText;
    public GameManager gameManager;

    private bool isTimerRunning = true;

    public void Start()
    {
        currentTime = totalTime;
    }

    public void Update()
    {
        if (isTimerRunning)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                // Llama al método RestartGame solo si el temporizador está en funcionamiento
                if (gameManager != null)
                {
                    gameManager.RestartGame();
                }
            }
        }
    }

    public void ResetTimer()
    {
        currentTime = totalTime;
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (countdownText != null)
        {
            countdownText.text = Mathf.CeilToInt(currentTime).ToString();
        }
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }
}