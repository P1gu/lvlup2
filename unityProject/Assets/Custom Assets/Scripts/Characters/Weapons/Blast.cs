using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour
{
	public int dammage;
	public float blastTime;

	void Start() {
		Vector3 scale = transform.localScale;
		scale.x = Direction;
		transform.localScale = scale;
		Destroy (this.gameObject, blastTime);
	}

	void Update() {
		
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (other.name);
		if (this.transform.parent.tag == "Sbire" && other.tag == "Aventurier" || this.transform.parent.tag == "Aventurier" && other.tag == "Sbire") {
			other.GetComponent<CharacterBehaviour> ().health -= dammage;
		}
	}

	public float Direction { get; set;}
	public Mage Owner { get; set;}
}

