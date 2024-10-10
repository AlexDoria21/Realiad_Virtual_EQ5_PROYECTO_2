using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Para la UI del cronómetro

public class TimeChallenge : MonoBehaviour
{
    public float timeLimit = 20f;
    private float timeRemaining;

    // Referencia a la UI del cronómetro
    public TextMeshProUGUI timerText;

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerText();
    }

    void Update()
    {
        // Actualizar cronómetro
        timeRemaining -= Time.deltaTime;
        UpdateTimerText();

        if (timeRemaining <= 0)
        {
            // Reiniciar la escena si el tiempo se agota
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Método para actualizar el cronómetro en pantalla
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60F);
        int seconds = Mathf.FloorToInt(timeRemaining % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // Detectar cuando el objeto que estamos empujando llega a la plataforma final
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjMov")) // Verifica si el objeto es el correcto
        {
            // El objeto llegó a la plataforma final, cargar la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
