using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	// Use this for initialization
	public GameObject player;

	[SerializeField]
	private float _maxChargePower = 50f;

	private float	_chargePower = 0.0f;
	private bool	_isCharging = false;
	private bool 	_shouldInvert = false;
	private Rigidbody _rb;
	private GameObject _arrow;

	void Start () {
		this._arrow = player.GetComponent<PlayerController> ().arrow;
		this._rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		this.charging ();
	}

	void charging()
	{
		if (Input.GetKey(KeyCode.Space) && player.GetComponent<PlayerController>().getIsFocus() && !this._isCharging && this._rb.velocity == Vector3.zero)
		{
			this._isCharging = true;
		}
		else if (!Input.GetKey(KeyCode.Space) && this._isCharging)
		{
			this.player.GetComponent<PlayerController> ().setIsFocus (false);
			this.player.GetComponent<PlayerController> ().modifyMeshArrow (false);
			this._isCharging = false;
			this._rb.velocity = -this._arrow.transform.forward * this._chargePower;
			this._chargePower = 0f;
		}

		if (this._isCharging)
		{
			// Check if the charge power should be inverted
			if (this._chargePower >= this._maxChargePower && this._shouldInvert == false)
			{
				this._shouldInvert = true;
			}
			else if (this._chargePower <= 0 && this._shouldInvert)
			{
				this._shouldInvert = false;
			}

			// Inversion happens here
			if (this._shouldInvert)
			{
				this._chargePower -= this._maxChargePower / 1.5f * Time.deltaTime;
			}
			else
				this._chargePower += this._maxChargePower / 1.5f * Time.deltaTime;
		}
	}

	#region Getters, Setters and some utils
	public bool getIsCharging()
	{
		return (this._isCharging);
	}

	public float percentageCharge()
	{
		return (this._chargePower / this._maxChargePower);
	}
	#endregion
}
