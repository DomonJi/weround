using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[SerializeField]
public enum RoundState
{
	Completed = 0,
	Current = 1,
	Locked = 2
}

public class RoundButton : MonoBehaviour
{
	public int roundIndex;
	public RoundState state;
	SpriteRenderer sprite;
	Color spriteColor;
	Sprite basicSprite;

	void Awake ()
	{
		sprite = GetComponent <SpriteRenderer> ();
		spriteColor = sprite.color;
	}
	// Use this for initialization
	void Start ()
	{
		if (GameController.Instance.currentLevel < GameController.Instance.unlockLevel) {
			state = RoundState.Completed;
		} else if (GameController.Instance.currentLevel == GameController.Instance.unlockLevel) {
			if (roundIndex < GameController.Instance.unlockRound)
				state = RoundState.Completed;
			else if (roundIndex == GameController.Instance.unlockRound)
				state = RoundState.Current;
			else
				state = RoundState.Locked;
		}
		switch (state) {
		case RoundState.Locked:
			sprite.sprite = RoundSelect.Instance.lockedButn;
			break;
		case RoundState.Current:
			sprite.sprite = RoundSelect.Instance.curntButn;
			break;
		case RoundState.Completed:
			sprite.sprite = RoundSelect.Instance.completedButn;
			break;
		}
		basicSprite = sprite.sprite;
	}

	void OnMouseDown ()
	{
		if (state == RoundState.Completed) {
			sprite.sprite = RoundSelect.Instance.pressedCoompleted;
		} else if (state == RoundState.Current) {
			sprite.sprite = RoundSelect.Instance.pressedCurnt;
		}
	}

	void OnMouseExit ()
	{
		sprite.sprite = basicSprite;
	}

	void OnMouseUpAsButton ()
	{
		GameController.Instance.currentRound = roundIndex;
		if (state != RoundState.Locked) {
			SceneManager.LoadScene (RoundSelect.Instance.mainScene);
		}
	}
}
