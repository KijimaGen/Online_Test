using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    [SerializeField] GameObject areaParticle;
    [SerializeField] GameObject expParticle;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float rand = Random.Range(8, 11);
        rb.AddForce((transform.forward + transform.up) * rand, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(areaParticle,transform.position,Quaternion.identity);
        Instantiate(expParticle,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
