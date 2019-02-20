using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInSecScript : MonoBehaviour {
    float m_timer = 0;
	


	// Update is called once per frame
	void Update () {
        m_timer += Time.deltaTime;
        if (m_timer > 3f)
            Destroy(gameObject);
   
	}
}
