using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private float _maxSprintSpeed = 2f;
	[SerializeField]
	private float _speed = 1.5f;
	private float _sprintSpeed = 1f;
	[SerializeField]
	private float _sensitivity = 2f;
	[SerializeField]
	private float _jumpHeight = 2f;
	private Rigidbody _rb;
	void Start () {
		this._rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerInput ();
	}

	void playerInput()
	{
		this.rotateCamera ();
		this.sprinting ();

		float strafing = Input.GetAxis("Vertical") * -this._speed * this._sprintSpeed * Time.deltaTime;
		float horizontal = Input.GetAxis("Horizontal") * this._speed * this._sprintSpeed * Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space) && this.canJump())
		{
			this._rb.AddForce (0, this._jumpHeight * 100, 0);
		}

		this.transform.Translate (new Vector3 (strafing, 0, horizontal));
	}

	void rotateCamera()
	{
		float horizontal = this.transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * this._sensitivity;
		float vertical = Camera.main.transform.localEulerAngles.x + Input.GetAxis ("Mouse Y") * this._sensitivity * -1;

		Debug.Log (vertical);
		this.transform.localEulerAngles = new Vector3 (0, horizontal, 0);
		Camera.main.transform.localEulerAngles = new Vector3 (vertical, -90);

	}

	void sprinting()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			this._sprintSpeed = this._maxSprintSpeed;
		}
		if (!Input.GetKey(KeyCode.LeftShift))
		{
			this._sprintSpeed = 1f;
		}
	}

	bool canJump()
	{
		Ray ray = new Ray(this.transform.position, Vector3.down);
		RaycastHit hit;

		Debug.DrawRay (this.transform.position, Vector3.down);
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.distance < 0.3f)
				return (true);
		}
		return (false);
	}
}
