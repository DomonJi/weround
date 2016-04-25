using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Policy;


public class SceneController : SceneSingleton<SceneController>
{
	public List<AntiVirus> antiVirus;
	public List<AntiVirus> antiVirusOnLine;
	public List<Virus> virus;
	public List<Virus> virusToDie;
	//public List<AntiVirus> antiCanInfect;

	public bool startSelecting;
	public bool completeSelecting;
	public Vector2 centerOfAnti;
	public AntiVirus focusedAnti;

	[SerializeField]Camera mainCamera;
	[SerializeField]Canvas gameCanvas;
	[SerializeField]Canvas menuCanvas;
	[SerializeField]Canvas loseCanvas;
	[SerializeField]Canvas winCanvas;
	[SerializeField]PolygonCollider2D checkInRangeCollider;

	[HideInInspector]public bool paused;
	[HideInInspector]public bool noVirusInScene;
	public bool canOperate = true;

	//List<Vector3> crossResults;
	//store the results of vector cross

	public delegate void TickHandler ();

	public event TickHandler tickEvent;

	public float tickInterval;
	float tickTimer;

	public bool canVInfect;
	public bool canVBoom;

	public float timeToInfect;
	float infectTimer;

	[SerializeField]float crazyTimer;

	void Awake ()
	{
		antiVirus = new List<AntiVirus> ();
		virus = new List<Virus> ();
		antiVirusOnLine = new List<AntiVirus> ();
		//crossResults = new List<Vector3> ();
		virusToDie = new List<Virus> ();
		virus = new List<Virus> ();
//		antiCanInfect = new List<AntiVirus> ();
		menuCanvas.gameObject.SetActive (false);
		checkInRangeCollider = GetComponent<PolygonCollider2D> (); 
		Init ();
		paused = true;
	}

	void Start ()
	{
		ItemManager.Instance.StartGame ();
		StartCoroutine (SpawnAnti ());
		ObjectManager.Instance.ShowWaveText ();
		Invoke ("StartGame", 3.5f);
	}

	IEnumerator SpawnAnti ()
	{
		int antiNum = GameController.Instance.roundConfigs.levels [GameController.Instance.currentLevel] [GameController.Instance.currentRound].antiNum;
		for (int i = 0; i < antiNum; i++) {
			yield return new WaitForSeconds (2.5f / antiNum);
			Debug.Log (antiNum);
			ObjectManager.Instance.SpawnAnti (1);
		}
	}

	public void StartGame ()
	{
		paused = false;
		infectTimer = Time.time;
		canVInfect = GameController.Instance.roundConfigs.levels [GameController.Instance.currentLevel] [GameController.Instance.currentRound].canVInfect > 0;
		tickTimer = Time.time;
		StartCoroutine (Infect ());
		tickEvent += CalcuACenter;
		//antiCanInfect = new List<AntiVirus> (antiVirus);
		ObjectManager.Instance.StartSpawn ();
	}

	void Init ()
	{
		canVInfect = true;
		timeToInfect = 8f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (paused) {
			return;
		}
		if (Time.time > tickTimer + tickInterval) {
			tickEvent ();
			tickTimer = Time.time;
		}
		if (Time.time > crazyTimer + 8f) {
			tickInterval += 0.5f;
			if (tickInterval > 2f) {
				tickInterval = 2f;
			}
			ProteinManager.Instance.proteinFactor = 1;
		}
		if (Input.GetMouseButtonUp (0)) {
			EndSelection ();
			canOperate = true;
		}

		if (startSelecting) {
			focusedAnti.line.LineTo (mainCamera.ScreenToWorldPoint (Input.mousePosition));
		}
//		antiCanInfect = new List<AntiVirus> (antiVirus);
//		foreach (AntiVirus a in antiVirusOnLine)
//			antiCanInfect.Remove (a);
	}

	public void EndSelection ()
	{
		foreach (AntiVirus a in antiVirusOnLine) {
			a.Recover ();
		}
		antiVirusOnLine.Clear ();
		//crossResults.Clear ();
		virusToDie.Clear ();
		startSelecting = false;
		completeSelecting = false;
	}

	public void CompleteSelection ()
	{
		if (!canOperate)
			return;
		TestVirusInRange ();
		int canDestroyNum = 0;
		ProteinManager.Instance.CalcuProtein (virusToDie.ToArray ());
		foreach (Virus v in virusToDie) {
			if (v.canBeDestroyed) {
				canDestroyNum++;
			}
			v.Die ();
		}
		if (canDestroyNum > 0) {
			foreach (AntiVirus anti in antiVirusOnLine) {
				anti.Die ();
			}
			if (antiVirus.Count < 1) {
				ObjectManager.Instance.Duplicate (2);
			}
			ObjectManager.Instance.Duplicate (canDestroyNum + 2);
			tickInterval -= 0.2f;
			if (tickInterval < 1.2f)
				tickInterval = 1.2f;
			ProteinManager.Instance.proteinFactor += 0.1f;
			if (canDestroyNum > 5) {
				tickInterval -= 0.4f;
				if (tickInterval < 1f)
					tickInterval = 1f;
				ProteinManager.Instance.proteinFactor = 2f;
			}
			crazyTimer = Time.time;
		}
		CheckGameOver ();
		EndSelection ();
	}


	void TestVirusInRange ()
	{
		Vector2[] path = new Vector2[antiVirusOnLine.Count];
		for (int i = 0; i < antiVirusOnLine.Count; i++) {
			path [i] = (Vector2)antiVirusOnLine [i].transform.position;
		}
		checkInRangeCollider.SetPath (0, path);
		foreach (Virus v in virus) {
			/*
			v.shouldRemain = false;
			Vector3 firstResult = Vector3.zero;
			for (int i = 0; i < antiVirusOnLine.Count; i++) {
				Vector3 crossResult = Vector3.Cross (antiVirusOnLine [(i + 1) % antiVirusOnLine.Count].transform.position - antiVirusOnLine [i].transform.position,
					                      v.transform.position - antiVirusOnLine [i].transform.position);
				if (i == 0) {
					firstResult = crossResult;
				}
				if (crossResult.z * firstResult.z < 0) {
					v.shouldRemain = true;
				}
			}
			if (!v.shouldRemain) {
				virusToDie.Add (v);
			}*/
			if (checkInRangeCollider.OverlapPoint ((Vector2)v.transform.position)) {
				virusToDie.Add (v);
			}
		}
	}

	public void Pause ()
	{
		Time.timeScale = 0;
		paused = true;
		menuCanvas.gameObject.SetActive (true);
	}

	public void Resume ()
	{
		paused = false;
		menuCanvas.GetComponent <Animator> ().SetTrigger ("Resume");
	}

	public void ReStart ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("main");
	}

	IEnumerator Infect ()
	{
		while (true) {
			yield return new WaitUntil (() => Time.time > infectTimer + timeToInfect /*&& antiCanInfect.Count > 0*/);
			if (canVInfect && virus.Count > 0) {
				antiVirus [Random.Range (0, antiVirus.Count - 1)].transform.FindChild ("Warning").GetComponent <Animator> ().SetTrigger ("Warning");
				//ObjectManager.Instance.AntiTransToVirus (antiCanInfect [Random.Range (0, antiCanInfect.Count - 1)]);
				infectTimer = Time.time;
				//CheckGameOver ();
			}
		}
	}

	void CalcuACenter ()
	{
		float centerX = 0;
		float centerY = 0;
		foreach (AntiVirus a in antiVirus) {
			centerX += a.transform.position.x;
			centerY += a.transform.position.y;
		}
		centerX /= antiVirus.Count;
		centerY /= antiVirus.Count;
		centerOfAnti = new Vector2 (centerX, centerY);
	}

	public void CheckGameOver ()
	{
		if (antiVirus.Count < 1 && ProteinManager.Instance.protein < 100) {
			Lose ();
			return;
		}
		if (virus.Count < 1) {
			noVirusInScene = true;
			if (ObjectManager.Instance.spawnVEnd) {
				Win ();
			}
		}
	}

	void Win ()
	{
		if (GameController.Instance.currentLevel == GameController.Instance.unlockLevel) {
			if (GameController.Instance.unlockRound < 4) {
				if (GameController.Instance.currentRound == GameController.Instance.unlockRound)
					PlayerPrefs.SetInt ("UnLockRound", ++GameController.Instance.unlockRound);
			} else {
				if (GameController.Instance.currentRound == GameController.Instance.unlockRound) {
					PlayerPrefs.SetInt ("UnLockLevel", ++GameController.Instance.unlockLevel);
					GameController.Instance.unlockRound = 0;
					PlayerPrefs.SetInt ("UnLockRound", 0);
				}
			}
		}
		winCanvas.GetComponentInChildren<GoldShow> (true).preGold = PlayerPrefs.GetInt ("Gold");
		PlayerPrefs.SetInt ("Gold", PlayerPrefs.GetInt ("Gold") + 20 + ProteinManager.Instance.protein / 10);
		winCanvas.gameObject.SetActive (true);
		if (GameController.Instance.currentLevel == 3 && GameController.Instance.currentRound == 4) {
			winCanvas.transform.FindChild ("Next").GetComponent<Button> ().interactable = false;
		}
	}

	void Lose ()
	{
		loseCanvas.GetComponentInChildren<GoldShow> (true).preGold = PlayerPrefs.GetInt ("Gold");
		PlayerPrefs.SetInt ("Gold", PlayerPrefs.GetInt ("Gold") + 20 + ProteinManager.Instance.protein / 10);
		loseCanvas.gameObject.SetActive (true);
	}

	public void RetuenRoundSelect ()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene ("Level" + (GameController.Instance.currentLevel + 1), LoadSceneMode.Single);
	}

	public void NextLevel ()
	{
		if (GameController.Instance.currentRound < 4) {
			GameController.Instance.currentRound++;
		} else {
			GameController.Instance.currentLevel++;
			GameController.Instance.currentRound = 0;
		}
		ReStart ();
	}
}
