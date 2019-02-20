using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreateScript : MonoBehaviour {
    public GameObject m_Obstacle_Prefab;
    GameObject m_Obstacle;
	// Use this for initialization
	void Start () {
        m_Obstacle = GameObject.Instantiate(m_Obstacle_Prefab, transform.position, transform.rotation);
        m_Obstacle.GetComponent<Transform>().SetParent(gameObject.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reactive()
    {
        if (m_Obstacle.CompareTag("FracturingObj")) {
            m_Obstacle.SetActive(true);
            m_Obstacle.GetComponent<FracturingScript>().SetTimerZero();
        }
    }
}
