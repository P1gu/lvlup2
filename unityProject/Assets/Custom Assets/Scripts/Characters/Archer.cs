using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour, IActions
{
	public GameObject flecheObject;
	public Transform flecheSpawner;
	public float fireRate;
	public float shootAngle;
	public float shootForce;

	private float fireTime;
	private CharacterBehaviour cb;
	private Animator animator;

	void Start()
    {
		cb = GetComponent<CharacterBehaviour> ();
		animator = GetComponent<Animator> ();
		fireTime = 0.0f;
    }

	void Update()
    {
		
    }

	void TirerFleche()
	{
		GameObject fleche = Instantiate (flecheObject, flecheSpawner.position, flecheSpawner.rotation) as GameObject;

		Vector3 direction = Vector3.zero;
		direction.x = Mathf.Cos (Mathf.Deg2Rad * shootAngle) * cb.direction;
		direction.y = Mathf.Sin (Mathf.Deg2Rad * shootAngle);

		fleche.GetComponent<Fleche> ().Direction = direction;
		fleche.GetComponent<Fleche> ().Force = shootForce;
		fleche.GetComponent<Fleche> ().Owner = this;
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
		// TODO : Action 2 de l'archer.
	}
}