using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{
	protected void Start ()
	{
		GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
		foreach (GameObject item in items) {
			Physics.IgnoreCollision(item.GetComponent<CapsuleCollider> (), this.GetComponent<CapsuleCollider> ());
		}
	}

	protected void FixedUpdate ()
	{

	}

	protected void Update ()
	{

	}
}

