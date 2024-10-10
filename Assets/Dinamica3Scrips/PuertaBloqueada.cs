using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PuertaBloqueada : MonoBehaviour
{
    public float timeLimit = 20f; // Cronómetro de 20 segundos
    private float timeRemaining;

    public TextMeshProUGUI timerText; // Referencia a la UI del cronómetro
    public TextMeshProUGUI itemsCollectedText; // Referencia a la UI del contador de objetos

    public int totalItems = 10; // Número total de objetos a recolectar
    private int itemsCollected = 0; // Contador de objetos recolectados

    public GameObject door; // Puerta que se desbloqueará

    void Start()
    {
        timeRemaining = timeLimit;
        UpdateTimerText();
        UpdateItemsCollectedText();
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

    // Método para recolectar un objeto
    public void CollectItem()
    {
        itemsCollected++;
        UpdateItemsCollectedText();

        // Si recolecta todos los objetos, desbloquear la puerta
        if (itemsCollected >= totalItems)
        {
            UnlockDoor();
        }
    }

    // Método para actualizar el cronómetro en pantalla
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60F);
        int seconds = Mathf.FloorToInt(timeRemaining % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // Método para actualizar el contador de objetos en pantalla
    void UpdateItemsCollectedText()
    {
        itemsCollectedText.text = "Items: " + itemsCollected.ToString() + "/" + totalItems.ToString();
    }

    // Método para desbloquear la puerta
    void UnlockDoor()
    {
        door.SetActive(false); // Se podría hacer más avanzado con animaciones
    }

    // Detectar si el jugador entra en el trigger de un objeto con la etiqueta "llave"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lave"))
        {
            // Llamar al método CollectItem() cuando se recoja una llave
            CollectItem();

            // Desactivar el objeto recolectado (puedes usar Destroy si prefieres)
            other.gameObject.SetActive(false);
            // o Destroy(other.gameObject); si prefieres eliminarlo completamente
        }
          // Detectar si el jugador pisa la plataforma para pasar a la siguiente dinámica
        if (other.CompareTag("Meta"))
        {
            // El jugador pisa la plataforma final, cambiar a la siguiente dinámica
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (other.CompareTag("Trampa"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}