using UnityEngine;
using System.Collections;

public class Level2 : Level
{
	ParticleSystem[] particles;

	protected override void Awake ()
	{
		base.Awake ();
		particles = GetComponentsInChildren <ParticleSystem> (true);
	}

	protected override void Start ()
	{
		base.Start ();
		for (int i = 0; i < particles.Length; i++) {
			particles [i].Simulate (8f);
		}
	}

	public override void Selected ()
	{
		base.Selected ();
		for (int i = 0; i < particles.Length; i++) {
			particles [i].Simulate (8f);
			particles [i].Play ();
		}
	}

	public override void UnSelected ()
	{
		base.UnSelected ();
		for (int i = 0; i < particles.Length; i++) {
			particles [i].Pause ();
		}
	}
}
