using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Shuttle");

        if (ball == null) return;

        transform.LookAt(ball.transform.position);
    }
}
