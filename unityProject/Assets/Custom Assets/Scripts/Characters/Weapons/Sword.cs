using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
	public int dammage;
	public AnimationCurve curve;

	private SpriteRenderer sr;

	private bool swinging;
	private float swinging_start;

	public void Start() {
		swinging = false;
		sr = GetComponent<SpriteRenderer> ();
	}

	public void Update() {
		if (swinging) {

		}
	}

	public void Swing() {
		swinging = true;
		swinging_start = Time.time;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Character") {
			other.GetComponent<CharacterBehaviour> ().health -= dammage;
		}
	}
}

