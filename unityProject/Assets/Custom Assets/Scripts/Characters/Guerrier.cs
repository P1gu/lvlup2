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

	void Start()
	{
		fireTime = 0.0f;
		shieldTime = 0.0f;
	}

	void Update()
	{
		
	}

	public void Action1()
	{
		if (Time.time - fireTime > fireRate) {
			sword.GetComponent<Sword> ().Swing ();
			fireTime = Time.time;
		}
	}

	public void Action2()
	{
		if (Time.time - shieldTime < shieldLag) {
			//shield.GetComponent<Shie>
			shieldTime = Time.time;
		}
	}


}