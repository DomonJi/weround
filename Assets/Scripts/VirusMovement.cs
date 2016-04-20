using UnityEngine;
using System.Collections;

public class VirusMovement : Movement
{
	protected Vector2 distanceFormCenter;

	protected override void Init ()
	{
		moveForce = 1500f;
	}

	protected override void RandomMove ()
	{
		if (CanMove) {
			animator.SetTrigger ("Move");
			distanceFormCenter = SceneController.Instance.centerOfAnti - (Vector2)transform.position;
			float lengthFromCenter = distanceFormCenter.magnitude;
			if (lengthFromCenter > 5.5) {
				if (Random.value < 0.5) {
					rgbody.MoveRotation (transform.eulerAngles.z + Vector2.Angle (transform.TransformDirection (-Vector3.right), distanceFormCenter));
				} else {
					rgbody.MoveRotation (Random.Range (0f, 360f));
				}
			} else if (lengthFromCenter > 4) {
				if (Random.value < 0.4) {
					rgbody.MoveRotation (transform.eulerAngles.z + Vector2.Angle (transform.TransformDirection (-Vector3.right), distanceFormCenter));
				} else {
					rgbody.MoveRotation (Random.Range (0f, 360f));
				}
			} else if (Random.value < 0.25) {
				rgbody.MoveRotation (transform.eulerAngles.z + Vector2.Angle (transform.TransformDirection (-Vector3.right), distanceFormCenter));
			} else {
				rgbody.MoveRotation (Random.Range (0f, 360f));
			}
		}
	}
		
}
