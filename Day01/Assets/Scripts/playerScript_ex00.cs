using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex00 : MonoBehaviour {

	[SerializeField]
	private float 		_speed;
	[SerializeField]
	private float 		_jumpHeight;
	private Rigidbody2D _rbody;
	private string		_tagName;
	// Use this for initialization
	void Start () {
		this._rbody = GetComponent<Rigidbody2D> ();
		this._tagName = "Thomas";
	}
	
	// Update is called once per frame
	void Update () {
		playerInput ();
		changeCharacter (this._tagName);
	}


	void changeCharacter(string tagName)
	{
		if (CompareTag(tagName))
		{
			if (Input.GetKey(KeyCode.RightArrow))
			{
				this._rbody.AddForce (new Vector2(this._speed, 0));
			}
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				this._rbody.AddForce (new Vector2(-this._speed, 0));
			}
			if (Input.GetKey(KeyCode.Space))
			{
				this._rbody.velocity = new Vector2(0, this._jumpHeight);
			}
		}
			
	}

	void playerInput()
	{
		if (Input.GetKeyDown (KeyCode.Alpha1))
			this._tagName = "Thomas";
		if (Input.GetKeyDown (KeyCode.Alpha2))
			this._tagName = "John";
		if (Input.GetKeyDown (KeyCode.Alpha3))
			this._tagName = "Claire";
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public string getTagName()
	{
		return (this._tagName);
	}
}
