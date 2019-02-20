using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerScript : MonoBehaviour {

    float m_smooth = 500f;
    private Quaternion m_TargetRotation;

	// Use this for initialization
	void Start () {
        m_TargetRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        //m_TargetRotation *= Quaternion.AngleAxis(60, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, m_TargetRotation, 10 * m_smooth * Time.deltaTime);
        transform.Rotate(Vector3.forward, m_smooth * Time.deltaTime, Space.Self);
    }
}
