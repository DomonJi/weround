using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoundSelect : SceneSingleton<RoundSelect>
{

	public Sprite completedButn;
	public Sprite curntButn;
	public Sprite lockedButn;
	public Sprite pressedCoompleted;
	public Sprite pressedCurnt;
	public string mainScene = "main";
	public RoundButton[] rounds;
	public int levelIndex;
	public GameObject ButtonEffect;

	// Use this for initialization
	//	void Start ()
	//	{
	//		if (levelIndex == GameController.Instance.unlockLevel) {
	//			for (int i = 0; i < GameController.Instance.unlockRound; i++) {
	//				rounds [i].GetComponent <SpriteRenderer> ().sprite = completedButn;
	//			}
	//			rounds [GameController.Instance.unlockRound].GetComponent <SpriteRenderer> ().sprite = curntButn;
	//		} else if (levelIndex < GameController.Instance.unlockLevel) {
	//			for (int i = 0; i < GameController.Instance.unlockRound+1; i++) {
	//				rounds [i].GetComponent <SpriteRenderer> ().sprite = completedButn;
	//			}
	//		}
	//	}

	public void Back ()
	{
		SceneManager.LoadScene ("LevelsSelect", LoadSceneMode.Single);
	}

	public void Shop ()
	{
		//SceneManager.LoadScene ("Level" + (GameController.Instance.currentLevel + 1));
		SceneManager.LoadScene ("Shop");
	}
}
