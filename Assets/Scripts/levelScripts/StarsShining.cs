using UnityEngine;

public class StarsShining : MonoBehaviour
{
	SpriteRenderer[] stars;
	float[] delays;
	// Use this for initialization
	void Awake ()
	{
		stars = GetComponentsInChildren <SpriteRenderer> ();
	}

	void Start ()
	{
		delays = new float[stars.Length];
		for (int i = 0; i < stars.Length; i++) {
			delays [i] = Random.Range (0, 180);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < stars.Length; i++) {
			stars [i].color = new Color (stars [i].color.r, stars [i].color.g, stars [i].color.b, Mathf.Abs (1.5f * Mathf.Sin (Time.time - delays [i])));
		}
	}
}
