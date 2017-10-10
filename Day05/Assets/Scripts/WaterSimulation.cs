using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSimulation : MonoBehaviour {

	// Use this for initialization

	private Renderer _renderer;
	private float	_speed;

	void Start () {
		this._renderer = GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		this._speed += 0.02f * Time.deltaTime;
		this._renderer.material.SetTextureOffset ("_MainTex", new Vector2(this._speed, 0));
	}
}
