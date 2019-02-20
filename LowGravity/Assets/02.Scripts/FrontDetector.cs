using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDetector : MonoBehaviour {
    PlayerScript m_PlayerScript;

	// Use this for initialization
	void Start () {
        m_PlayerScript = GetComponentInParent<PlayerScript>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Obstacle"))
    //    {
    //        if (gameObject.CompareTag("FrontDetector"))
    //        {
    //            m_PlayerScript.SetMoveUpward(true);
    //        }else if (gameObject.CompareTag("StairDetector"))
    //        {
    //            m_PlayerScript.SetStairUpward(true);
    //        }
    //    }


    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (gameObject.CompareTag("FrontDetector"))
    //    {
    //        m_PlayerScript.SetMoveUpward(false);
    //    }else if (gameObject.CompareTag("StairDetector"))
    //    {
    //        m_PlayerScript.SetStairUpward(false);
    //    }
    //}
}
