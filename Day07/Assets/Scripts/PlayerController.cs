using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	[SerializeField]
	private float	_speed = 2f;
	[SerializeField]
	private float	_rotateSpeed = 2f;

	private float		_sprintSpeed = 1f;
	private ParticleSystem[] _particleSystems;
	private bool		_PSShouldLoop = false;



	// Use this for initialization
	void Start () {
		this._particleSystems = GetComponentsInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.playerInput ();
		this.playParticle ();

	}

	void playerInput()
	{
		if (Input.GetKey (KeyCode.LeftShift))
		{
			this._sprintSpeed = 2f;	
		}
		else
			this._sprintSpeed = 1f;

		if (Input.GetKey(KeyCode.W))
		{
			this.transform.Translate (Vector3.forward * this._speed * this._sprintSpeed * Time.deltaTime);
			this._PSShouldLoop = true;
		}
		if (Input.GetKey(KeyCode.S))
		{
			this.transform.Translate (Vector3.back * this._speed * this._sprintSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A))
		{
			this.transform.Rotate (Vector3.down * this._speed * this._rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			this.transform.Rotate (Vector3.up * this._speed * this._rotateSpeed * Time.deltaTime);
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
		}

		if (onHead ())
			this.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	bool onHead()
	{
		if (Vector3.Dot (transform.up, Vector3.down) >= 0.7f)
		{
			return (true);
		}
		return (false);
	}

	void playParticle()
	{
		if (!Input.GetKey (KeyCode.W))
			this._PSShouldLoop = false;
		if (this._PSShouldLoop)
		{
			for (int i = 0; i < this._particleSystems.Length; i++)
			{
				if (!this._particleSystems [i].isPlaying && this._particleSystems[i].tag == "LowerTank")
				{
					ParticleSystem.MainModule main = this._particleSystems [i].main;
					main.loop = true;
					this._particleSystems [i].Play ();
				}
			}
		}
		else
		{
			for (int i = 0; i < this._particleSystems.Length; i++)
			{
				if (this._particleSystems [i].isPlaying && this._particleSystems[i].tag == "LowerTank")
				{
					ParticleSystem.MainModule main = this._particleSystems [i].main;
					main.loop = false;
					this._particleSystems [i].Stop ();
				}
			}
		}
		
	}
}
