using UnityEngine;
using System.Collections;

public class PlayableCharacter : CharacterBehaviour
{
	protected void Update ()
	{
		base.Update ();

		Move (Input.GetAxis ("Horizontal"));

		if (Input.GetKeyDown (KeyCode.W)) {
			Jump ();
		}

		if(Input.GetButton("Fire1"))
		{
			Action1 ();
		}
	}
}
