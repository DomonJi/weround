using UnityEngine;
using System.Collections;

public class CamouflageVirus : Virus
{
	[SerializeField]float timeToCamou;

	public override void Die ()
	{
		if (!canEquipShield || canBeDestroyed) {
			if (SceneController.Instance)
				SceneController.Instance.virus.Remove (this);
			movement.enabled = false;
			collider.enabled = false;
			rgbody.Sleep ();
			StopCoroutine (Camouflage ());
			transform.FindChild ("Expression").gameObject.SetActive (false);
			animator.SetTrigger ("Die");
		} else {
			DisableShield ();
		}
	}

	protected override void OnEnable ()
	{
		base.OnEnable ();
		transform.FindChild ("Expression").gameObject.SetActive (true);
		StartCoroutine (Camouflage ());
	}

	protected override void Init ()
	{
		canBeDestroyed = true;
		level = 1;
		timeToCamou = 10f;
	}

	IEnumerator Camouflage ()
	{
		while (true) {
			yield return new WaitForSeconds (timeToCamou);
			Camou ();
			yield return new WaitForSeconds (timeToCamou / 2);
			DisCamou ();
		}
	}

	public override void Push ()
	{
		ObjectManager.Instance.Push ("CamouflagVirus", gameObject);
	}

	void Camou ()
	{
		//canBeDestroyed = false;
		animator.SetBool ("IsCamou", true);
		transform.FindChild ("Expression").gameObject.SetActive (false);
	}

	void DisCamou ()
	{
		//canBeDestroyed = true;
		animator.SetBool ("IsCamou", false);
	}

	public override void DisableFeature ()
	{
		base.DisableFeature ();
		DisCamou ();
		StopCoroutine (Camouflage ());
	}

	public override void EnableFeatue ()
	{
		base.DisableFeature ();
		StartCoroutine (Camouflage ());
	}

	public void CamouEvent ()
	{
		if (animator.GetBool ("IsCamou")) {
			canBeDestroyed = false;
			movement.CanMove = false;
		} else {
			canBeDestroyed = true;
			movement.CanMove = true;
		}
	}

	public void AddFace ()
	{
		if (!animator.GetBool ("IsCamou"))
			transform.FindChild ("Expression").gameObject.SetActive (true);
	}
}
