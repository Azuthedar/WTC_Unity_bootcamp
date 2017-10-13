using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

#region Player Stats
	private int		_health;
	private int		_maxHealth = 300;
	private int		_armor;
	private float	_fireRate = 0.2f;
#endregion

#region Level handlers
	private int		_level = 1;
	private long 	_experience;
	private long 	_maxExperience = 100;
#endregion
	[SerializeField]
	private float	_speed = 2f;
	[SerializeField]
	private float	_rotateSpeed = 2f;

	private float		_sprintSpeed = 1f;
	private ParticleSystem[] _particleSystems;
	private ParticleSystem	_particleLevelUp;
	private bool		_PSShouldLoop = false;



	// Use this for initialization
	void Start () {
		this._particleSystems = GetComponentsInChildren<ParticleSystem> ();
		this._health = this._maxHealth;
		this._particleLevelUp = GameObject.FindGameObjectWithTag ("levelUp").GetComponent<ParticleSystem>();
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

	public void takeDamage(int damage)
	{
		this._health -= damage - this._armor;
		if (this._health <= 0)
		{
			destroy ();
		}
	}

	void destroy()
	{
		Destroy (this.gameObject);
	}

#region Level handling

	public void gainExp(int experience)
	{
		this._experience += experience;

		if (this._experience >= _maxExperience)
			this.levelUp ();
	}

	void levelUp()
	{
		this._experience -= this._maxExperience;
		while (this._experience >= _maxExperience)
		{
			this._experience -= this._maxExperience;
			this._level++;

			this._maxExperience = (int)Mathf.Pow (this._level + (this._maxExperience / 600), 2);

			if (!this._particleLevelUp.isPlaying)
				this._particleLevelUp.Play ();

			this.transform.localScale += new Vector3 (0.001f, 0.001f, 0.001f);

			if (this._level % 5 == 0)
			{
				this._armor += 2;
			}
			if (this._level % 10 == 0)
				this._fireRate -= 0.005f;
			GetComponentInChildren<TankRotation> ().setFireRate (this._fireRate);
			this._maxHealth += 50 * (this._level / 10);
			this._health = this._maxHealth;
		}
	}

#endregion

#region Getters / Setters

	public float	getFireRate()	{return (this._fireRate);}
	public int		getLevel()		{return (this._level);}
	public int		getHealth()		{return (this._health);}
	public int		getMaxHealth()	{return (this._maxHealth);}
	public long		getExp()		{return (this._experience);}
	public long		getMaxExp()		{return (this._maxExperience);}

#endregion
}
