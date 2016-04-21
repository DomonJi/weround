using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAds : MonoBehaviour
{
	public bool showed;

	public void ShowAd ()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}

	void Start ()
	{
		showed = false;
	}

	void FixedUpdate ()
	{
		if (!showed) {
			ShowAd ();
			showed = true;
		}
	}
}