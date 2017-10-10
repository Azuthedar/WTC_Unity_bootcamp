using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float maxSprintSpeed = 2f;
	public GameObject ball;
	public GameObject arrow;

	[SerializeField]
	private float	_sensitivity = 2f;

	private Vector3 _ballPos;
	private float 	_speed = 2f;
	private float 	_sprintSpeed = 1f;
	private Vector3 _offset;
	private bool	_isFocus = false;
	private bool	_posSet = false;
	private Vector3 _arrowOffset;
	private Renderer[] _arrowMeshes;

	// Use this for initialization
	void Start () {
		this._ballPos = ball.GetComponent<Transform> ().position;
		this._offset = this.transform.position - this._ballPos;
		this._arrowOffset = this.ball.transform.position - this.arrow.transform.position;
		this._arrowOffset.y = 0;
		this._arrowMeshes = arrow.GetComponentsInChildren<MeshRenderer> ();
		this.modifyMeshArrow (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this._ballPos = ball.GetComponent<Transform> ().position;
		this.transformPosition ();
	}


	void cameraRotation()
	{
		float horizontal = this.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * this._sensitivity;
		float vertical = this.transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * this._sensitivity * -1;

		transform.localEulerAngles = new Vector3 (vertical, horizontal, 0);
	}

	void transformPosition()
	{
		this.cameraRotation ();
		this.sprint ();

		if (Input.GetKey (KeyCode.W) && !this.ball.GetComponent<BallController>().getIsCharging())
		{
			transform.Translate (Vector3.forward * this._speed * this._sprintSpeed);
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate (Vector3.left * this._speed * this._sprintSpeed);
		}
		if (Input.GetKey (KeyCode.D))
		{
			transform.Translate (Vector3.right * this._speed * this._sprintSpeed);
		}
		if (Input.GetKey (KeyCode.S) && !this.ball.GetComponent<BallController>().getIsCharging())
		{
			transform.Translate (Vector3.back * this._speed * this._sprintSpeed);
		}
		if (Input.GetKey (KeyCode.E) && !this._isFocus)
		{
			transform.Translate (Vector3.up * this._speed * this._sprintSpeed);
		}
		if (Input.GetKey (KeyCode.Q) && !this._isFocus)
		{
			transform.Translate (Vector3.down * this._speed * this._sprintSpeed);
		}
		this.focusObject ();
		this.limitCamera ();
	}

	void sprint()
	{
		if (Input.GetKey (KeyCode.LeftShift))
			this._sprintSpeed = this.maxSprintSpeed;
		else if (!Input.GetKey (KeyCode.LeftShift) && this._sprintSpeed > 1f)
			this._sprintSpeed = 1f;
	}

	void focusObject()
	{
		// Check focus and if focus do some focus logic
		if (Input.GetKeyDown (KeyCode.F))
		{
			if (!this._isFocus && this.ball.GetComponent<Rigidbody> ().velocity == Vector3.zero)
			{
				this.modifyMeshArrow (true);
				this._isFocus = true;
			}
			else if (this._isFocus)
			{
				this.modifyMeshArrow (false);

				this._posSet = false;
				this._isFocus = false;
			}
		}

		if (this._isFocus)
		{
			// If the position has not been set yet set it
			if (!this._posSet)
			{
				this._posSet = true;
				this.transform.position = this._ballPos + this._offset;
				Debug.Log (this._offset);
			}
			this.transform.LookAt (this._ballPos);
			this.placeArrow ();
		}
	}

	void limitCamera()
	{
		//Used to clamp the player inside a specific area in the gameplay map.
		this.transform.position = new Vector3(Mathf.Clamp (this.transform.position.x, 50.0f, 450.0f),
			Mathf.Clamp (this.transform.position.y, 110.0f, 200.0f),
			Mathf.Clamp (this.transform.position.z, 50.0f, 450.0f));
	}

	void placeArrow()
	{
		float precision = 1f;
		if (Input.GetKey (KeyCode.LeftShift))
			precision = 0.2f;
		if (!Input.GetKey (KeyCode.LeftShift))
			precision = 1f;

		if (Input.GetKey(KeyCode.W) && this.ball.GetComponent<BallController>().getIsCharging())
		{
			this.arrow.transform.Rotate (-this.transform.rotation.x * this._speed * 5 * precision, 0, 0);
		}
		if (Input.GetKey(KeyCode.S) && this.ball.GetComponent<BallController>().getIsCharging())
		{
			this.arrow.transform.Rotate (this.transform.rotation.x * this._speed * 5 * precision, 0, 0);
		}
	}

	public void modifyMeshArrow(bool isEnabled)
	{
		if (isEnabled)
			for (int i = 0; i < this._arrowMeshes.Length; i++)
				this._arrowMeshes [i].enabled = true;
		else
			for (int i = 0; i < this._arrowMeshes.Length; i++)
				this._arrowMeshes [i].enabled = false;
	}

	#region Getters & Setters

	public void setIsFocus(bool isFocus)
	{
		this._isFocus = isFocus;
	}

	public bool getIsFocus()
	{
		return (this._isFocus);
	}
	#endregion
}
