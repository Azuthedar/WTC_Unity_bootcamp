using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {
	
	private Vector3		_targetPoint;
	[SerializeField]
	private float		_aggroDistance = 5f;
	[SerializeField]
	private float 		_speed = 2f;
	[SerializeField]
	private float 		_walkRadius = 4f;
	private Vector3		_randomPos;
	private bool		_followPlayer = false;

	private float		_time = 2f;

	void Start ()
	{
		this._targetPoint = this.transform.position;
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
		if (!this._followPlayer && this._time <= 0f)
		{
			Debug.Log ("You fucker");
			this._time = 2f;
			Invoke("InvokeGetRandomPointOnMap", 0.2f);
		}

		Debug.Log (this._targetPoint);
		this.transform.LookAt (this._targetPoint);
		if (Vector3.Distance(this.transform.position, this._targetPoint) > 1f)
			this.transform.position = Vector3.MoveTowards (this.transform.position, this._targetPoint, this._speed * Time.deltaTime);
	}


	void followPlayer()
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (Vector3.Distance (player.transform.position, this.transform.position) < this._aggroDistance)
			this._followPlayer = true;
		else
			this._followPlayer = false;

		if (this._followPlayer)
		{
			this._targetPoint = player.transform.position;
		}
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
			Random.InitState(System.Environment.TickCount);
			this._randomPos = new Vector3(2, 0 , 4) + Random.insideUnitSphere * this._walkRadius;
			if (this._randomPos.y < 0)
				this._randomPos.y = 0f;
			Debug.Log ("RandPos: " + this._randomPos);
			NavMeshHit hit;

			if (NavMesh.SamplePosition (this._randomPos, out hit, this._walkRadius, 1))
			{
				Debug.Log ("Hit pos is: " + hit.position);
				this._targetPoint = hit.position;
			}
		}
	}

#endregion
}
