using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour
{

	void OnTriggerEnter2D (Collider2D other)
	{
		Virus v = other.GetComponent <Virus> ();
		if (v != null) {
			v.Die ();
		}
	}
}
