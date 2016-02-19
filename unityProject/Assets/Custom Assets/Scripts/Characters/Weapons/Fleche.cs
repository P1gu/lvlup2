using UnityEngine;
using System.Collections;

public class Fleche : Item
{
	public int dammage;


	private Rigidbody rb;
	private CapsuleCollider cc;
	private bool hit;

	void Start()
	{
		base.Start ();

		Physics.IgnoreCollision(Owner.GetComponent<CapsuleCollider> (), this.GetComponent<CapsuleCollider> ());

		rb = GetComponent<Rigidbody> ();
		cc = GetComponent<CapsuleCollider> ();
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
		cc.enabled = false;
		if (other.tag == "Dammagable") {
			other.GetComponent<CharacterBehaviour> ().health -= dammage;
			Debug.Log ("Hit");
		}
	}

	public float Force { get; set;}
	public Vector3 Direction { get; set;}
	public Archer Owner { get; set;}
}