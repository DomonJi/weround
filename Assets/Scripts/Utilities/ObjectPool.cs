using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{

	private int m_AllocNum = 0;
	private GameObject m_BaseGameObj;
	/// <summary>
	/// 空闲对象列表
	/// </summary>
	private List<GameObject> m_IdleList = new List<GameObject> ();
	private int m_ReAllocNum = 0;
	/// <summary>
	/// //正在使用中的对象列表
	/// </summary>
	private List<GameObject> m_UsingList = new List<GameObject> ();


	public ObjectPool ()
	{
		this.m_IdleList.Clear ();
		this.m_UsingList.Clear ();
		this.m_BaseGameObj = null;
		this.m_IdleList.Clear ();
		this.m_UsingList.Clear ();
	}



	public void Init (GameObject baseobj, int allocnum, int reallocnum)
	{
		this.m_BaseGameObj = baseobj;
		this.m_AllocNum = allocnum;
		this.m_ReAllocNum = reallocnum;
		this.Alloc (this.m_AllocNum);

	}


	/// <summary>
	/// 分配
	/// </summary>
	/// <param name="allocnum"></param>
	public void Alloc (int allocnum)
	{
		for (int i = 0; i < allocnum; i++) {
			GameObject item = UnityEngine.Object.Instantiate (this.m_BaseGameObj, Vector3.zero, Quaternion.identity) as GameObject;
			item.gameObject.SetActive (false);
			this.m_IdleList.Add (item);
		}

	}

	/// <summary>
	/// 回收不在使用的对象
	/// </summary>
	/// <param name="pushobj"></param>
	/// <returns></returns>
	public bool Push (GameObject pushobj)
	{
		if (this.m_UsingList.Find (x => x == pushobj)) {
			pushobj.gameObject.SetActive (false);

			this.m_IdleList.Add (pushobj);
			this.m_UsingList.Remove (pushobj);

			return true;
		}

		return false;
	}


	/// <summary>
	/// 返回索引为0的元素
	/// </summary>
	/// <returns></returns>
	public GameObject Pop (Vector3 position)
	{
		if (this.m_IdleList.Count == 0) {
			if (this.m_ReAllocNum == 0) {
				if (this.m_UsingList.Count > 0) {
					GameObject obj2 = this.m_UsingList [0];
					this.m_IdleList.Add (obj2);
					this.m_UsingList.Remove (obj2);
				} else {

				}
			} else {
				this.Alloc (this.m_ReAllocNum);

			}
		}

		if (this.m_IdleList.Count <= 0) {
			return null;
		}

		GameObject item = this.m_IdleList [0];
		item.gameObject.SetActive (true);
		this.m_UsingList.Add (item);
		this.m_IdleList.Remove (item);
		item.transform.position = position;
		Debug.Log (item.name + "poped");
		return item;
	}



	public int GetAllocNum ()
	{
		return this.m_AllocNum;
	}

	public int GetReAllocNum ()
	{
		return this.m_ReAllocNum;
	}


	/// <summary>
	/// 清理对象池
	/// </summary>
	public void Clean ()
	{
		for (int i = 0; i < this.m_UsingList.Count; i++) {
			UnityEngine.Object.DestroyImmediate (this.m_UsingList [i]);
		}

		for (int j = 0; j < this.m_IdleList.Count; j++) {
			UnityEngine.Object.DestroyImmediate (this.m_IdleList [j]);
		}
	}
}

