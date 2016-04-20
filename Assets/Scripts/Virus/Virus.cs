﻿using UnityEngine;
using System.Collections;

public class Virus : MonoBehaviour
{
	protected Animator animator;
	protected Animator shieldAnimator;
	public Movement movement;
	protected Collider2D collider;
	protected Rigidbody2D rgbody;

	public int level;

	public bool canBeDestroyed;
	public bool canEquipShield;
	public int shieldRecoverTime = 10;

	void Awake ()
	{
		Init ();
		movement = GetComponent<VirusMovement> ();
		animator = GetComponent<Animator> ();
		collider = GetComponent<Collider2D> ();
		rgbody = GetComponent<Rigidbody2D> ();
	}

	protected virtual void OnEnable ()
	{
		SceneController.Instance.virus.Add (this);
		movement.enabled = true;
		collider.enabled = true;
		transform.FindChild ("Expression").gameObject.SetActive (true);
		if (canEquipShield) {
			UnEquipShield ();
		}
		Transform effect = transform.FindChild ("UlrayVEffect(Clone)");
		if (effect != null) {
			Destroy (effect.gameObject);
		}
		movement.CanMove = true;
		rgbody.WakeUp ();
	}

	protected virtual void OnMouseDown ()
	{
		if (SceneController.Instance.canVBoom) {
			Die ();
			ProteinManager.Instance.CalcuProtein (new []{ this });
			SceneController.Instance.CheckGameOver ();
		}
	}

	public void EquipShield ()
	{
		canEquipShield = true;
		canBeDestroyed = false;
		GameObject shield = Instantiate (ObjectManager.Instance.shieldPfb, transform.position, Quaternion.identity)as GameObject;
		shield.transform.SetParent (transform);
		shieldAnimator = shield.GetComponent <Animator> ();
	}

	public void UnEquipShield ()
	{
		canEquipShield = false;
		canBeDestroyed = true;
		var shield = transform.FindChild ("shield(Clone)");
		Destroy (shield.gameObject);
	}

	protected virtual void Init ()
	{
		level = 1;
		canEquipShield = false;
		canBeDestroyed = true;
	}

	public virtual void Die ()
	{
		if (!canEquipShield || canBeDestroyed) {
			if (SceneController.Instance)
				SceneController.Instance.virus.Remove (this);
			movement.enabled = false;
			collider.enabled = false;
			rgbody.Sleep ();
			transform.FindChild ("Expression").gameObject.SetActive (false);
			animator.SetTrigger ("Die");
		} else {
			DisableShield ();
		}
	}

	void OnDisable ()
	{
		if (SceneController.Instance) {
			if (SceneController.Instance.virus.Contains (this))
				SceneController.Instance.virus.Remove (this);
		}
	}

	public virtual void Push ()
	{
		ObjectManager.Instance.Push ("Virus", gameObject);
	}

	protected void EnableShield ()
	{
		if (canEquipShield) {
			canBeDestroyed = false;
			level = 1;
			shieldAnimator.SetBool ("HasShield", true);
		}
	}

	protected void DisableShield ()
	{
		if (!canEquipShield)
			return;
		canBeDestroyed = true;
		level = 0;
		shieldAnimator.SetBool ("HasShield", false);
		StartCoroutine (RecoverShield ());
	}

	public virtual void DisableFeature ()
	{
		if (canEquipShield) {
			DisableShield ();
			StopCoroutine (RecoverShield ());
		}
	}

	public virtual void EnableFeatue ()
	{
		if (canEquipShield) {
			StartCoroutine (RecoverShield ());
		}
	}

	IEnumerator RecoverShield ()
	{
		//set animation
		yield return new WaitForSeconds (shieldRecoverTime);
		EnableShield ();
	}
}
