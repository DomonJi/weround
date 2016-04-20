using UnityEngine;
using UnityEngine.Advertisements;

public class ShowAds : MonoBehaviour
{
	public void ShowAd ()
	{
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}
}