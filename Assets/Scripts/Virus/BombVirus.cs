using UnityEngine;
using System.Collections;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SocialPlatforms;

public class BombVirus : Virus
{
	public float bombForceFactor;

	protected override void OnEnable ()
	{
		base.OnEnable ();
		StartCoroutine (Expand ());
		transform.FindChild ("Expression").gameObject.SetActive (true);
	}

	protected override void Init ()
	{
		level = 2;
		canBeDestroyed = true;
		bombForceFactor = 800f;
	}

	IEnumerator Expand ()
	{
		for (int i = 0; i < 6; i++) {
			yield return new WaitForSeconds (SceneController.Instance.tickInterval);
		}
		if (canBeDestroyed) {
			animator.SetTrigger ("Bomb");
			transform.FindChild ("Expression").gameObject.SetActive (false);
		}
	}

	public override void Die ()
	{
		base.Die ();
		StopCoroutine (Expand ());
		DisableFeature ();
	}

	public override void Push ()
	{
		ObjectManager.Instance.Push ("BombVirus", gameObject);
	}

	public override void DisableFeature ()
	{
		base.DisableFeature ();
		StopCoroutine (Expand ());
	}

	public override void EnableFeatue ()
	{
		base.DisableFeature ();
		StartCoroutine (Expand ());
	}

	public void Bomb ()
	{
		ObjectManager.Instance.BombBomb (this);

		if (SceneController.Instance)
			SceneController.Instance.virus.Remove (this);
		movement.enabled = false;
		collider.enabled = false;
		rgbody.Sleep ();
	}

	public override void DisableShield ()
	{
		if (!canEquipShield)
			return;
		canBeDestroyed = true;
		level = 0;
		Debug.Log (shieldAnimator);
		shieldAnimator.SetTrigger ("Shield");
		StartCoroutine (Expand ());
	}

	public override void EquipShield ()
	{
		base.EquipShield ();
		StopCoroutine (Expand ());
	}
}
