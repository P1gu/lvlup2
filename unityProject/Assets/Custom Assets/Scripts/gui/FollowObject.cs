using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public Transform objectToFollow;
    void Update()
    {
     //   Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.position);
       // this.transform.position = screenPoint;
    }
}
