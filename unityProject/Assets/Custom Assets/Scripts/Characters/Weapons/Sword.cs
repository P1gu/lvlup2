using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
	public int dammage;

	public void Start() {

	}

	public void Update() {

	}

	void OnTriggerEnter(Collider other)
	{
		if (this.gameObject.transform.parent.tag == "Sbire" && other.tag == "Aventurier" || this.gameObject.transform.parent.tag == "Aventurier" && other.tag == "Sbire") {
			other.GetComponent<CharacterBehaviour> ().health -= dammage;
		}
	}
}

