using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAnimScript : MonoBehaviour {
    float m_smooth = 500f;
    float m_Moving_Speed = 10f;
    public GameObject m_Wings;
    public GameObject m_Explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_Wings.GetComponent<Transform>().Rotate(Vector3.right, m_smooth * Time.deltaTime, Space.Self);
        //transform.rotation(z)
        transform.Translate(0, 0, -m_Moving_Speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("FrontDetector"))
        {
            //m_Wings.gameObject.transform.SetParent(null);
            m_Explosion.transform.SetParent(null);
            m_Explosion.GetComponent<ParticleSystem>().Play();

            Renderer[] renderers = GetComponentsInChildren<Renderer>();

            foreach (Renderer rd in renderers)
            {
                rd.enabled = false;
            }

            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerScript>().Fail();
            }

            Destroy(gameObject);
        }

    }

}
