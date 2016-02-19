using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
	public int dammage;
	public AnimationCurve curve;

	private SpriteRenderer sr;

	private bool swinging;

	public void Start() {
		swinging = false;
		sr = GetComponent<SpriteRenderer> ();
	}

	public void Update() {
		if()
	}

	public void Swing() {
		swinging = true;

	}/*

	OnTriggerEnter(Collider other) {
		if(other.tag == "Character")

	}*/
}

