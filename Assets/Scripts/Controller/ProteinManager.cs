using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProteinManager : SceneSingleton<ProteinManager>
{
	public int protein;
	public int antiConsume;
	[SerializeField]int L0Protein;
	[SerializeField]int L1Protein;
	[SerializeField]int L2Protein;
	[SerializeField]int L3Protein;
	[SerializeField]int L4Protein;
	[SerializeField]float power;
	[SerializeField]int accelerateNum;

	public float proteinFactor = 1;


	[SerializeField]Text[] text;
	[SerializeField]Button produceButton;

	int helpTimes = 0;

	void Start ()
	{
		for (int i = 0; i < text.Length; i++) {
			text [i].text = protein.ToString ();
		}
		Init ();
		helpTimes = 0;
	}

	void Init ()
	{
		protein = 0;
		L0Protein = 20;
		L1Protein = 30;
		L2Protein = 40;
		L3Protein = 50;
		L4Protein = 10;
		power = 1.5f;
		proteinFactor = 1;
		accelerateNum = 8;
	}

	public void WinProtein (int num)
	{
		protein += num;
		for (int i = 0; i < text.Length; i++) {
			text [i].text = protein.ToString ();
		}
		produceButton.interactable |= protein >= antiConsume + helpTimes * accelerateNum;
	}

	public void LoseProtein (int num)
	{
		protein -= num;
		for (int i = 0; i < text.Length; i++) {
			text [i].text = protein.ToString ();
		}
		produceButton.interactable &= protein >= antiConsume + (helpTimes + 1) * accelerateNum;
	}

	public void CalcuProtein (Virus[] virusToDie)
	{
		int L0Num = 0;
		int L1Num = 0;
		int L2Num = 0;
		int L3Num = 0;
		int L4Num = 0;
		foreach (Virus v in virusToDie) {
			if (!v.canBeDestroyed)
				continue;
			if (v.level == 1) {
				L1Num++;
			} else if (v.level == 0) {
				L0Num++;
			} else if (v.level == 2) {
				L2Num++;
			} else if (v.level == 3) {
				L3Num++;
			} else if (v.level == 4) {
				L4Num++;
			}
		}
		int winProtein = (int)Mathf.Pow (L0Num, power) * L0Protein + (int)Mathf.Pow (L2Num, power) * L2Protein +
		                 (int)Mathf.Pow (L1Num, power) * L1Protein +
		                 (int)Mathf.Pow (L3Num, power) * L3Protein +
		                 (int)Mathf.Pow (L4Num, power) * L4Protein;
		WinProtein ((int)(winProtein * proteinFactor));
	}

	public void ProduceAnti ()
	{
		LoseProtein (antiConsume + helpTimes * accelerateNum);
		ObjectManager.Instance.SpawnAnti (1);
		helpTimes++;
	}
}
