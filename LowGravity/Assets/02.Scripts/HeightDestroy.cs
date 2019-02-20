using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y <= -15f)
        {
            Destroy(gameObject);
        }
	}

    public void DestroyFromPlayer()
    {
        Destroy(gameObject);
    }
}
