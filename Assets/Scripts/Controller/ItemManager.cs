using UnityEngine;
using System.Collections;

public class ItemManager : SceneSingleton<ItemManager>
{
	public Explode explodeItem;
	public UlRay ulRayItem;
	public Antibiotic antibioticItem;


	public void StartGame ()
	{
		explodeItem.StartGame ();
		ulRayItem.StartGame ();
		antibioticItem.StartGame ();
	}

	public void DisableOthers ()
	{
		explodeItem.Disable ();
		ulRayItem.Disable ();
		antibioticItem.Disable ();
	}

	public void EnableOthers ()
	{
		explodeItem.Enable ();
		ulRayItem.Enable ();
		antibioticItem.Enable ();
	}
		
}
