using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{

	public void StartGame ()
	{
		SceneManager.LoadScene ("LevelsSelect");
	}

	public void About ()
	{
		SceneManager.LoadScene ("About");
	}
}
