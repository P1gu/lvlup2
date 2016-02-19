using UnityEngine;
using System.Collections;

public class Mage : MonoBehaviour, IActions
{
	public float fireRate;
	public GameObject blast;
	private float fireTime;
	public bool fireBallSpell;
	public Transform fireBallSpawn;
	private Animator animator;
	private CharacterBehaviour ch;

	void Start()
	{
		fireTime = 0.0f;
		animator = GetComponent<Animator> ();
		ch = GetComponent<CharacterBehaviour> ();
	}

	void Update()
	{
		
	}

	public void Blaster() {
		if (!fireBallSpell) {
			GameObject obj = Instantiate (blast, fireBallSpawn.position, fireBallSpawn.rotation) as GameObject;
			Blast bl = obj.GetComponent<Blast> ();
			bl.Direction = ch.direction;
			bl.Owner = this;
		}
		else {
			GameObject fireBall = Instantiate (blast, fireBallSpawn.position, fireBallSpawn.rotation) as GameObject;
			FireBall fb = fireBall.GetComponent<FireBall> ();
			fb.Direction = ch.direction;
			fb.Owner = this;
		}
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