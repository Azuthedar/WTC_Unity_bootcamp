using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateText : MonoBehaviour {


	public gameManager gameManager;

	private Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		drawText ();
	}


	void drawText()
	{
		if (CompareTag("Lives_text"))
		{
			text.text = gameManager.playerHp.ToString();
		}
		if (CompareTag("Energy_text"))
		{
			text.text = gameManager.playerEnergy.ToString();
		}
	}
}
