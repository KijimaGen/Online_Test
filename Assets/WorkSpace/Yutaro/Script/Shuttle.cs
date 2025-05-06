using UnityEngine;
using DG.Tweening;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    int touchTeam;

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
        if (collision.gameObject.name == "赤床")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.state = GameManager.gameState.repaly;
            rb.isKinematic = true;
            TriggerShockwave();

            if (ReplayRecorder.instance.isReplaying) return;
            ScoreManager.instance.whiteScore++;
        }

        if (collision.gameObject.name == "白床")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.state = GameManager.gameState.repaly;
            rb.isKinematic = true;
            TriggerShockwave();

            if (ReplayRecorder.instance.isReplaying) return;
            ScoreManager.instance.redScore++;
        }

        if (collision.gameObject.tag == "Out")
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            GameManager.instance.state = GameManager.gameState.repaly;
            rb.isKinematic = true;

            if (ReplayRecorder.instance.isReplaying) return;
            if(touchTeam == 0) { ScoreManager.instance.whiteScore++; }
            if(touchTeam == 1) { ScoreManager.instance.redScore++; }
            //ScoreManager.instance.redScore++;
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
                //other.transform.parent.position = new Vector3(transform.localPosition.x, 0, transform.position.z);

                Transform point = player.hitPoint.transform;

                if(player.chargeSlider.fillAmount <= 0.2)
                {
                    player.chargeSlider.fillAmount = 0.3f;
                }

                var dirX = Vector3.zero.x - player.transform.position.x;
                if(dirX < 10f) { dirX = 10; }
                rb.AddForce((point.position - transform.position).normalized * player.chargeSlider.fillAmount* 12 * dirX, ForceMode.Impulse);

                if(other.transform.parent.tag == "WhiteTeam") { touchTeam = 0; }
                if(other.transform.parent.tag == "RedTeam") { touchTeam = 1; }
            }
        }
    }

    public void TriggerShockwave()
    {
        Vector3 ExplosionPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        // 一定範囲内のオブジェクトを取得
        Collider[] colliders = Physics.OverlapSphere(ExplosionPos, 20);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 衝撃波を外向きに与える
                rb.AddExplosionForce(10000, ExplosionPos, 20);
                //Debug.Log($"[衝撃波] {hit.name} に力を適用しました");
            }
        }

    }
}
