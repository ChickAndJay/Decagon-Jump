using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    float timeLeft = 3;
    Color targetColor;
    Renderer renderer;
    private void Start()
    {
        renderer = GetComponent<Renderer>();
        targetColor = Color.red;

    }

    void Update()
    {

    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("sefsfe");
    }

}
