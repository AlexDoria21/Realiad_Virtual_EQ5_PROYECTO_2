using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinamica2 : MonoBehaviour
{
    public Transform respawnPoint;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador cae, se reinicia en el punto de respawn
        if (other.CompareTag("DeathZone"))
        {
            transform.position = respawnPoint.position;
        }
    }
}
