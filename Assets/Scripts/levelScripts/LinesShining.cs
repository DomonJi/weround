using UnityEngine;
using System.Collections;

public class LinesShining : MonoBehaviour
{

	public Transform[] lines;
	float[] velocities;
	// Use this for initialization
	void Awake ()
	{
		lines = GetComponentsInChildren <Transform> ();
	}

	void Start ()
	{
		velocities = new float[lines.Length];
		for (int i = 1; i < lines.Length; i++) {
			velocities [i] = Random.Range (5f, 15f);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 1; i < lines.Length; i++) {
			lines [i].position += new Vector3 (velocities [i] * Time.deltaTime, 0, 0);
			if (lines [i].position.x > 10) {
				velocities [i] = Random.Range (5f, 12f);
				lines [i].position = new Vector3 (-10, lines [i].position.y, 0);
			}
		}
	}
}
