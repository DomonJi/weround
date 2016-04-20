using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

	protected Rigidbody2D rgbody;
	protected Animator animator;
	[SerializeField]protected float moveForce;

	bool canMove = true;

	public bool CanMove {
		get {
			return canMove;
		}
		set {
			canMove = value;
		}
	}

	void Awake ()
	{
		rgbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent <Animator> ();
		//Init ();
	}
	// Use this for initialization

	protected virtual void Init ()
	{
		//moveForce = 1500f;
	}

	void OnEnable ()
	{
		SceneController.Instance.tickEvent += RandomMove;
	}

	protected virtual void RandomMove ()
	{
		if (CanMove)
			rgbody.AddForce (new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized * moveForce);
	}

	void OnDisable ()
	{
		if (SceneController.Instance)
			SceneController.Instance.tickEvent -= RandomMove;
	}

	public void AddForce ()
	{
//		if (GetComponent <Virus> () != null) {
		rgbody.AddForce (transform.TransformDirection (-Vector3.right) * moveForce);
//			return;
//		}
//		rgbody.AddForce (transform.TransformDirection (Vector3.right) * moveForce);
	}
}
