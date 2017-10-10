using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

	// Use this for initialization

	private Button _startButton;
	private Button _exitButton;
	void Start ()
	{
		if (CompareTag("Start_button"))
		{
			this._startButton = GetComponent<Button> ();
			this._startButton.onClick.AddListener (TaskOnClickStart);
		}
		else if (CompareTag("Exit_button"))
		{
			this._exitButton = GetComponent<Button> ();
			this._exitButton.onClick.AddListener (TaskOnClickExit);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClickStart()
	{
		SceneManager.LoadScene ("ex01");
	}

	void TaskOnClickExit()
	{
		Application.Quit ();
	}
}
