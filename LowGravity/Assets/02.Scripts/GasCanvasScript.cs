﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasCanvasScript : MonoBehaviour {
    public GameObject m_Camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(m_Camera.GetComponent<Transform>());
	}
}