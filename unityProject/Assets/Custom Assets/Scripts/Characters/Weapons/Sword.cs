﻿using UnityEngine;
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
		Debug.Log (other.name);
		if (other.tag == "Dammagable") {
			other.GetComponent<CharacterBehaviour> ().health -= dammage;
		}
	}
}

