﻿using UnityEngine;
using UnityEngine.Advertisements;

[SerializeField]
public enum AdsType
{
	RANDOM,
	DEFAULT
}

public class ShowAds : MonoBehaviour
{
	public AdsType adstype;

	public void ShowAd ()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}

	//	void Start ()
	//	{
	//		showed = false;
	//	}
	//
	//	void FixedUpdate ()
	//	{
	//		if (!showed) {
	//			ShowAd ();
	//			showed = true;
	//		}
	//	}

	public void Start ()
	{
		if (adstype == AdsType.RANDOM) {
			if (Random.value > 0.6) {
				ShowAd ();
			}
		}
		if (adstype == AdsType.DEFAULT) {
			ShowAd ();
		}
	}
}