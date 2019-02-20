using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeepScript : MonoBehaviour {
    AudioSource m_BeepAudio;
    PlayerScript m_PlayerScript;

    bool m_GoalMade;

    float m_Timer;
	// Use this for initialization
	void Start () {
        m_PlayerScript = gameObject.GetComponentInParent<PlayerScript>();
        m_BeepAudio = GetComponentInParent<AudioSource>();
        m_Timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        m_GoalMade = m_PlayerScript.GetGoalMade();
        m_Timer += Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && !m_GoalMade && m_Timer >= 0.5f)
        {
            m_BeepAudio.Play();
            m_Timer = 0f;
        }
         
    }
}
