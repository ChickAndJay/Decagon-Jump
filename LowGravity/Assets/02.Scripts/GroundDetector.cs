using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {
    PlayerScript m_PlayerScript;

	// Use this for initialization
	void Start () {
        m_PlayerScript = GetComponentInParent<PlayerScript>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            m_PlayerScript.SetGround(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
       
            m_PlayerScript.SetGround(false);
        
    }

}
