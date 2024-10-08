using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dinamica2 : MonoBehaviour
{
    public float timeLimit = 25f; // Cronómetro de 25 segundos
    private float timeRemaining;
    public Transform respawnPoint;

    // Referencia a la UI del cronómetro
    public TextMeshProUGUI timerText;

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerText(); // Inicializar el cronómetro en pantalla
    }

    void Update()
    {
        // Actualizar el cronómetro
        timeRemaining -= Time.deltaTime;
        UpdateTimerText();

        // Si el tiempo se agota, reiniciar la escena
        if (timeRemaining <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Reiniciar el nivel si el jugador presiona la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Método para actualizar el cronómetro en la pantalla
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60F);
        int seconds = Mathf.FloorToInt(timeRemaining % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // Detectar si el jugador llega a la plataforma final
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador llegó a la plataforma final, avanzar a la siguiente escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.CompareTag("DeathZone"))
        {
            transform.position = respawnPoint.position;
    }
}    
}   
