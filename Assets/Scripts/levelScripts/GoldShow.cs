using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public enum ShowType
{
	Show,
	DelayShow
}

public class GoldShow : MonoBehaviour
{
	public ShowType showType = ShowType.Show;
	public int preGold;
	Text text;
	float timer;

	void Start ()
	{
		timer = Time.time;
		text = GetComponentInChildren<Text> ();
	}

	void FixedUpdate ()
	{
		if (showType == ShowType.Show)
			text.text = PlayerPrefs.GetInt ("Gold").ToString ();
		else {
			if (Time.time > timer + 1) {
				if (Mathf.Abs (PlayerPrefs.GetInt ("Gold") - float.Parse (text.text)) > 0.9) {
					Debug.Log (PlayerPrefs.GetInt ("Gold"));
					text.text = Mathf.Lerp (float.Parse (text.text), PlayerPrefs.GetInt ("Gold"), 2f * Time.unscaledDeltaTime).ToString ().Split ('.') [0];
				}
			} else {
				text.text = preGold.ToString ();
			}
		}
	}
}
