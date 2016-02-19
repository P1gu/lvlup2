using UnityEngine;
using System.Collections;

public class Fleche : Item
{
	private Rigidbody rb;
	private bool hit;

	void Start()
	{
		base.Start ();

		rb = GetComponent<Rigidbody> ();
		rb.velocity = Direction * Force;

		hit = false;
	}

	void FixedUpdate ()
	{
		base.FixedUpdate ();

		if (!hit) {
			transform.LookAt (transform.position + rb.velocity.normalized);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		hit = true;
		rb.isKinematic = true;
		Destroy (this.gameObject, 5.0f);
		transform.SetParent (other.transform);

	}

	public float Force { get; set;}
	public Vector3 Direction { get; set;}
}