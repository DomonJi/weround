using UnityEngine;
using System.Collections;

public class ShieldVirus : Virus
{
	public float shieldRecoverTime = 8f;

	protected override void Init ()
	{
		level = 1;
		canBeDestroyed = false;
	}

	public override void Die ()
	{
		if (canBeDestroyed)
			base.Die ();
		else {
			DisableShield ();
		}
	}

	public override void Push ()
	{
		ObjectManager.Instance.Push ("ShieldVirus", gameObject);
	}

	public override void DisableFeature ()
	{
		DisableShield ();
		StopCoroutine (RecoverShield ());
	}

	public override void EnableFeatue ()
	{
		StartCoroutine (RecoverShield ());
	}

	IEnumerator RecoverShield ()
	{
		//set animation
		yield return new WaitForSeconds (shieldRecoverTime);
		EnableShield ();
	}

	void EnableShield ()
	{
		canBeDestroyed = false;
		level = 1;
		animator.SetBool ("HasShield", true);
	}

	void DisableShield ()
	{
		canBeDestroyed = true;
		level = 0;
		animator.SetBool ("HasShield", false);
		StartCoroutine (RecoverShield ());
	}
}
