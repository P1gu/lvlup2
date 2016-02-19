using UnityEngine;
using System.Collections;

public class Archer : MonoBehaviour, IActions
{
	public GameObject flecheObject;
	public Transform flecheSpawner;
	public float fireRate;

	private float fireTime;

	void Start()
    {
		fireTime = 0.0f;
    }

	void Update()
    {
		if(Input.GetKey(KeyCode.Space))
		{
			Vector3 direction = Vector3.right + Vector3.up;

			TirerFleche (direction.normalized);
		}
    }

	void TirerFleche(Vector3 direction)
	{
		if (Time.time - fireTime > fireRate) {
			
			GameObject fleche = Instantiate (flecheObject, flecheSpawner.position, flecheSpawner.rotation) as GameObject;
			fleche.GetComponent<Fleche> ().Direction = direction;
			fleche.GetComponent<Fleche> ().Force = 10.0f;

			fireTime = Time.time;
		}
	}

	public void Action1(Vector3 mousePosition)
	{
		TirerFleche (mousePosition - transform.position);
	}

	public void Action2(Vector3 mousePosition)
	{
		// TODO : Action 2 de l'archer.
	}
}