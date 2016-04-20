using UnityEngine;
using System.Collections;

public class BackgroundController : SceneSingleton<BackgroundController>
{

	public GameObject[] backgrounds;
	public Color[] backgroundColors;

	void Start ()
	{
		Camera.main.backgroundColor = backgroundColors [GameController.Instance.currentLevel];
		backgrounds [GameController.Instance.currentLevel].SetActive (true);
	}
}
