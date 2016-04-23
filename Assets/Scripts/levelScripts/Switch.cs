using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[SerializeField]
public enum SwitchType
{
	MUSIC,
	AUDIO
}

public class Switch : MonoBehaviour
{
	public SwitchType type;
	public Toggle toggle;

	void Start ()
	{
		toggle = GetComponent <Toggle> ();
		if (type == SwitchType.MUSIC) {
			toggle.isOn = PlayerPrefs.GetInt ("Music") < 1;
		} else {
			toggle.isOn = PlayerPrefs.GetInt ("Audio") < 1;
		}
	}

	public void SetOnOffAnim ()
	{
		GetComponent <Animator> ().SetBool ("IsOn", toggle.isOn);
	}

	public void SetOnOff ()
	{
		SetOnOffAnim ();
		if (type == SwitchType.MUSIC) {
			PlayerPrefs.SetInt ("Music", toggle.isOn ? 0 : 1);
		} else {
			PlayerPrefs.SetInt ("Audio", toggle.isOn ? 0 : 1);
		}
	}
}
