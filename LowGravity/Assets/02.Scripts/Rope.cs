using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {
    Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
        this.gameObject.AddComponent<Rigidbody>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.isKinematic = true;

        int childCount = this.transform.childCount;

        for(int i=0; i<childCount; i++)
        {
            Transform t = this.transform.GetChild(i);
            if (t.CompareTag("BridgePart"))
            {
                t.gameObject.AddComponent<Rigidbody>();

                t.gameObject.AddComponent<HingeJoint>();

                HingeJoint hinge = t.gameObject.GetComponent<HingeJoint>();

                hinge.connectedBody = i == 0 ? m_rigidbody :
                    transform.GetChild(i - 1).GetComponent<Rigidbody>();

                if (i == childCount - 1)
                {
                    t.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                }
                else
                    hinge.useSpring = true;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
