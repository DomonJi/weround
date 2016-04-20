using UnityEngine;
using System.Collections;

public class InfectedAnti : Virus
{
	protected override void Init ()
	{
		level = -1;
		canBeDestroyed = true;
	}

	public override void Push ()
	{
		ObjectManager.Instance.Push ("InfectedAnti", gameObject);
	}
}
