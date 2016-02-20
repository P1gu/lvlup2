using UnityEngine;
using System.Collections;

public class GuerrierIA : MonoBehaviour {

    private PlayableCharacter pc;

    private float timer;

    // Use this for initialization
    void Start()
    {
        pc = this.GetComponent<PlayableCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
        GameObject sbire = GameObject.FindGameObjectWithTag("Sbire");
        if (sbire != null)
        {
            Vector3 v3 = sbire.transform.position;
            if (v3.x - 2.5f > transform.position.x)
            {
                pc.Move(0.6f);
            }
            else if (v3.x + 2.5f < transform.position.x)
            {
                pc.Move(-0.6f);
            }
            int proba=Random.Range(0, 100);
            if (proba < 15) {
               // pc.Jump();
            }
            if (timer + 5 > Time.time)
            {
                timer = Time.time;
                pc.Action1();
            }

        }
    }
}
