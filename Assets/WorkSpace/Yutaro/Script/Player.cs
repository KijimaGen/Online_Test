using UnityEngine;
public class Player : MonoBehaviour
{
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;
    void Start()
    {
        gameObject.tag = "Player";
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(moveX, 0, moveZ).normalized * 0.01f;
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("リプレイ開始");
            ReplayManager.instance.StartReplay();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "RedSetTeam")
        {
            gameObject.tag = "RedTeam";
        }
    }
}