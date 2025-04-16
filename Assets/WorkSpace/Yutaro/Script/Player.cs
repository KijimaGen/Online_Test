using TMPro;
using UnityEngine;
public class Player : MonoBehaviour
{
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;

    

    void Start()
    {
        
    }

    private void Update()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        //float moveZ = Input.GetAxisRaw("Vertical");

        //transform.position += new Vector3(moveX, 0, moveZ).normalized * 0.01f;

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<ReplayRecorder>().StartReplay();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "RedSetTeam")
        {
            gameObject.tag = "RedTeam";
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (collision.gameObject.name == "WhiteSetTeam")
        {
            gameObject.tag = "WhiteTeam";
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }


}