using UnityEngine;
using System.Collections;

public class SceneSingleton<T> : MonoBehaviour where T :Component
{

	static T _Instance;

	public static T Instance {
		get {
			if (_Instance == null) {
				_Instance = FindObjectOfType<T> ();
				if (_Instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_Instance = obj.GetComponent<T> () as T;
				}
			}
			return _Instance;
		}
	}
}