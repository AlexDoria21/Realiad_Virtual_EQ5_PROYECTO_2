using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                if (Input.GetKeyDown(KeyCode.B))
        {
            // Cambia a la escena 1 (por nombre o por índice)
            SceneManager.LoadScene("Instrucciones");
    }
    }           
}
