using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject Thomas;
	public GameObject John;
	public GameObject Claire;


	private bool _inPos_Thomas = false;
	private bool _inPos_John = false;
	private bool _inPos_Claire = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		nextLevel ();
	}

	void nextLevel()
	{
		this._inPos_Thomas = Thomas.GetComponent<playerScript_ex01>().getInPos ();
		this._inPos_John = John.GetComponent<playerScript_ex01> ().getInPos ();
		this._inPos_Claire = Claire.GetComponent<playerScript_ex01> ().getInPos ();

		if (this._inPos_Thomas && this._inPos_John && this._inPos_Claire)
		{
			Debug.Log ("Completed Level");
			SceneManager.LoadScene(Application.loadedLevel + 1);
		}
	}

}
