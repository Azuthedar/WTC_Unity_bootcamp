using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex01 : MonoBehaviour {

	public LayerMask	_groundMask;
	[SerializeField]
	private float 		_speed;
	[SerializeField]
	private float 		_jumpHeight;
	private Rigidbody2D _rbody;
	private string		_tagName;
	private bool		_inPos;
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


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Blue_Exit") && this.CompareTag("Claire"))
			this._inPos = true;
		else if (col.CompareTag ("Red_Exit") && this.CompareTag("Thomas"))
			this._inPos = true;
		else if (col.CompareTag ("Yellow_Exit") && this.CompareTag("John"))
			this._inPos = true;
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
			if (Input.GetKeyDown(KeyCode.Space) && this.checkJump())
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

	bool checkJump()
	{
		float distance = 0.5f;
		RaycastHit2D hit;

		if (this._tagName == "Thomas")
		{
			distance = 0.15f;
		}
		else if (this._tagName == "John")
		{
			distance = 0.3f;
		}
		else if (this._tagName == "Claire")
		{
			distance = 0.3f;
		}
		hit = Physics2D.Raycast (this.transform.position, Vector2.down, distance, this._groundMask);

		if (hit.collider != null)
			return true;
		else
			return false;
	}

	public string	getTagName()	{return (this._tagName);}
	public bool		getInPos()		{return (this._inPos);}
}
