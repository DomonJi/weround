using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Explode : Item
{

	public override void Init ()
	{
		base.Init ();
		cd = 15f;
		duration = 3.5f;
	}

	protected override void Effect ()
	{
		base.Effect ();
		StartCoroutine (Boom ());
	}

	IEnumerator Boom ()
	{
		SceneController.Instance.canVBoom = true;
		yield return new WaitForSeconds (duration);
		SceneController.Instance.canVBoom = false;
	}
}
