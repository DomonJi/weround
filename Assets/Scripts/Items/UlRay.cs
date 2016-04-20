using UnityEngine;
using System.Collections;

public class UlRay : Item
{
	public GameObject ulrayEffect;
	public GameObject ulrayScreen;

	public override void Init ()
	{
		base.Init ();
		cd = 15f;
		duration = 7f;
	}

	protected override void Effect ()
	{
		base.Effect ();
		Instantiate (ulrayScreen);
		StartCoroutine (UltravioletRay ());
	}

	IEnumerator UltravioletRay ()
	{
		foreach (Virus v in SceneController.Instance.virus) {
			v.movement.CanMove = false;
			GameObject rayEffect = Instantiate (ulrayEffect, v.transform.position, Quaternion.identity) as GameObject;
			rayEffect.transform.SetParent (v.transform);
			v.DisableFeature ();
		}
		yield return new WaitForSeconds (duration);

		foreach (Virus v in SceneController.Instance.virus) {
			v.movement.CanMove = true;
			Transform effect = v.transform.FindChild ("UlrayVEffect(Clone)");
			if (effect != null)
				Destroy (effect.gameObject);
			v.EnableFeatue ();
		}
	}
}
