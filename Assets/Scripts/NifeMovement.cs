using UnityEngine;
using System.Collections;

public class NifeMovement : VirusMovement
{
	protected override void Init ()
	{
		moveForce = 1500f;
	}

	protected override void RandomMove ()
	{
		if (CanMove) {
			float ndistanceFormCenter = ((Vector2)transform.position - SceneController.Instance.centerOfAnti).magnitude;
			if (ndistanceFormCenter > 7) {
				if (Random.value < 0.7) {
					rgbody.AddForce (-transform.position.normalized * moveForce);
				} else {
					rgbody.AddForce (new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized * moveForce);
				}
			} else if (ndistanceFormCenter > 5) {
				if (Random.value < 0.45) {
					rgbody.AddForce (-transform.position.normalized * moveForce);
				} else {
					rgbody.AddForce (new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized * moveForce);
				}
			} else if (Random.value < 0.3) {
				rgbody.AddForce (-transform.position.normalized * moveForce);
			} else {
				rgbody.AddForce (new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)).normalized * moveForce);
			}
			rgbody.AddTorque (1000 * 2 / SceneController.Instance.tickInterval);
		}
	}
}
