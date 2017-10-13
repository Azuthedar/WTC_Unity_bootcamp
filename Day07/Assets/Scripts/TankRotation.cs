using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankRotation : MonoBehaviour {

	// Use this for initialization
	public ParticleSystem	turretMuzzleFlash;
	public ParticleSystem	missileMuzzleFlash;
	public ParticleSystem	missileExplosion;

	public Image 			crosshair;

	public AudioClip 		turretShoot;
	public AudioClip 		missileHit;
	public AudioClip 		missileMiss;

	[SerializeField]
	private float			_rotationSpeed = 2f;
	private bool 			_isShooting = false;
	private bool			_hitEnemy = false;
	private AudioSource		_as;

	void Start ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		this._as = GetComponent<AudioSource> ();
	}

	void Awake()
	{
		ParticleSystem.MainModule turret = this.turretMuzzleFlash.main;
		ParticleSystem.MainModule missile = this.missileMuzzleFlash.main;

		turret.playOnAwake = false;
		missile.playOnAwake = false;
	}

	void Update () {
		this.rotateTank ();
		this.playerInput ();
	}

	void rotateTank()
	{
		float horizontal = Input.GetAxis("Mouse X") * this._rotationSpeed * Time.deltaTime;
		float vertical =  Input.GetAxis ("Mouse Y") * this._rotationSpeed * Time.deltaTime * -1;

		this.transform.Rotate (vertical, 0, horizontal);
	}

	void shootBullet()
	{
		Ray ray = new Ray (this.transform.position, -this.transform.up);
		RaycastHit hit;

		Debug.DrawRay (this.transform.position, -this.transform.up, Color.red);

		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log (hit.collider.tag);
			if (hit.collider.tag == "Enemy")
			{
				this._hitEnemy = true;
				this.crosshair.color = Color.red;
				// TODO: Enemy must take damage
			}
		}
	}

	void playerInput()
	{
		if (this.crosshair.color == Color.red)
		{
			Invoke ("InvokeCrosshairColor", 0.2f);
		}

		if (Input.GetMouseButton(0) && !this._isShooting)
		{
			this._isShooting = true;
			if (!this.turretMuzzleFlash.isPlaying)
				this.turretMuzzleFlash.Play ();
			else
				this.turretMuzzleFlash.Stop ();
			this.shootBullet ();
			this.playSound ("turret");
			Invoke ("InvokeFireRate", 0.2f);
		}
		else if (Input.GetMouseButtonDown(1) && !this._isShooting)
		{
			this._isShooting = true;
			this._hitEnemy = false;

			if (!this.missileMuzzleFlash.isPaused)
				this.missileMuzzleFlash.Play ();
			else
				this.missileMuzzleFlash.Stop ();
			this.shootBullet ();
			this.playSound ("missile");
				

			Invoke ("InvokeFireRate", 1f);
		}
	}

	void playSound(string type)
	{
		if (this._as)
		{
			if (type == "turret")
				this._as.clip = turretShoot;
			else
			if (type == "missile")
			{
				if (this._hitEnemy)
					this._as.clip = missileHit;
				else
					this._as.clip = missileMiss;
			}
				this._as.Play ();
		}
	}

#region Invokes / Coroutines
	void InvokeCrosshairColor()
	{
		this.crosshair.color = Color.white;
	}

	void InvokeFireRate()
	{
		this._isShooting = false;
	}
#endregion
}
