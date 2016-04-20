using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

	public Transform halo;
	public int levelIndex;
	public bool isSelected = true;

	SpriteRenderer[] sprites;

	protected virtual void Awake ()
	{
		sprites = GetComponentsInChildren <SpriteRenderer> ();
	}

	protected virtual void Start ()
	{
		LevelSelect.Instance.levelSelectedEvent += OnLevelSelected;
	}

	void OnMouseUp ()
	{
		if (isSelected && Mathf.Abs (LevelSelect.Instance.levelPositions [levelIndex] - LevelSelect.Instance.transform.position.x) < 0.03) {
			GameController.Instance.currentLevel = levelIndex;
			SceneManager.LoadScene ("Level" + (levelIndex + 1));
		}
	}

	void OnLevelSelected (int i)
	{
		if (i == levelIndex) {
			isSelected = true;
			Selected ();
		} else if (isSelected) {
			UnSelected ();
			isSelected = false;
		}
	}

	public virtual void Selected ()
	{
		halo.transform.localScale = Vector3.Lerp (halo.transform.localScale, Vector3.one, Time.deltaTime);
		for (int i = 0; i < sprites.Length; i++) {
			sprites [i].color = new Color (1, 1, 1, 1);
		}
	}

	public virtual void UnSelected ()
	{
		halo.transform.localScale = Vector3.Lerp (halo.transform.localScale, Vector3.zero, Time.deltaTime);
		for (int i = 0; i < sprites.Length; i++) {
			sprites [i].color = new Color (0.7f, 0.7f, 0.7f, 0.9f);
		}
	}
}
