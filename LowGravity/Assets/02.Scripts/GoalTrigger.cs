using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {
    StageScript m_StageScript;

	// Use this for initialization
	void Start () {
        m_StageScript = GetComponentInParent<StageScript>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_StageScript.ReachPlayerGoal();
           // other.GetComponentInParent<PlayerScript>().GoalMade(true);
        }
    }
}
