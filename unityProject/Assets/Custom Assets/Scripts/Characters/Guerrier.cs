using UnityEngine;
using System.Collections;

public class Guerrier : MonoBehaviour, IActions
{
	public GameObject sword;
	public GameObject shield;
	public float fireRate;
	public float shieldLag;
	private float fireTime;
	private float shieldTime;
	private Animator animator;

	void Start()
	{
		fireTime = 0.0f;
		shieldTime = 0.0f;

		animator = GetComponent<Animator> ();
		shield.SetActive(false);
		sword.SetActive(false);
	}

	void Update()
	{
		if (Time.time > shieldTime + shieldLag) {
			shield.SetActive(false);
			animator.SetBool ("Shield", false);
		}
	}

	public void Action1()
	{
		if (Time.time - fireTime > fireRate) {
			animator.SetTrigger ("Shoot");
			sword.SetActive(true);
			fireTime = Time.time;
		}
	}

	public void StopSwinging() {
		sword.SetActive(false);
	}

	public void Action2()
	{
		Debug.Log ("Shield");
		shield.SetActive(true);
		shieldTime = Time.time;
		animator.SetBool ("Shield", true);
	}


}