using UnityEngine;
using DG.Tweening;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    private void Start()
    {
        Initialize();
    }
    void FixedUpdate()
    {
        if(ReplayRecorder.instance.isReplaying) { Initialize(); }
        if (GameManager.instance.roundStart) { rb.isKinematic = true; }

        GameObject[] target = GameObject.FindGameObjectsWithTag("Racket");

        rb.AddForce(Vector3.down * 10, ForceMode.Acceleration);
        
    }


    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ê‘è∞")
        {
            ScoreManager.instance.whiteScore++;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.state = GameManager.gameState.repaly;
        }

        if (collision.gameObject.name == "îíè∞")
        {
            ScoreManager.instance.redScore++;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.state = GameManager.gameState.repaly;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Racket")
        {
            if (other.transform.parent.GetComponent<Player>().attack)
            {
                rb.isKinematic = false;
                Player player = other.transform.parent.GetComponent<Player>();
                player.attack = false;
                other.transform.parent.position = new Vector3(transform.localPosition.x, 0, transform.position.z);

                Transform point = player.hitPoint.transform;

                if(player.chargeSlider.fillAmount <= 0.2)
                {
                    player.chargeSlider.fillAmount = 0.3f;
                }

                var dirX = Vector3.zero.x - player.transform.position.x;
                if(dirX < 10f) { dirX = 10; }
                rb.AddForce((point.position - transform.position).normalized * player.chargeSlider.fillAmount* 12 * dirX, ForceMode.Impulse);
                Debug.Log("ÉlÉbÉgÇÃãóó£" + dirX);
                Debug.Log( "power" + player.chargeSlider.fillAmount);
            }
        }
    }

}
