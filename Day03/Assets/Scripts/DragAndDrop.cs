using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour {

	public GameObject Gattling;
	public GameObject Rocket;
	public GameObject Turret;

	public GameObject UI_Gattling;
	public GameObject UI_Rocket;
	public GameObject UI_Turret;
	// Use this for initialization
	private string _tagName;
	private bool _isDragging = false;
	private Vector2 _initialPos_Gattling;
	private Vector2 _initialPos_Rocket;
	private Vector2 _initalPos_Turret;

	void Start () {
		this._initialPos_Gattling = UI_Gattling.transform.position;
		this._initialPos_Rocket = UI_Rocket.transform.position;
		this._initalPos_Turret = UI_Turret.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		doActions ();
	}

	void doActions()
	{
		if (Input.GetMouseButton (0))
		{
			Debug.Log (Camera.main.ScreenToWorldPoint (Input.mousePosition));
			doRayCast ();
		}
		if (this._isDragging)
		{
			doDrag (this._tagName);
		}
		if (!Input.GetMouseButton(0) && this._isDragging)
		{
			Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			if (this._tagName == "Gattling")
			{
				this.UI_Gattling.transform.position = this._initialPos_Gattling;
				Gattling.transform.position = mousePos;
				GameObject.Instantiate (Gattling);
			}
			else if (this._tagName == "Rocket")
			{
				this.UI_Rocket.transform.position = this._initialPos_Rocket;
				Rocket.transform.position = mousePos;
				GameObject.Instantiate (Rocket);
			}
			else if (this._tagName == "Turret")
			{
				this.UI_Turret.transform.position = this._initalPos_Turret;
				Turret.transform.position = mousePos;
				GameObject.Instantiate (Turret);
			}
			this._tagName = null;
			this._isDragging = false;
		}

	}


	void doRayCast()
	{
		Vector2 mousePos = Input.mousePosition;
		RaycastHit2D hit = Physics2D.Raycast (mousePos, Vector2.zero);

		if (hit.collider != null)
		{
			if (hit.collider.tag == "Gattling")
			{
				this._isDragging = true;
				this._tagName = "Gattling";
			}
			else if (hit.collider.tag == "Rocket")
			{
				Debug.Log ("Found Rocket");
				this._isDragging = true;
				this._tagName = "Rocket";
			}
			else if (hit.collider.tag == "Turret")
			{
				Debug.Log ("Fouund Turret");
				this._tagName = "Turret";
				this._isDragging = true;
			}
		}
	}

	void doDrag(string tagName)
	{
		Vector2 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		if (tagName == "Gattling")
		{
			UI_Gattling.transform.position = mousePos;
		}
		else if (tagName == "Rocket")
		{
			UI_Rocket.transform.position = mousePos;
		}
		else if (tagName == "Turret")
		{
			UI_Turret.transform.position = mousePos;
		}
	}
}
