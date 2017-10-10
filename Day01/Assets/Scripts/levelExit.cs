using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelExit : MonoBehaviour {

	private bool inPos;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerExit(Collider col)
	{
		inPos = false;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
