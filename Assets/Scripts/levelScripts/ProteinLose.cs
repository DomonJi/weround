using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProteinLose : MonoBehaviour
{
	Text text;
	float timer;

	void Start ()
	{
		timer = Time.time;
		text = GetComponentInChildren<Text> ();
	}

	void FixedUpdate ()
	{
		if (Time.time > timer + 1)
			text.text = Mathf.Lerp (float.Parse (text.text), 0, 2 * Time.unscaledDeltaTime).ToString ().Split ('.') [0];
	}


}
