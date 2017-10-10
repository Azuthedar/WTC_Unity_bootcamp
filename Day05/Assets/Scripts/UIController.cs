using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	// Use this for initialization
	public GameObject ball;

	private RectTransform	_rect;

	void Start () {
		this._rect = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		this._rect.sizeDelta = new Vector2 (200 * ball.GetComponent<BallController> ().percentageCharge (), this._rect.rect.height);
	}
}
