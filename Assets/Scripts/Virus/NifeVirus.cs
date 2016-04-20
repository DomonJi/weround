using UnityEngine;
using System.Collections;

public class NifeVirus : Virus
{

	public GameObject nifeNife;
	Line linetouched;

	public override void Push ()
	{
		ObjectManager.Instance.Push ("NifeVirus", gameObject);
	}

	protected override void Init ()
	{
		level = 3;
		canBeDestroyed = true;
	}

	protected override void OnMouseDown ()
	{
		if (SceneController.Instance.canVBoom && canEquipShield && !canBeDestroyed) {
			DisableShield ();
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.gameObject.name);
		if (other.gameObject.name == "Line") {
			GameObject nife = Instantiate (nifeNife, transform.position, Quaternion.identity) as GameObject;
			nife.transform.SetParent (transform);
			linetouched = other.gameObject.GetComponent<Line> ();
//			other.gameObject.GetComponent <Line> ().IsActive = false;
//			other.gameObject.GetComponent <Line> ().IsCollider = false;
//			SceneController.Instance.EndSelection ();
		}
	}

	public void NifeNife ()
	{
		StartCoroutine (KnifeStart ());
	}

	IEnumerator KnifeStart ()
	{
		linetouched.Broke ();
		SceneController.Instance.canOperate = false;
		yield return new WaitUntil (() => !linetouched.IsActive);
		yield return new WaitForSeconds (0.2f);
		SceneController.Instance.EndSelection ();
		SceneController.Instance.canOperate = true;
	}
}
