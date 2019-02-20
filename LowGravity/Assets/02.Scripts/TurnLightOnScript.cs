using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLightOnScript : MonoBehaviour {
    public Material m_TurnOnMaterial;
    Material m_OriginalMaterial;
    Color m_OriginalColor;

	// Use this for initialization
	void Start () {


    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // other.GetComponentInParent<PlayerScript>().ChangeToWhite();             
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // other.GetComponentInParent<PlayerScript>().ChangeToBlue();
        }
    }
}
