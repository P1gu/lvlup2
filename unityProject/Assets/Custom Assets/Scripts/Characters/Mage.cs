using UnityEngine;
using System.Collections;

public class Mage : MonoBehaviour, IActions
{
	public float fireRate;
	private float fireTime;
	private Animator animator;

	void Start()
	{
		fireTime = 0.0f;

		animator = GetComponent<Animator> ();
	}

	void Update()
	{
		
	}

	public void Action1()
	{
		if (Time.time - fireTime > fireRate) {
			animator.SetTrigger ("Shoot");

			fireTime = Time.time;
		}
	}

	public void Action2()
	{

	}
}