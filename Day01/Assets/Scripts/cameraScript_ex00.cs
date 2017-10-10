using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript_ex00 : MonoBehaviour {

	public GameObject Thomas;
	public GameObject John;
	public GameObject Claire;
	// Use this for initialization
	private string		_tagName;
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
		this._tagName = Thomas.GetComponent<playerScript_ex00> ().getTagName();
		if (this._tagName == "Thomas")
		{
			this.transform.position = new Vector3 (Thomas.transform.position.x, Thomas.transform.position.y, this.transform.position.z);
		}
		else if (this._tagName == "John")
		{
			this.transform.position = new Vector3 (John.transform.position.x, John.transform.position.y, this.transform.position.z);
		}
		else if (this._tagName == "Claire")
		{
			this.transform.position = new Vector3 (Claire.transform.position.x, Claire.transform.position.y, this.transform.position.z);
		}
	}
}
