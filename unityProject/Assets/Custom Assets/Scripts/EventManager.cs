using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public delegate void UnSbireEstMort();
    public static event UnSbireEstMort OnUnSbireEstMort;

    public delegate void UnAventurierEstMort();
    public static event UnAventurierEstMort OnUnAventurierEstMort;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
