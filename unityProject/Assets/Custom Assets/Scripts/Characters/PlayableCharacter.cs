using UnityEngine;
using System.Collections;

public class PlayableCharacter : CharacterBehaviour
{
	protected void Update ()
	{
		base.Update ();

		Move (Input.GetAxis ("Horizontal"));
		if (Input.GetKey (KeyCode.Space))
			Jump ();
		if (Input.GetButton ("Fire1"))
			Action1 ();
		if (Input.GetButton ("Fire2"))
			Action2 ();
	}
}
