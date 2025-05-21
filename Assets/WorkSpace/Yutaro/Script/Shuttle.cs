using UnityEngine;
using DG.Tweening;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    int touchTeam;
    int lastTouch;
    public bool initialize;

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

        
        Quaternion toRotation = Quaternion.LookRotation(rb.velocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 10f);
    }


    public void Initialize()
    {
        if (initialize) return; initialize = true;
        rb = GetComponent<Rigidbody>();
        transform.GetComponent<Collider>().enabled = true;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!transform.GetChild(0).GetComponent<MeshRenderer>().enabled) return;
        if (GameManager.instance.state==GameManager.gameState.standBy) return;
        if (collision.gameObject.name == "赤床")
        {
            transform.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            Invoke("SetGameReplay", 3);
            rb.isKinematic = true;
            TriggerShockwave();

            if (ReplayRecorder.instance.isReplaying) return;
            ScoreManager.instance.whiteScore++;

            for (int i = 0; i < GameManager.instance.playerList.Count; i++)
            {
                if(lastTouch == i + 1) { GameManager.instance.playerList[i].goal++; }
            }
            //効果音(爆発(得点))
            SoundManager.Instance.PlaySound(1);
        }

        if (collision.gameObject.name == "白床")
        {
            transform.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            Invoke("SetGameReplay", 2);
            rb.isKinematic = true;
            TriggerShockwave();

            if (ReplayRecorder.instance.isReplaying) return;
            ScoreManager.instance.redScore++;

            for (int i = 0; i < GameManager.instance.playerList.Count; i++)
            {
                if (lastTouch == i + 1) { GameManager.instance.playerList[i].goal++; }
                //Debug.Log("ゴール"+ GameManager.instance.playerList[i].goal);
            }
            //効果音(爆発(得点))
            SoundManager.Instance.PlaySound(1);
        }

        if (collision.gameObject.tag == "Out")
        {
            transform.GetComponent<Collider>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            Invoke("SetGameReplay", 2);
            rb.isKinematic = true;

            if (ReplayRecorder.instance.isReplaying) return;
            if(touchTeam == 0) { ScoreManager.instance.redScore++; }
            if(touchTeam == 1) { ScoreManager.instance.whiteScore++; }
            //ScoreManager.instance.redScore++;
            //効果音(爆発(失点))
            SoundManager.Instance.PlaySound(2);
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
                GetComponent<shatleEffect>().ShotEffect();


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

                lastTouch = other.transform.parent.GetComponent<Player>().playerName;

                if(transform.position.y <= 0.5f)
                {
                    other.transform.parent.GetComponent<Player>().save++;
                }
                if (GameManager.instance.state != GameManager.gameState.repaly) {
                    //効果音(カキーン)
                    if (player.Smash)
                        SoundManager.Instance.PlaySound(8);
                    else
                        SoundManager.Instance.PlaySound(3);
                }
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
                if (ReplayRecorder.instance.isReplaying) return;
                // 衝撃波を外向きに与える
                rb.AddExplosionForce(10000, ExplosionPos, 20);
                Debug.Log($"[衝撃波] {hit.name} に力を適用しました");
            }
        }

    }

    private void SetGameReplay()
    {
        GameManager.instance.state = GameManager.gameState.repaly;
    }
}
