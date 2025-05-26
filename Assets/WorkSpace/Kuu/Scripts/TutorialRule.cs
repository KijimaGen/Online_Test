using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialRule : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        Vector3 pos = myTransform.position;
        if (pos.y <= -20) {
            Debug.Log("落ちてまーーーーーーす");
            if (gameObject.tag == "Shuttle") {
                pos.x = 0;
                pos.y = 3.37f;
                pos.z = 2.19f;
            } else if (gameObject.tag == "RedTeam") {
                pos.x = -3;
                pos.y = 2;
                pos.z = 2.5f;
            } else if (gameObject.tag == "WhiteTeam") {
                pos.x = 3;
                pos.y = 2;
                pos.z = -2.5f;
            } else if (gameObject.tag == "Player") {
                pos.x = 0.1f;
                pos.y = 1;
                pos.z = -10;
            }
            myTransform.position = pos;
        }
    }
}
