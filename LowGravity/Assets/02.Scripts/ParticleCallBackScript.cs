using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCallBackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnParticleSystemStopped()
    {
        Debug.Log("OnParticleSystemStopped");
    }
}
