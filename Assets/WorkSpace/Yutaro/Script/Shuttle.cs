using UnityEngine;
using DG.Tweening;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    RaycastHit[] hits;

    void FixedUpdate()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Racket");

        for (int i = 0; i < target.Length; i++)
        {
            if (target[i] != null)
            {
                Shoot(target[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<ReplayRecorder>().StartReplay();
        }

    }



    private void Shoot(GameObject _Target)
    {
        //Vector3 direction = (_Target.transform.position - transform.position).normalized;
        //float radius = 0.1f;
        //float maxDistance = 1f;

        //// SphereCastAll
        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, direction, maxDistance);

        //// Debug用Rayをシーンに描画（赤い線）
        //Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

        //// ヒットしたオブジェクトの名前を表示
        //foreach (RaycastHit hit in hits)
        //{
        //    if (hit.collider.gameObject.tag == "Racket")
        //    {
        //        if (hit.collider.transform.parent.GetComponent<Player>().attack)
        //        {
        //            Player player = hit.collider.transform.parent.GetComponent<Player>();
        //            player.attack = false;
        //            hit.collider.transform.parent.position = new Vector3(transform.localPosition.x - 1.5f, 0, transform.position.z);
        //            rb.AddForce(direction * -20, ForceMode.Impulse);

        //        }
        //    }
        //}

    }

    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "赤床") 
        { 
            ScoreManager.instance.whiteScore++; 
            gameObject.GetComponent<Collider>().enabled = false; 
            gameObject.GetComponent<MeshRenderer>().enabled = false; 
        }

        if (collision.gameObject.name == "白床")
        {
            ScoreManager.instance.redScore++;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Racket")
        {
            if (other.transform.parent.GetComponent<Player>().attack)
            {
                Player player = other.transform.parent.GetComponent<Player>();
                player.attack = false;
               // other.transform.parent.position = new Vector3(transform.localPosition.x - 1f, 0, transform.position.z);
                other.transform.DOMoveX(transform.position.x - 1f , 0.5f,true);

                Transform point = player.hitPoint.transform;

                if(player.chargeSlider.fillAmount <= 0.1)
                {
                    player.chargeSlider.fillAmount = 1;
                }

                var dirX = Vector3.zero.x - player.transform.position.x;

                rb.AddForce((point.position - transform.position).normalized * player.chargeSlider.fillAmount* 10 * dirX, ForceMode.Impulse);
                Debug.Log("ネットの距離" + dirX);
                Debug.Log( "power" + player.chargeSlider.fillAmount);
            }
        }
    }

}
