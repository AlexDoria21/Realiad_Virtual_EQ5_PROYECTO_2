using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   
public class Dinamica4SeguimietoEnemigo : MonoBehaviour
{
    public float timeLimit = 30f; // Cronómetro para la dinámica
    private float tiemporestante;

    public TextMeshProUGUI timerText; // Referencia a la UI del cronómetro
    public TextMeshProUGUI itemsCollectedText; // Referencia a la UI del contador de objetos

    public int totalItems = 10; // Número total de objetos a recolectar
    private int itemsCollected = 0; // Contador de objetos recolectados

    public GameObject enemy; // Enemigo que sigue al jugador
    public Transform player; // Jugador que será seguido por el enemigo

    public float enemySpeed = 3f; // Velocidad del enemigo

    void Start()
    {
        tiemporestante = timeLimit;
        UpdateTimerText();
        UpdateItemsCollectedText();
    }

    void Update()
    {
        // Actualizar el cronómetro
        tiemporestante -= Time.deltaTime;
        UpdateTimerText();

        // Si el tiempo se agota, reiniciar la escena
        if (tiemporestante <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Hacer que el enemigo siga al jugador
        SeguirJugador();

        // Reiniciar el nivel si el jugador presiona la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Método para actualizar el cronómetro en pantalla
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(tiemporestante / 60F);
        int seconds = Mathf.FloorToInt(tiemporestante % 60F);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    // Método para actualizar el contador de objetos en pantalla
    void UpdateItemsCollectedText()
    {
        itemsCollectedText.text = "Items: " + itemsCollected.ToString() + "/" + totalItems.ToString();
    }

    // Hacer que el enemigo siga al jugador
    void SeguirJugador()
    {
        // Mover al enemigo hacia la posición del jugador
        Vector3 direction = player.position - enemy.transform.position;
        enemy.transform.position += direction.normalized * enemySpeed * Time.deltaTime;
    }

    // Detectar si el jugador entra en el trigger de un objeto con la etiqueta "llave"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lave"))
        {
            // Recolectar el objeto
            CollectItem();
            other.gameObject.SetActive(false);
        }

        // Si el enemigo toca al jugador, reiniciar la escena
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (other.CompareTag("Meta"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Método para recolectar un objeto
    void CollectItem()
    {
        itemsCollected++;
        UpdateItemsCollectedText();

        // Si se recolectan todos los objetos, avanzar a la siguiente escena
        if (itemsCollected >= totalItems)
        {
            if (enemy != null)
            {
                enemy.SetActive(false); // Desaparecer al enemigo
        }
    }
}    
}
