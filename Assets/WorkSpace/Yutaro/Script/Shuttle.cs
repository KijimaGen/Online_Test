using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    int touchTeam;
    int lastTouch;
    public bool initialize;
    [SerializeField] Image fallPoint;

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

        fallPoint.transform.position = new Vector3(transform.position.x,0.4f,transform.position.z);
        fallPoint.transform.rotation = Quaternion.Euler(90, 0, 0);

        float dir = transform.position.y - fallPoint.transform.position.y;
        fallPoint.transform.localScale = new Vector3(dir / 4 + 1, dir / 4 + 1, dir / 4 + 1);
        if (dir > 4)
        {
            fallPoint.color = Color.white;
        }
        if (dir <= 4)
        {
            fallPoint.color = Color.yellow;
        }
        if (dir <= 2)
        {
            fallPoint.color = Color.red;
        }
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
        // チュートリアルじゃなかったら実行
        if (!TutorialRule.tutorial) {
            if (!transform.GetChild(0).GetComponent<MeshRenderer>().enabled) return;
            if (GameManager.instance.state == GameManager.gameState.standBy) return;
            if (collision.gameObject.name == "赤床") {
                transform.GetComponent<Collider>().enabled = false;
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                Invoke("SetGameReplay", 3);
                rb.isKinematic = true;
                TriggerShockwave();

                if (ReplayRecorder.instance.isReplaying) return;
                ScoreManager.instance.whiteScore++;
                if (touchTeam == 1)
                    GameManager.instance.serveTeam = 0;

                if (touchTeam == 0)
                    GameManager.instance.serveTeam = 0;
                for (int i = 0; i < GameManager.instance.playerList.Count; i++) {
                    if (lastTouch == i + 1) { GameManager.instance.playerList[i].goal++; }
                }
                //効果音(爆発(得点))
                SoundManager.Instance.PlaySound(1);
            }

            if (collision.gameObject.name == "白床") {
                transform.GetComponent<Collider>().enabled = false;
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                Invoke("SetGameReplay", 2);
                rb.isKinematic = true;
                TriggerShockwave();

                if (ReplayRecorder.instance.isReplaying) return;
                ScoreManager.instance.redScore++;
                if (touchTeam == 0)
                    GameManager.instance.serveTeam = 1;

                if (touchTeam == 1)
                    GameManager.instance.serveTeam = 1;
                for (int i = 0; i < GameManager.instance.playerList.Count; i++) {
                    if (lastTouch == i + 1) { GameManager.instance.playerList[i].goal++; }
                    //Debug.Log("ゴール"+ GameManager.instance.playerList[i].goal);
                }
                //効果音(爆発(得点))
                SoundManager.Instance.PlaySound(1);
            }

            if (collision.gameObject.tag == "Out") {
                transform.GetComponent<Collider>().enabled = false;
                transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                Invoke("SetGameReplay", 2);
                rb.isKinematic = true;

                if (ReplayRecorder.instance.isReplaying) return;
                if (touchTeam == 0) { ScoreManager.instance.redScore++; GameManager.instance.serveTeam = 1; }
                if (touchTeam == 1) { ScoreManager.instance.whiteScore++; GameManager.instance.serveTeam = 0; }
                //ScoreManager.instance.redScore++;
                //効果音(爆発(失点))
                SoundManager.Instance.PlaySound(2);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Racket")
        {
            if (other.transform.parent.GetComponent<Player>().attack)
            {
                rb.velocity = Vector3.zero;
                GetComponent<shatleEffect>().ShotEffect();
                rb.isKinematic = false;
                Player player = other.transform.parent.GetComponent<Player>();
                player.attack = false;
                //other.transform.parent.position = new Vector3(transform.localPosition.x, 0, transform.position.z);
                player.score += 10;
                Transform point = player.hitPoint.transform;

                if(player.chargeSlider.fillAmount <= 0.1)
                {
                    player.chargeSlider.fillAmount = 0.2f;
                }
                player.currentRate += 0.05f;

                var dirX = Vector3.zero.x - player.transform.position.x;
                if(dirX < 10f) { dirX = 10; }
                if (player.useSkill)
                {
                    rb.AddForce((point.position - transform.position).normalized * 6 * dirX, ForceMode.Impulse);
                }
               
                else
                {
                    rb.AddForce((point.position - transform.position).normalized * player.chargeSlider.fillAmount * 16 * dirX, ForceMode.Impulse);
                }
                if(other.transform.parent.tag == "WhiteTeam") { touchTeam = 0; }
                if(other.transform.parent.tag == "RedTeam") { touchTeam = 1; }

                lastTouch = other.transform.parent.GetComponent<Player>().playerName;

                if(transform.position.y <= 0.5f)
                {
                    other.transform.parent.GetComponent<Player>().save++;
                }
                SoundManager.Instance.PlaySound(3);

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
