using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtnScript : MonoBehaviour {
    Text text;
    // Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();

        GetComponent<Button>().onClick.AddListener(delegate {
            gameObject.GetComponentInParent<JumpAndMenuScript>().LevelSelected(text);});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
