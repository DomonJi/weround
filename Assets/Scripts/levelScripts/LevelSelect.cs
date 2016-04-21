using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelSelect : SceneSingleton<LevelSelect>
{
	public Transform backGround;
	public int curntSelct = 0;
	public float[] levelPositions;
	public float[] backgroundPositions;
	public float levelSettingPosition;
	bool canMoveNext = true;
	bool canMovePre = true;

	public event Action<int> levelSelectedEvent;

	public bool inSetting = false;

	public bool InSetting {
		get {
			return inSetting;
		}
		set {
			inSetting = value;
		}
	}

	void Start ()
	{
		curntSelct = GameController.Instance.currentLevel;
		transform.position = new Vector3 (levelPositions [curntSelct], transform.position.y, 0);
		levelSelectedEvent (curntSelct);
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
//		if (setting) {
//			if (transform.position.x < levelSettingPosition) {
//				transform.position += new Vector3 (20f * Time.deltaTime, 0, 0);
//			}
//			return;
//		} else {
//			if (transform.position.x > levelPositions [curntSelct]) {
//				transform.position -= new Vector3 (20f * Time.deltaTime, 0, 0);
//			}
//		}
		backGround.position = new Vector2 (Mathf.Lerp (backgroundPositions [3], backgroundPositions [0], (transform.position.x - levelPositions [3]) / (levelPositions [0] - levelPositions [3])), backGround.position.y);
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				if (transform.position.x < levelPositions [0] + 2 - 0.02f * Input.GetTouch (0).deltaPosition.x && transform.position.x > levelPositions [GameController.Instance.unlockLevel] - 2 - 0.02f * Input.GetTouch (0).deltaPosition.x) {
					transform.position += new Vector3 (0.02f * Input.GetTouch (0).deltaPosition.x, 0, 0);
					if (Input.GetTouch (0).deltaPosition.x < -10 && canMoveNext) {
						if (curntSelct < GameController.Instance.unlockLevel) {
							transform.position = Vector2.Lerp (transform.position, new Vector2 (levelPositions [curntSelct + 1], transform.position.y), 14f * Time.deltaTime);
							curntSelct++;
							levelSelectedEvent (curntSelct);
							canMoveNext = false;
							canMovePre = true;
						}
					}
					if (Input.GetTouch (0).deltaPosition.x > 10 && canMovePre) {
						if (curntSelct > 0) {
							transform.position = Vector2.Lerp (transform.position, new Vector2 (levelPositions [curntSelct - 1], transform.position.y), 14f * Time.deltaTime);
							curntSelct--;
							levelSelectedEvent (curntSelct);
							canMoveNext = true;
							canMovePre = false;
						}
					}
				} 
			}
			return;
		}
		if (!inSetting) {
			transform.position = Vector2.Lerp (transform.position, new Vector2 (levelPositions [curntSelct], transform.position.y), 14f * Time.deltaTime);
			canMoveNext = true;
			canMovePre = true;
		}
	}

	public void Settings ()
	{
//			SceneManager.LoadScene ("Settings");
		StartCoroutine (SettingAnim ());
	}

	public void SettingBack ()
	{
		StartCoroutine (BackAnim ());
	}

	IEnumerator SettingAnim ()
	{
		InSetting = true;
		while (transform.position.x < levelSettingPosition) {
			transform.position += new Vector3 ((levelSettingPosition - levelPositions [curntSelct]) * Time.deltaTime, 0, 0);
			yield return null;
		} 
	}

	IEnumerator BackAnim ()
	{
		InSetting = true;
		while (transform.position.x > levelPositions [curntSelct]) {
			transform.position -= new Vector3 ((levelSettingPosition - levelPositions [curntSelct]) * Time.deltaTime, 0, 0);
			yield return null;
		}
		InSetting = false;
	}

	public void Home ()
	{
		SceneManager.LoadScene ("Start");
	}
}
