using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Player;
    private Vector3 offset;
    void Awake()
    {
        offset= transform.position-Player.position;
    }
    void LateUpdate()
    {
        transform.position = Player.position+offset;
    }
}
