using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cs : MonoBehaviour
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
            // Cambia a la primer dincamica (Se cambia por el nombre de la escena)
            SceneManager.LoadScene("Dinmica2");
    }
    }           
}


