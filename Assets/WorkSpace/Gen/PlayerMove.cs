using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Axis 1") > 0f)
            transform.position += new Vector3(0.1f, 0, 0);
        else if (Input.GetAxis("Axis 1") < 0f)
            transform.position += new Vector3(-0.1f, 0, 0);

        if (Input.GetAxis("Axis 2") > 0f)
            transform.position += new Vector3(0, 0, -0.1f);
        else if (Input.GetAxis("Axis 2") < 0f)
            transform.position += new Vector3(0, 0, 0.1f);

    }
}
