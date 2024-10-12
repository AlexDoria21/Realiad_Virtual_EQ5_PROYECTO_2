using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lluviaobjetos : MonoBehaviour
{
    public GameObject[] fallingObjects; // Array de objetos que caerán
    public Transform[] spawnPoints; // Puntos de spawn desde donde caen los objetos
    public float fallSpeed = 5f; // Velocidad de caída
    public float respawnTime = 2f; // Tiempo de espera antes de hacer respawn

    public int totalCollectibles = 3; // Cantidad de objetos a recolectar
    private int collectedItems = 0; // Contador de objetos recolectados
    public TextMeshProUGUI collectedItemsText; // Texto para mostrar los objetos recolectados

    public Transform player; // Referencia al jugador

    void Start()
    {
        UpdateCollectedItemsText();
        StartFallingObjects();
    }

    // Actualizar el contador de objetos en pantalla
    void UpdateCollectedItemsText()
    {
        collectedItemsText.text = "Items: " + collectedItems.ToString() + "/" + totalCollectibles.ToString();
    }

    // Iniciar la caída de objetos
    void StartFallingObjects()
    {
        foreach (GameObject fallingObject in fallingObjects)
        {
            RespawnObject(fallingObject);
        }
    }

    // Hacer respawn de los objetos que caen
    void RespawnObject(GameObject obj)
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        obj.transform.position = spawnPoints[randomIndex].position;
        obj.SetActive(true);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.down * fallSpeed; // Hacer que caigan
        }
    }

    void Update()
    {
// Revisar si algún objeto cayó al suelo
        foreach (GameObject fallingObject in fallingObjects)
        {
            if (fallingObject.transform.position.y < 1f) // Asumimos que el suelo está a y = -5
            {
                fallingObject.SetActive(false); // Desactivar el objeto
                StartCoroutine(RespawnAfterDelay(fallingObject)); // Usar coroutine para hacer respawn
            }
        }

        // Reiniciar el nivel si el jugador presiona la tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    

    // Coroutine para hacer respawn de un objeto después de un retraso
    private IEnumerator RespawnAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(respawnTime); // Esperar el tiempo de respawn
        RespawnObject(obj); // Volver a hacer el respawn del objeto
    }

    // Detectar si el jugador recolecta un objeto o es golpeado por un objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Proyectil"))
        {
            // Si el jugador es golpeado por un objeto que cae, reiniciar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (other.CompareTag("Lave"))
        {
            // Si el jugador recolecta un objeto
            collectedItems++;
            other.gameObject.SetActive(false);
            UpdateCollectedItemsText();

            // Si recolecta todos los objetos, avanzar a la siguiente escena
            if (collectedItems >= totalCollectibles)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}

