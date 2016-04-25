using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ObjectManager : SceneSingleton<ObjectManager>
{
	[SerializeField] Text waveText;

	const float virusSpawnDelay = 15f;

	const float spawnVForceFactor = 200f;
	const float spawnAForceFactor = 300f;
	const float duplicateAForceFactor = 2000f;

	int constrainA = 16;

	//	int virusMaxWaves = 2;
	int currentWave;

	public bool spawnVEnd;

	[SerializeField]Transform[] spawnPoints;

	public GameObject antiPfb;
	public GameObject vPfb;
	public GameObject camouPfb;
	public GameObject bombPfb;
	public GameObject infectedAPfb;
	public GameObject littlePfb;
	public GameObject nifePfb;
	public GameObject shieldPfb;

	Dictionary<string,ObjectPool> objPools;

	RoundItem roundItem;

	void Awake ()
	{
		objPools = new Dictionary<string, ObjectPool> () {
			{ "AntiVirus",new ObjectPool () },
			{ "Virus",new ObjectPool () },
			{ "CamouflagVirus",new ObjectPool () },
			{ "BombVirus",new ObjectPool () },
			{ "InfectedAnti",new ObjectPool () },
			{ "LittleVirus",new ObjectPool () },
			{ "NifeVirus",new ObjectPool () }
		};
		roundItem = GameController.Instance.roundConfigs.levels [GameController.Instance.currentLevel] [GameController.Instance.currentRound];
	}

	void Start ()
	{
		roundItem = GameController.Instance.roundConfigs.levels [GameController.Instance.currentLevel] [GameController.Instance.currentRound];
		Init ();
	}

	public void ShowWaveText ()
	{
		Debug.Log (roundItem.antiNum);
		waveText.text = currentWave + "/" + roundItem.waves.Length;
	}

	public void StartSpawn ()
	{
		StartCoroutine (SpawnWaves ());
	}

	void Init ()
	{
		objPools ["AntiVirus"].Init (antiPfb, 15, 3);
		objPools ["Virus"].Init (vPfb, 8, 2);
		objPools ["CamouflagVirus"].Init (camouPfb, 2, 1);
		objPools ["BombVirus"].Init (bombPfb, 2, 1);
		objPools ["InfectedAnti"].Init (infectedAPfb, 2, 1);
		objPools ["LittleVirus"].Init (littlePfb, 5, 2);
		objPools ["NifeVirus"].Init (nifePfb, 2, 1);
	}

	void SpawnVirus (int vn, int sn, int cn, int bn, int fn)
	{
		var newSpawnedList = new List<Virus> ();
		SceneController.Instance.noVirusInScene = false;
		var spawnPointsIndex = new List<Transform> (spawnPoints);
		for (int i = 0; i < vn; i++) {
			int p = Random.Range (0, spawnPointsIndex.Count);
			Virus v = objPools ["Virus"].Pop (spawnPointsIndex [p].position).GetComponent <Virus> ();
			v.GetComponent<Rigidbody2D> ().AddForce (-spawnPoints [p].position * spawnVForceFactor);
			newSpawnedList.Add (v);
			spawnPointsIndex.RemoveAt (p);
		}
		for (int i = 0; i < cn; i++) {
			int p = Random.Range (0, spawnPointsIndex.Count);
			Virus v = objPools ["CamouflagVirus"].Pop (spawnPointsIndex [p].position).GetComponent <Virus> ();
			v.GetComponent<Rigidbody2D> ().AddForce (-spawnPoints [p].position * spawnVForceFactor);
			newSpawnedList.Add (v);
			spawnPointsIndex.RemoveAt (p);
		}
		for (int i = 0; i < bn; i++) {
			int p = Random.Range (0, spawnPointsIndex.Count);
			Virus v = objPools ["BombVirus"].Pop (spawnPointsIndex [p].position).GetComponent <Virus> ();
			v.GetComponent<Rigidbody2D> ().AddForce (-spawnPoints [p].position * spawnVForceFactor);
			newSpawnedList.Add (v);
			spawnPointsIndex.RemoveAt (p);
		}
		for (int i = 0; i < fn; i++) {
			int p = Random.Range (0, spawnPointsIndex.Count);
			Virus v = objPools ["NifeVirus"].Pop (spawnPointsIndex [p].position).GetComponent <Virus> ();
			v.GetComponent<Rigidbody2D> ().AddForce (-spawnPoints [p].position * spawnVForceFactor);
			newSpawnedList.Add (v);
			spawnPointsIndex.RemoveAt (p);
		}
//		newSpawnedList.ForEach (v => {
//			if (v.canEquipShield)
//				v.UnEquipShield ();
//			Transform effect = v.transform.Find ("UlrayVEffect(Clone)");
//			if (effect != null)
//				Destroy (effect.gameObject);
//			v.movement.CanMove = true;
//		});
		for (int i = 0; i < sn; i++) {
			int p = Random.Range (0, newSpawnedList.Count);
			newSpawnedList [p].EquipShield ();
			newSpawnedList.RemoveAt (p);
		}
	}

	public void SpawnAnti (int num)
	{
		var spawnPointsIndex = new List<Transform> (spawnPoints);
		for (int i = 0; i < num; i++) {
			int p = Random.Range (0, spawnPointsIndex.Count);
			objPools ["AntiVirus"].Pop (spawnPointsIndex [p].position).GetComponent<Rigidbody2D> ().AddForce (
				(-spawnPoints [p].position + new Vector3 (Random.Range (-5, 5), Random.Range (-5, 5))) * spawnAForceFactor * Random.Range (0.5f, 1.1f)
			);
			spawnPointsIndex.RemoveAt (p);
		}
	}

	public void Duplicate (int num)
	{
		if (SceneController.Instance.antiVirus.Count < constrainA) {
			SpawnAnti (num);
		}
	}

	IEnumerator SpawnWaves ()
	{
		for (int i = 0; i < roundItem.waves.Length; i++) {
			SpawnVirus (roundItem.waves [i] [0], roundItem.waves [i] [1], roundItem.waves [i] [2], roundItem.waves [i] [3], roundItem.waves [i] [4]);
			currentWave++;
			waveText.text = currentWave + "/" + roundItem.waves.Length;
			if (currentWave == roundItem.waves.Length)
				spawnVEnd = true;
			yield return new WaitUntil (() => SceneController.Instance.noVirusInScene || Time.timeSinceLevelLoad > virusSpawnDelay * currentWave);
		}
	}

	public void AntiTransToVirus (AntiVirus a)
	{
		if (SceneController.Instance.antiVirusOnLine.Contains (a)) {
			SceneController.Instance.EndSelection ();
		}
		a.Die ();
		SceneController.Instance.CheckGameOver ();
		objPools ["InfectedAnti"].Pop (a.transform.position);
	}

	public void BombBomb (BombVirus b)
	{
		Vector2 bombForce = new Vector2 (Random.value > 0.5 ? 1 : -1, Random.value > 0.5 ? 1 : -1) * b.bombForceFactor;
		objPools ["LittleVirus"].Pop (b.transform.TransformPoint (2 * Vector3.right)).GetComponent<Rigidbody2D> ().AddForce (bombForce);
		bombForce = new Vector2 (Random.value > 0.5 ? 1 : -1, Random.value > 0.5 ? 1 : -1) * b.bombForceFactor;
		objPools ["LittleVirus"].Pop (b.transform.position).GetComponent<Rigidbody2D> ().AddForce (bombForce);
		bombForce = new Vector2 (Random.value > 0.5 ? 1 : -1, Random.value > 0.5 ? 1 : -1) * b.bombForceFactor;
		objPools ["LittleVirus"].Pop (b.transform.TransformPoint (-2 * Vector3.right)).GetComponent<Rigidbody2D> ().AddForce (bombForce);
	}

	public void Push (string objType, GameObject obj)
	{
		objPools [objType].Push (obj);
		Debug.Log (objType + "pushed");
	}
}
