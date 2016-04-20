using UnityEngine;
using System.Collections;

public class AntiMovement : Movement
{
	Vector2 distanceFormCenter;

	protected override void RandomMove ()
	{
		if (CanMove) {
			animator.SetTrigger ("Move");
			distanceFormCenter = (Vector2)transform.position - SceneController.Instance.centerOfAnti;
			float lengthFromCenter = distanceFormCenter.magnitude;
			if (lengthFromCenter < 1) {
				rgbody.MoveRotation (transform.eulerAngles.z + Vector2.Angle (transform.TransformDirection (-Vector3.right), distanceFormCenter));
			} else if (lengthFromCenter < 5) {
				if (Random.value < 0.45) {
					rgbody.MoveRotation (transform.eulerAngles.z + Vector2.Angle (transform.TransformDirection (-Vector3.right), distanceFormCenter));
				} else {
					rgbody.MoveRotation (Random.Range (0f, 360f));
				}
			} else if (Random.value < 0.15) {
				rgbody.MoveRotation (transform.eulerAngles.z + Vector2.Angle (transform.TransformDirection (-Vector3.right), distanceFormCenter));
			} else {
				rgbody.MoveRotation (Random.Range (0f, 360f));
			}
		}
	}
		
}
