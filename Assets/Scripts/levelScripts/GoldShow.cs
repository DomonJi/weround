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
	float realGold;
	public RectTransform rect;

	void Start ()
	{
		timer = Time.time;
		text = GetComponentInChildren<Text> ();
		realGold = preGold;
		if (showType == ShowType.DelayShowMinus) {
			text.text = preGold.ToString ();
			Debug.Log (preGold);
		}
		rect = GetComponent <RectTransform> ();
	}

	void FixedUpdate ()
	{
		if (showType == ShowType.Show)
			text.text = PlayerPrefs.GetInt ("Gold").ToString ();
		else if (showType == ShowType.DelayShowPlus) {
			if (Time.time > timer + 0.8f) {
				if (PlayerPrefs.GetInt ("Gold") - float.Parse (text.text) > 0.9) {
					//text.text = Mathf.Lerp (float.Parse (text.text), PlayerPrefs.GetInt ("Gold"), 2f * Time.unscaledDeltaTime).ToString ().Split ('.') [0];
					realGold += ((PlayerPrefs.GetInt ("Gold") - float.Parse (text.text)) * 1.3f + 10) * Time.unscaledDeltaTime;
					text.text = realGold.ToString ().Split ('.') [0];
					float scaleFactor = 1.1f + 0.1f * Mathf.Sin (60 * Time.time);
					rect.localScale = new Vector3 (scaleFactor, scaleFactor, 1);
				} else {
					rect.localScale = new Vector3 (1, 1, 1);
				}
			} else {
				text.text = preGold.ToString ();
			}
		} else {
			if (float.Parse (text.text) - PlayerPrefs.GetInt ("Gold") > 0.9) {
				realGold -= ((float.Parse (text.text) - PlayerPrefs.GetInt ("Gold")) * 5 + 50) * Time.unscaledDeltaTime;
				text.text = realGold.ToString ().Split ('.') [0];
				float scaleFactor = 0.92f + 0.08f * Mathf.Sin (60 * Time.time);
				rect.localScale = new Vector3 (scaleFactor, scaleFactor, 1);
			} else {
				rect.localScale = new Vector3 (1, 1, 1);
			}
		}
	}
}
