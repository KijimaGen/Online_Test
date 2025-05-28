using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EbiSpin : MonoBehaviour{
    void Update(){
        transform.Rotate(new Vector3(0, 1000, 0) * Time.deltaTime);
    }
}
