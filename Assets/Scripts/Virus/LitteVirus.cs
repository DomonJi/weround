using UnityEngine;
using System.Collections;

public class LitteVirus : Virus
{
	public override void Push ()
	{
		ObjectManager.Instance.Push ("LittleVirus", gameObject);
	}

	protected override void Init ()
	{
		level = 4;
		canBeDestroyed = true;
	}
}
