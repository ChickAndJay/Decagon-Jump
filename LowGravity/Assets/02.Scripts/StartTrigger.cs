using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrigger : MonoBehaviour {
    GameObject m_GameManager;
    StageScript m_StageScript;

    bool m_ShownBefore;

	// Use this for initialization
	void Start () {
        m_StageScript = GetComponentInParent<StageScript>();
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager");

        m_ShownBefore = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !m_ShownBefore)
        {
            m_GameManager.GetComponent<GameManagerScript>().ShowStageText();
            m_ShownBefore = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Stay");
    }
}
