using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour {

	public GameObject player;

	private Text		_text;

	void Start () {
		this._text = GetComponent<Text> ();	
	}

	// Update is called once per frame
	void Update () {
		this._text.text = this.player.GetComponent<PlayerController> ().getLevel ().ToString ();
	}
}