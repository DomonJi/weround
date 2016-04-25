using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public enum ShowType
{
	Show,
	DelayShowPlus,
	DelayShowMinus,
}

public class GoldShow : MonoBehaviour
{
	public ShowType showType = ShowType.Show;
	public int preGold;
	Text text;
	float timer;
	int baseFontSize;
	float realGold;

	void Start ()
	{
		timer = Time.time;
		text = GetComponentInChildren<Text> ();
		baseFontSize = text.fontSize;
		realGold = preGold;
		if (showType == ShowType.DelayShowMinus) {
			text.text = preGold.ToString ();
		}
	}

	void FixedUpdate ()
	{
		if (showType == ShowType.Show)
			text.text = PlayerPrefs.GetInt ("Gold").ToString ();
		else if (showType == ShowType.DelayShowPlus) {
			if (Time.time > timer + 0.8f) {
				if (PlayerPrefs.GetInt ("Gold") - float.Parse (text.text) > 0.9) {
					Debug.Log (PlayerPrefs.GetInt ("Gold"));
					//text.text = Mathf.Lerp (float.Parse (text.text), PlayerPrefs.GetInt ("Gold"), 2f * Time.unscaledDeltaTime).ToString ().Split ('.') [0];
					realGold += ((PlayerPrefs.GetInt ("Gold") - float.Parse (text.text)) * 1.3f + 10) * Time.unscaledDeltaTime;
					text.text = realGold.ToString ().Split ('.') [0];
					text.fontSize = (int)((1.08f + 0.08f * Mathf.Sin (50 * Time.time)) * baseFontSize);
				} else {
					text.fontSize = baseFontSize;
				}
			} else {
				text.text = preGold.ToString ();
			}
		} else {
			if (float.Parse (text.text) - PlayerPrefs.GetInt ("Gold") > 0.9) {
				realGold -= ((float.Parse (text.text) - PlayerPrefs.GetInt ("Gold")) * 5 + 50) * Time.unscaledDeltaTime;
				text.text = realGold.ToString ().Split ('.') [0];
				text.fontSize = (int)((0.94f + 0.06f * Mathf.Sin (50 * Time.time)) * baseFontSize);
			} else {
				text.fontSize = baseFontSize;
			}
		}
	}
}
