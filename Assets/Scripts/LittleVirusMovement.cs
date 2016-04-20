using UnityEngine;
using System.Collections;

public class LittleVirusMovement : VirusMovement
{
	protected override void RandomMove ()
	{
		StartCoroutine (FastMove ());
	}

	IEnumerator FastMove ()
	{
		base.RandomMove ();
		yield return new WaitForSeconds (SceneController.Instance.tickInterval / 2);
		base.RandomMove ();
	}
}
