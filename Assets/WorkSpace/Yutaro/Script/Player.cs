using Unity.Netcode;
using UnityEngine;
public class Player : NetworkBehaviour
{


    void Start()
    {

    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, 0, moveZ).normalized * 0.01f;
    }
}