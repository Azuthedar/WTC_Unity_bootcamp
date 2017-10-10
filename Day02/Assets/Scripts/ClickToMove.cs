using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {

	public AudioClip	_movementSound;

	private Vector3		_targetPos;
	[SerializeField]
	private float		_speed;
	private AudioSource _audSrc;

	void Start () {
		this._targetPos = this.transform.position;
		this._audSrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position = Vector2.MoveTowards (this.transform.position, this._targetPos, this._speed * Time.deltaTime);
	}


	public void playerActions()
	{
		this.playerInput ();
		this.playMovementSound ();
		this.playAnimations ();
	}

	void castRay()
	{
		Vector2 mousePos = Input.mousePosition;
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (mousePos), Vector2.zero);
		if (hit.collider != null && hit.collider.tag == "Tile")
			this._targetPos = hit.point;
	}

	void playerInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.castRay ();
			this.lookRotation ();
		}

	}

	void playMovementSound()
	{
		if (this.transform.position != this._targetPos && !this._audSrc.isPlaying)
		{
			this._audSrc.clip = this._movementSound;
			this._audSrc.Play ();
		}
	}

	void lookRotation()
	{
		Vector3 moveDirection = this._targetPos - this.transform.position; 
		if (moveDirection != Vector3.zero) 
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		}
	}

	void playAnimations()
	{
		Animator animator = GetComponent<Animator> ();
		if (CompareTag ("Footman"))
		{
			//animator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Animations/Footman_0", typeof(RuntimeAnimatorController ));
			if (Vector2.Distance (this.transform.position, this._targetPos) < 0.05f)
			{
				animator.SetBool ("isRunning", false);
			}
			else if (this.transform.position != this._targetPos)
			{
				animator.SetBool ("isRunning", true);
			}
		}
	}
}
