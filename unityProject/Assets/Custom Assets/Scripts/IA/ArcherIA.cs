using UnityEngine;
using System.Collections;

public class ArcherIA : MonoBehaviour {

    private PlayableCharacter pc;
    // Use this for initialization
    void Start()
    {
        pc = this.GetComponent<PlayableCharacter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject sbire = GameObject.FindGameObjectWithTag("Sbire");
        if (sbire != null)
        {
            Vector3 v3 = sbire.transform.position;
            if (v3.x - 6f > transform.position.x)
            {
                pc.Move(0.4f);
            }
            else if (v3.x + 6f < transform.position.x)
            {
                pc.Move(-0.4f);
            }
            int proba = Random.Range(0, 100);
            if (proba < 15)
            {
                pc.Jump();
            }
            pc.Action1();

        }
    }
}