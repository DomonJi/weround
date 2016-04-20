using UnityEngine;
using System.Collections;

public class BackgroundWave : MonoBehaviour
{
	[SerializeField]BackgroundWave adjiont;
	public float speed = 1f;
	Vector3 startPos;
	//[SerializeField] float endPosX;
	float offset;

	void Start ()
	{
		startPos = transform.position;
		offset = adjiont.transform.position.x - transform.position.x;
	}

	void Update ()
	{
		
		if (speed > 0) {
			if (transform.position.x >= 4.75f) {
				transform.position = startPos;
			}
		} else {
			if (transform.position.x <= -4.75f) {
				transform.position = startPos;
			}
		}
		transform.Translate (Vector3.right * Time.smoothDeltaTime * speed);
	}


}
