using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {


	[Header("Aesthetics")]
	public AudioClip		turretSound;
	public AudioClip		missileSound;
	public ParticleSystem	particleTurret;
	public ParticleSystem	particleMissile;
	[Space(20)]
	private int 		_health = 300;
	private Vector3		_targetPoint;
	[Header("General")]
	[SerializeField]
	private float		_aggroDistance = 5f;
	[SerializeField]
	private float 		_walkRadius = 4f;
	private float		_maxRange = 500f;
	private Vector3		_randomPos;
	private bool		_followPlayer = false;
	private NavMeshAgent _agent;

	private GameObject	_player;
	private AudioSource _as;

	private float		_time = 0f;
	private bool		_canShoot = true;

	void Start ()
	{
		this._player = GameObject.FindGameObjectWithTag ("Player");
		this._targetPoint = this.transform.position;
		this._agent = GetComponent<NavMeshAgent> ();
		this._as = GetComponent<AudioSource> ();
	}


	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere (this.transform.position, this._aggroDistance);
		Gizmos.DrawWireSphere (this.transform.position, this._walkRadius);
	}

	void Update ()
	{
		this._time -= Time.deltaTime;

		this.followPlayer ();
		this.shootPlayer ();
		if (!this._followPlayer && this._time <= 0)
		{
			this._time = 2f;
			Invoke("InvokeGetRandomPointOnMap", 0.2f);
		}

		if (Vector3.Distance (this.transform.position, this._targetPoint) > 1f)
		{
			//this.transform.position = Vector3.MoveTowards (this.transform.position, this._targetPoint, this._speed * Time.deltaTime);
			this.transform.LookAt (this._targetPoint);
			this._agent.SetDestination (this._targetPoint);
		}
	}


	void followPlayer()
	{
		if (Vector3.Distance (this._player.transform.position, this.transform.position) < this._aggroDistance)
			this._followPlayer = true;
		else
			this._followPlayer = false;

		if (this._followPlayer)
		{
			this._targetPoint = this._player.transform.position;
		}
	}

	void shootPlayer()
	{
		if (Vector3.Distance(this.transform.position, this._player.transform.position) <= this._aggroDistance && this._canShoot)
		{
			float i = Random.Range (1.0f, this._maxRange);

			if (i > (this._maxRange * (7 / 8)))
			{
//				Debug.Log ("shooting missile");
				this._player.GetComponent<PlayerController> ().takeDamage (75 + this._player.GetComponent<PlayerController>().getLevel());
				this.playSndParticles ("missile");
			}
			else if (i > this._maxRange / 2)
			{
//				Debug.Log ("shooting turret");
				this._player.GetComponent<PlayerController> ().takeDamage (15 + this._player.GetComponent<PlayerController>().getLevel());
				this.playSndParticles ("turret");
			}
			else
			{
//				Debug.Log ("shooting turret");
				this.playSndParticles ("turret");
			}
			this._canShoot = false;
			Invoke ("InvokeFireRate", 3f);
		}	
	}

	void playSndParticles(string type)
	{
		if (type == "turret")
		{
			if (!this.particleTurret.isPlaying)
				this.particleTurret.Play ();
			this._as.clip = this.turretSound;
		}
		if (type == "missile")
		{
			if (!this.particleMissile.isPlaying)
				this.particleMissile.Play ();
			this._as.clip = this.missileSound;
		}
		this._as.Play ();
	}

	public void takeDamage (int damage)
	{
		this._health -= damage;

		if (this._health <= 0)
			this.destroy ();
	}

	void destroy()
	{
		this._player.GetComponent<PlayerController> ().gainExp (200);
		Destroy (this.gameObject);
	}

#region Coroutines / Invokes

	void InvokeGetRandomPointOnMap()
	{
		if (this._followPlayer)
		{
			CancelInvoke ();
		}
		else
		{
			//Random.InitState(System.Environment.TickCount);
			this._randomPos += Random.insideUnitSphere * 2 *  this._walkRadius;
			if (this._randomPos.y < 0)
				this._randomPos.y = 0f;
			NavMeshHit hit;

			if (NavMesh.SamplePosition (this._randomPos, out hit, this._walkRadius, -1))
			{
				this._targetPoint = hit.position;
			}
		}
	}

	void InvokeFireRate()
	{
		this._canShoot = true;
	}

#endregion
}
