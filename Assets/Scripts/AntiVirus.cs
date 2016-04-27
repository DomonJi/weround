using UnityEngine;

public class AntiVirus : MonoBehaviour
{
	public Line line;
	AntiVirus previous;

	public Movement movement;
	public Animator warningAnimator;
	Animator animator;
	Animator expression;
	Collider2D colliderTrigger;
	Collider2D collider;
	Rigidbody2D rgbody;

	public bool isSelected;
	static float doubleClickSpeed;

	float canBeInfectedTimer;

	void Awake ()
	{
		colliderTrigger = GetComponents<Collider2D> () [0];
		collider = GetComponents<Collider2D> () [1];
		rgbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		expression = transform.FindChild ("Expression").GetComponent <Animator> ();
		line = GetComponentInChildren<Line> ();
		movement = GetComponent<Movement> ();
		warningAnimator = transform.FindChild ("Warning").GetComponent <Animator> ();
		Init ();
	}

	void OnEnable ()
	{
		SceneController.Instance.antiVirus.Add (this);
		canBeInfectedTimer = Time.time;
		movement.enabled = true;
		colliderTrigger.enabled = true;
		collider.enabled = true;
		rgbody.WakeUp ();
		isSelected = false;
		transform.GetChild (1).gameObject.SetActive (true);
		if (Random.value > 0.5)
			expression.SetBool ("Expression2", true);
	}


	void Init ()
	{
		doubleClickSpeed = 0.4f;
	}

	void OnMouseDown ()
	{
		if (SceneController.Instance.paused) {
			return;
		}
		SceneController.Instance.focusedAnti = this;
		line.IsActive = true;
		line.IsCollider = false;
		SceneController.Instance.startSelecting = true;
		isSelected = true;
		movement.CanMove = false;
		rgbody.isKinematic = true;
		SceneController.Instance.antiVirusOnLine.Add (this);
		animator.SetBool ("Pressed", true);
		expression.SetBool ("Pressed", true);
	}

	void Update ()
	{
		if (previous && previous.line.IsActive) {
			previous.line.LineTo (transform.position);
			return;
		}
	}

	void FixedUpdate ()
	{
		if (SceneController.Instance.startSelecting) {
			if (((Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition) - (Vector2)transform.position).sqrMagnitude < 0.81f) {
				if (SceneController.Instance.antiVirusOnLine.Contains (this)) {
					if (previous == null && SceneController.Instance.antiVirusOnLine.Count > 2) {
						SceneController.Instance.CompleteSelection ();
					}
				}
			}
		}
	}

	void OnMouseEnter ()
	{

		if (SceneController.Instance.paused) {
			return;
		}
		if (SceneController.Instance.startSelecting) {
			if (SceneController.Instance.antiVirusOnLine.Contains (this)) {
//				if (previous == null && SceneController.Instance.antiVirusOnLine.Count > 2) {
//					SceneController.Instance.CompleteSelection ();
//				}
//
				return;
			}
			isSelected = true;
			animator.SetBool ("Pressed", true);
			expression.SetBool ("Pressed", true);
			SceneController.Instance.focusedAnti = this;
			SceneController.Instance.antiVirusOnLine.Add (this);
			if (SceneController.Instance.antiVirusOnLine.IndexOf (this) > 0) {
				previous = SceneController.Instance.antiVirusOnLine [SceneController.Instance.antiVirusOnLine.IndexOf (this) - 1];
			}
			line.IsActive = true;
			if (previous != null) {
				previous.line.IsActive = true;
				previous.line.IsCollider = true;
			}
			line.IsCollider = false;
			movement.CanMove = false;
			rgbody.isKinematic = true;
		}
	}

	void OnMouseExit ()
	{
		animator.SetBool ("Pressed", false);
	}

	public void Recover ()
	{
		isSelected = false;
		movement.CanMove = true;
		rgbody.isKinematic = false;
		line.IsActive = false;
		line.IsCollider = false;
		previous = null;
		animator.SetBool ("Pressed", false);
		expression.SetBool ("Pressed", false);
	}


	public void Die ()
	{

		if (SceneController.Instance) {
			SceneController.Instance.antiVirus.Remove (this);
		}
		transform.FindChild ("Expression").gameObject.SetActive (false);
		movement.enabled = false;
		colliderTrigger.enabled = false;
		collider.enabled = false;
		rgbody.Sleep ();
		animator.SetTrigger ("Die");
	}

	void OnDisable ()
	{
		if (SceneController.Instance) {
			if (SceneController.Instance.antiVirus.Contains (this))
				SceneController.Instance.antiVirus.Remove (this);
		}
	}

	public virtual void Push ()
	{
		ObjectManager.Instance.Push ("AntiVirus", gameObject);
	}
}
