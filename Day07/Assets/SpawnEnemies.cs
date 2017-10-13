using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemies : MonoBehaviour {


	public GameObject enemyTank;
	[SerializeField]
	private float	_radius;
	private Vector3 _spawnPos;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("InvokeSpawnEnemies", 0.4f, 15f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere (this.transform.position, this._radius);
	}

	void InvokeSpawnEnemies()
	{
		Vector3 randomPos = this.transform.position +  Random.insideUnitSphere * this._radius;

		if (randomPos.y > 10)
			randomPos.y = 10;
		NavMeshHit hit;
		if (NavMesh.SamplePosition (randomPos, out hit, this._radius, -1))
		{
			this._spawnPos = hit.position;
			GameObject.Instantiate (this.enemyTank, this._spawnPos, this.transform.rotation);
		}
	}
}
