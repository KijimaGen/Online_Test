using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAI : MonoBehaviour{
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;

    [SerializeField] public GameObject hitPoint;
    [SerializeField] public Image chargeSlider;
    [SerializeField] private Text nameText;
    [SerializeField] private GameObject scoreBoard;

    public bool attack;

    public int playerName;

    int index;
    Rigidbody rb;
    Animator animator;
    bool animPlay;

    bool Smash;
    bool right;
    bool left;
    bool jump;

    public int score;
    public int goal;
    public int save;
    public int punch;
    public int counter;

    public bool replayCancel;

    [SerializeField] GameObject ball;
    bool tatchBall = false;

    void Start() {
        animator = GetComponent<Animator>();


        index = GameManager.instance.playerIndex;
        rb = GetComponent<Rigidbody>();

        
        playerName = GameManager.instance.playerList.Count;
        //ボールの位置を把握しておきたい
        ball = GameObject.Find("Shuttle");
    }

    private void FixedUpdate() {
        if (ReplayRecorder.instance.isReplaying) {
            if (Input.GetKey("joystick " + index + " button 0")) {
                replayCancel = true;
            }
        }
        if (GetComponent<ReplayRecorder>().isReplaying) return;
        if (GameManager.instance.roundStart) { rb.isKinematic = true; }
        if (!GameManager.instance.roundStart) { rb.isKinematic = false; }

        ScoreBoard();
        nameText.text = playerName.ToString() + "P";
        
        if (Input.GetKey(KeyCode.R)) {
            GetComponent<ReplayRecorder>().StartReplay();
            
        }
        //デバッグ用の表示用の奴
        Debug.Log(Vector3.Distance(transform.position, ball.transform.position));

        //
        OnMove();
        WhichDir();
        if (Input.GetKey("joystick " + index + " button 1") && !animPlay && transform.tag != "Player" ||
            Input.GetKey(KeyCode.Space) && !animPlay && transform.tag != "Player" || 
            (Vector3.Distance(transform.position, ball.transform.position) > 3 && Vector3.Distance(transform.position, ball.transform.position) < 5) && !animPlay && transform.tag != "Player") {
            GetComponent<PlayerEffect>().ChargeEffect();
            chargeSlider.fillAmount += 0.005f;
        }
        else if (chargeSlider.fillAmount > 0) {

            animPlay = true;
            animator.SetBool("Walk", false);
            if (Smash) {
                animator.SetBool("Smash", true);
                if (!jump) {
                    jump = true;
                }
            }
            if (right) { animator.SetBool("Right", true); }
            if (left) { animator.SetBool("Left", true); }

            if (!right && !left && !Smash) { animator.SetBool("Front", true); }
        }
        if (!animPlay) {

            if (rb.velocity.magnitude > 0.01f) {
                animator.SetBool("Walk", true);
            }
            else {
                animator.SetBool("Walk", false);
            }
        }
        Vector3 center = GameObject.Find("court").transform.position;
        float dirX = center.x - transform.position.x;
        Vector3 dir = (center - transform.position).normalized;
        float velocityZ = rb.velocity.z;

        BoxCollider boxCollider = transform.Find("尻尾").GetComponent<BoxCollider>();
        if (transform.tag == "WhiteTeam") {
            nameText.transform.localRotation = Quaternion.Euler(0, 180, 0);
            chargeSlider.transform.localRotation = Quaternion.Euler(90, 0, 162);
            scoreBoard.transform.localRotation = Quaternion.Euler(0, 180, 0);
            if (chargeSlider.fillAmount >= 0.4f) {
                Smash = true;
                jump = false;
                chargeSlider.fillAmount = 0.4f;
                hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2);
                boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
            }
            else {
                hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.05f, 8 - chargeSlider.fillAmount * 10 - dirX / 3, transform.position.z + velocityZ / 2);
                boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
            }

        }

        if (transform.tag == "RedTeam") {
            nameText.transform.localRotation = Quaternion.Euler(0, 0, 0);
            chargeSlider.transform.localRotation = Quaternion.Euler(-90, 0, 162);
            scoreBoard.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (chargeSlider.fillAmount >= 0.4f) {
                Smash = true;
                jump = false;
                chargeSlider.fillAmount = 0.4f;
                hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2);
                boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
            }
            else {
                hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 8 - chargeSlider.fillAmount * 10 + dirX / 3, transform.position.z + velocityZ / 2);
                boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
            }

        }


        var distance = (hitPoint.transform.position - center).normalized;

        if (distance.z <= -0.4f) {
            hitPoint.transform.SetParent(null);
            hitPoint.transform.SetParent(null);

            Vector3 pos = hitPoint.transform.position;
            pos.z = center.z - 5f; // 固定したいZの値にする
            hitPoint.transform.position = pos;
        }
        else if (distance.z >= 0.4f) {
            hitPoint.transform.SetParent(null);
            hitPoint.transform.SetParent(null);

            Vector3 pos = hitPoint.transform.position;
            pos.z = center.z + 5f; // 固定したいZの値にする
            hitPoint.transform.position = pos;
        }
        else {
            hitPoint.transform.SetParent(transform.Find("尻尾"));
        }

        AiMove();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "RedSetTeam") {
            gameObject.tag = "RedTeam";
           
        }
        if (collision.gameObject.name == "WhiteSetTeam") {
            gameObject.tag = "WhiteTeam";
            
        }

        if (collision.gameObject.name == "Shuttle") {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * 2;
            tatchBall = true;
        }
    }

    
    
    private void WhichDir() {
        GameObject[] shuttle = GameObject.FindGameObjectsWithTag("Shuttle");
        if (shuttle == null) return;
        for (int i = 0, max = shuttle.Length; i < max; i++) {
            var norm = (shuttle[i].transform.position - transform.position).normalized;
            var distance = Vector3.Distance(shuttle[i].transform.position, transform.position);
            if (distance >= 6) return;
            if (norm.z <= -0.3f) { right = true; }
            if (norm.z >= 0.3f) { left = true; }
        }

    }

    private void OnMove() {
        float Speed = 8;
        if (rb.isKinematic) return;
        // InputManager で設定した名前を動的に作る
        string horizontalAxisL = "Horizontal_P" + index + "_L";
        string verticalAxisL = "Vertical_P" + index + "_L";

        float moveX = Input.GetAxisRaw(horizontalAxisL);
        float moveY = Input.GetAxisRaw(verticalAxisL);

       

        Vector3 moveDir = new Vector3(moveY, 0, -moveX).normalized;


        // プレイヤーを移動
        Vector3 conVelocity = rb.velocity;
        conVelocity.x = moveDir.x * Speed;
        conVelocity.z = moveDir.z * Speed;
        rb.velocity = conVelocity;

        rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
    }

    public bool AnimEnd() {
        animator.SetBool("Front", false);
        animator.SetBool("Right", false);
        animator.SetBool("Left", false);
        animator.SetBool("Smash", false);
        Smash = false;
        right = false;
        left = false;
        chargeSlider.fillAmount = 0;
        return animPlay = false;
    }

    public bool OffAttack() {
        return attack = false;
    }

    public bool OnAttack() {
        return attack = true;
    }

    private void ScoreBoard() {
        scoreBoard.SetActive(true);
        Text scoreText = scoreBoard.transform.Find("スコア").GetComponent<Text>();
        Text goalText = scoreBoard.transform.Find("ゴール").GetComponent<Text>();
        Text saveText = scoreBoard.transform.Find("セーブ").GetComponent<Text>();
        Text punchText = scoreBoard.transform.Find("パンチ").GetComponent<Text>();
        Text counterText = scoreBoard.transform.Find("カウンター").GetComponent<Text>();

        scoreText.text = score.ToString();
        goalText.text = goal.ToString();
        saveText.text = save.ToString();
        punchText.text = punch.ToString();
        counterText.text = counter.ToString();

        
    }

    /// <summary>
    /// Aiが移動する仕組み
    /// </summary>
    private void AiMove() {
        float distance = Vector3.Distance(transform.position, ball.transform.position);
        float speed = 10;
        
        // シャトルの方向へ向かって移動
        if (distance > 3 ) {
            Vector3 direction = (ball.transform.position - transform.position + transform.forward).normalized;
            Vector3 move = new Vector3(direction.x,0,direction.z);

            if(transform.position.x > 0.1f && move.x < 0) {
                move.x = 0;
            }
            if (transform.position.x < -0.1f && move.x > 0) {
                move.x = 0;
            }

            transform.position += move * speed * Time.deltaTime;
        }    

        //transform.position = ball.transform.position;
    }
}
