using UnityEngine;
using System.Collections;
using System;

public class Wind : MonoBehaviour
{
	public GameObject wind;
	Vector2 startPos;
	public bool canWind = true;
	// Use this for initialization
	void Start ()
	{
		startPos = wind.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!canWind)
			return;
		wind.transform.localPosition += new Vector3 (-.5f * Time.deltaTime, 0f, 0f);
		wind.GetComponent <SpriteRenderer> ().color = new Color (1, 1, 1, 2 - Mathf.Abs (Mathf.Lerp (-2, 2, (wind.transform.localPosition.x + 1.6f) / (startPos.x + 1.6f))));
		if (wind.transform.localPosition.x < -1.6f) {
			wind.transform.localPosition = startPos;
		}
	}
}
