using UnityEngine;
using System.Collections;

public class MageIA : MonoBehaviour {

    private PlayableCharacter pc;

    // Use this for initialization
    void Start () {
        pc = this.GetComponent<PlayableCharacter>();
	}

    // Update is called once per frame
    void Update()
    {
        // pc.Jump();
        pc.Action1();
    }
}
