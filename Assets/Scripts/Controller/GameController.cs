using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;

[SerializeField]
public enum GameMode
{
	develop = 0,
	publish = 1
}

public class GameController : AllSceneSingleton<GameController>
{
	public int currentLevel = 0;
	public int currentRound = 0;
	public int unlockLevel = 0;
	public int unlockRound = 0;
	public int gold = 0;
	public GameMode mode = GameMode.develop;
	public RoundConfig roundConfigs;

	void Start ()
	{
		if (mode == GameMode.develop) {
			PlayerPrefs.SetInt ("UnLockLevel", 3);
			PlayerPrefs.SetInt ("UnLockRound", 4);
			PlayerPrefs.SetInt ("Gold", 3000);
		}
		unlockLevel = PlayerPrefs.GetInt ("UnLockLevel");
		unlockRound = PlayerPrefs.GetInt ("UnLockRound");
		gold = PlayerPrefs.GetInt ("Gold");
//		StreamReader sr = new StreamReader ("Assets/Resources/roundConfigs.json");
//		roundConfigs = JsonMapper.ToObject<RoundConfig> (sr.ReadToEnd ());
//		sr.Close ();
		//roundConfigs = JsonMapper.ToObject<RoundConfig> ("{\"levels\":[[{\"waves\":[[1,1,0,1,1],[3,0,0,0,1]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0}],[{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0}],[{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0}],[{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0}]]}");
		roundConfigs = JsonMapper.ToObject <RoundConfig> ("{\"levels\":[[{\"waves\":[[2,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[4,0,0,0,0],[5,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,0,0,0,0],[3,0,0,0,0]],\"antiNum\":4,\"canVInfect\":0},{\"waves\":[[3,1,0,0,0],[3,2,0,0,0],[4,2,0,0,0],[5,3,0,0,0]],\"antiNum\":5,\"canVInfect\":0},{\"waves\":[[3,3,0,0,0],[4,4,0,0,0],[5,5,0,0,0],[6,6,0,0,0]],\"antiNum\":7,\"canVInfect\":1}],[{\"waves\":[[3,0,0,1,0],[1,2,0,2,0]],\"antiNum\":5,\"canVInfect\":1},{\"waves\":[[1,3,0,3,0],[3,2,0,4,0],[0,4,0,4,0]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[0,0,0,4,0],[1,4,0,5,0],[0,6,0,6,0]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[2,0,0,3,0],[2,3,0,3,0],[2,6,0,4,0]],\"antiNum\":5,\"canVInfect\":1},{\"waves\":[[0,2,0,4,0],[3,8,0,5,0],[0,6,0,6,0],[0,6,0,6,0]],\"antiNum\":5,\"canVInfect\":1}],[{\"waves\":[[3,0,1,0,0],[3,3,3,0,0]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[3,0,2,2,0],[1,3,3,2,0],[1,3,3,2,0]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[0,3,3,2,0],[0,5,4,3,0],[1,5,4,3,0]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[0,4,4,0,0],[0,5,5,0,0],[0,6,6,0,0]],\"antiNum\":7,\"canVInfect\":1},{\"waves\":[[0,2,4,0,0],[0,6,4,3,0],[2,6,3,3,0],[2,6,3,3,0]],\"antiNum\":5,\"canVInfect\":1}],[{\"waves\":[[3,0,1,1,1],[3,0,2,2,1]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[3,3,2,2,1],[2,7,2,2,1]],\"antiNum\":7,\"canVInfect\":1},{\"waves\":[[2,0,2,2,2],[1,3,2,2,3],[1,3,2,2,3],[1,7,2,2,3]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[0,7,2,2,3],[0,6,2,1,4],[0,6,2,0,4]],\"antiNum\":6,\"canVInfect\":1},{\"waves\":[[0,1,0,0,3],[0,3,0,0,3],[0,6,1,1,4],[0,8,2,2,4]],\"antiNum\":5,\"canVInfect\":1}]]}");
		Debug.Log (roundConfigs.levels [0] [0].antiNum);
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}
