using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Shop : SceneSingleton<Shop>
{

	public void Back ()
	{
		SceneManager.LoadScene ("Level" + (GameController.Instance.currentLevel + 1));
	}

	public void BuyExplode ()
	{
		if (PlayerPrefs.GetInt ("Gold") >= 500) {
			PlayerPrefs.SetInt ("Gold", PlayerPrefs.GetInt ("Gold") - 500);
			PlayerPrefs.SetInt ("Explode", PlayerPrefs.GetInt ("Explode") + 1);
		}
	}

	public void BuyAntibiotic ()
	{
		if (PlayerPrefs.GetInt ("Gold") >= 100) {
			PlayerPrefs.SetInt ("Gold", PlayerPrefs.GetInt ("Gold") - 100);
			PlayerPrefs.SetInt ("Antibiotic", PlayerPrefs.GetInt ("Antibiotic") + 1);
		}
	}

	public void BuyUlray ()
	{
		if (PlayerPrefs.GetInt ("Gold") >= 200) {
			PlayerPrefs.SetInt ("Gold", PlayerPrefs.GetInt ("Gold") - 200);
			PlayerPrefs.SetInt ("UlRay", PlayerPrefs.GetInt ("UlRay") + 1);
		}
	}
}
