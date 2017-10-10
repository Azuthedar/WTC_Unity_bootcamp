using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSelection : MonoBehaviour {


	private List<GameObject> footmenList;

	// Use this for initialization
	void Start () {
		footmenList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerInput ();


		if (footmenList != null)
		{
			for (int i = 0; i < footmenList.Count; i++)
			{
				footmenList [i].GetComponent<ClickToMove> ().playerActions ();
			}
		}
	}

	void playerInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePos = Input.mousePosition;
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (mousePos), Vector2.zero);

			if (hit.collider != null)
			{
				if (hit.collider.tag == "Footman")
				{
					if (!Input.GetKey (KeyCode.LeftControl))
						footmenList.Clear ();
					footmenList.Add (hit.collider.gameObject);
				}
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			footmenList.Clear ();
		}
	}
}
