using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour
{

	[SerializeField] bool isActive = false;
	[SerializeField] bool isCollider = false;

	public bool IsActive {
		get {
			return isActive;
		}
		set {
			isActive = value;
			if (GetComponent<SpriteRenderer> ())
				GetComponent<SpriteRenderer> ().enabled = value;
		}
	}

	public bool IsCollider {
		get {
			return isCollider;
		}
		set {
			isCollider = value;
			if (GetComponent<PolygonCollider2D> ())
				GetComponent<PolygonCollider2D> ().enabled = value;
		}
	}

	void Awake ()
	{
		IsActive = false;
		IsCollider = false;
	}

	public void LineTo (Vector2 pos)
	{
		float rotateAngle = Vector2.Angle (new Vector2 (1, 0), pos - new Vector2 (transform.position.x, transform.position.y));
		if ((pos - new Vector2 (transform.position.x, transform.position.y)).y < 0) {
			rotateAngle = 360 - rotateAngle;
		}
		transform.rotation = Quaternion.Euler (0, 0, rotateAngle);
		transform.localScale = new Vector3 ((pos - new Vector2 (transform.position.x, transform.position.y)).magnitude * 1f / 2.9f, 1.2f, 1);
	}

	public void Disable ()
	{
		IsActive = false;
		IsCollider = false;
	}

	public void Enable ()
	{
		IsActive = true;
		IsCollider = true;
	}

	public void Broke ()
	{
		GetComponent <Animator> ().SetTrigger ("Broken");
	}
		
}
