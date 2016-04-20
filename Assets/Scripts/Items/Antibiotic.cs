using UnityEngine;
using System.Collections;

public class Antibiotic : Item
{

	public override void Init ()
	{
		base.Init ();
		cd = 15f;
		duration = 10f;
	}

	protected override void Effect ()
	{
		base.Effect ();
		StartCoroutine (Antibio ());
	}

	IEnumerator Antibio ()
	{
		SceneController.Instance.canVInfect = false;
		yield return new WaitForSeconds (duration);
		SceneController.Instance.canVInfect = true;
	}
}
