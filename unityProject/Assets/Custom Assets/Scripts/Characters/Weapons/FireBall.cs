using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public int dammage;
	public GameObject explosion;
	public float speed;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Vector3 velo = Vector3.zero;
		velo.x = speed * Direction;
		rb.velocity = velo;

		Vector3 scale = transform.localScale;
		scale.x = Direction;
		transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (Owner.tag == "Sbire" && other.tag == "Aventurier" || Owner.tag == "Aventurier" && other.tag == "Sbire") {
			other.GetComponent<CharacterBehaviour> ().health -= dammage;
			Destroy (this.gameObject);
		} else {
			Quaternion rot = transform.rotation;
			GameObject obj = Instantiate(explosion, transform.position, rot) as GameObject;
			Destroy (obj, 0.5f);
			Destroy (this.gameObject);
		}
	}

	public float Direction { get; set;}
	public Mage Owner { get; set;}
}
