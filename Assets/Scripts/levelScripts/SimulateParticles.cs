using UnityEngine;
using System.Collections;

public class SimulateParticles : MonoBehaviour
{
	public float simulateTime;
	ParticleSystem[] particles;
	// Use this for initialization
	void Start ()
	{
		particles = GetComponentsInChildren <ParticleSystem> ();
		for (int i = 0; i < particles.Length; i++) {
			particles [i].Simulate (simulateTime);
			particles [i].Play ();
		}
	}
}
