using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlleTest : MonoBehaviour{

    Rigidbody rb;
    [SerializeField] float rotationSpeed;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.AddForce(new Vector3(0, 10, Random.Range(-10, 10)));
    }
}
