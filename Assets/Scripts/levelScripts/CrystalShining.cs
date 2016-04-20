using UnityEngine;
using System.Linq;
using System.Collections;

public class CrystalShining : MonoBehaviour
{

	Animator[] animators;

	void Awake ()
	{
		animators = GetComponentsInChildren <Animator> ();
	}
	// Use this for initialization
	void Start ()
	{
		animators.ToList ().ForEach (a => a.enabled = false);
		StartCoroutine (DelayAnim ());
	}

	IEnumerator DelayAnim ()
	{
		for (int i = 0; i < animators.Length; i++) {
			yield return new WaitForSeconds (Random.value);
			animators [i].enabled = true;
		}
	}
}
