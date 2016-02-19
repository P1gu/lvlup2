using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public string lvl;

    public void Change()
    {
        Application.LoadLevel(lvl);
    }
}
