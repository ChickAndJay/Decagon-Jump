using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturingScript : MonoBehaviour {
    float m_timer;
    bool m_start;
    bool m_explode;

    public GameObject m_ReplacementObj;
    public GameObject m_TorchFire;

    public float m_Limit_Time = 1.2f;
    bool m_torchOnOff;

	// Use this for initialization
	void Start () {
        m_timer = m_Limit_Time;
        m_start = false;
        m_explode = false;
        m_torchOnOff = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_start)
        {
            if (!m_torchOnOff)
            {
                m_torchOnOff = true;
                m_TorchFire.GetComponent<ParticleSystem>().Play();
            }

            m_timer -= Time.deltaTime;
            m_TorchFire.GetComponent<ParticleSystem>().startLifetime = m_timer;
            if (m_timer <= 0)
            {
                m_TorchFire.GetComponent<ParticleSystem>().Stop();
                GameObject.Instantiate(m_ReplacementObj, transform.position, transform.rotation);
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
        else
        {
            if (m_timer <= m_Limit_Time)
            {
                m_timer += Time.deltaTime;
            }else if(m_timer > m_Limit_Time)
            {
                m_timer = m_Limit_Time;
            }
            //m_TorchFire.GetComponent<ParticleSystem>().startLifetime = m_timer;
        }
    }

    public void SetTimerZero()
    {
        m_start = false;
        m_timer = m_Limit_Time;
        m_TorchFire.GetComponent<ParticleSystem>().startLifetime = m_timer;
        m_torchOnOff = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_start = true;
            //if (m_explode)
            //{
            //    GameObject.Instantiate(m_ReplacementObj, transform.position, transform.rotation);
            //    Destroy(gameObject);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_start = false;
        }
    }

}
