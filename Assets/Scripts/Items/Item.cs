using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
	[SerializeField]protected float cd;
	[SerializeField]protected bool condition;
	[SerializeField]protected int cost;
	[SerializeField]protected Button buttom;
	[SerializeField]protected float duration;

	public int num;
	 
	protected float cdTimer;

	void Awake ()
	{
		Init ();
		buttom.interactable = num > 0 && condition;
	}

	public void StartGame ()
	{
		cdTimer = Time.time;
		GetComponentInChildren <Text> ().text = num.ToString ();
	}

	public virtual void Init ()
	{
		condition = true;
		num = PlayerPrefs.GetInt (GetType ().Name);
	}

	public void Trigge ()
	{
		if (num > 0) {
			cdTimer = Time.time;
			Effect ();
			StartCoroutine (StartCD ());
			ItemManager.Instance.DisableOthers ();
		}
	}

	protected virtual void Effect ()
	{
		num--;
		PlayerPrefs.SetInt (GetType ().Name, num);
		GetComponentInChildren <Text> ().text = num.ToString ();
	}

	protected virtual IEnumerator StartCD ()
	{
		buttom.interactable = false;
		yield return new WaitUntil (() => Time.time > cdTimer + cd);
		ItemManager.Instance.EnableOthers ();
		buttom.interactable = num > 0 && condition;
	}


	public void Disable ()
	{
		buttom.interactable = false;
	}

	public void Enable ()
	{
		buttom.interactable = num > 0 && Time.time > cdTimer + cd;
	}
}
