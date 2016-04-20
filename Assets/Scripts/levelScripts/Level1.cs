using UnityEngine;
using System.Collections;

public class Level1 : Level
{

	public override void Selected ()
	{
		base.Selected ();
		GetComponent <Wind> ().canWind = true;
	}

	public override void UnSelected ()
	{
		base.UnSelected ();
		GetComponent <Wind> ().canWind = false;
	}
}
