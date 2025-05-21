using DG.Tweening;
using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;

    [SerializeField] public GameObject hitPoint;
    [SerializeField] public Image chargeSlider;
    [SerializeField] private Text nameText;
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private Image[] stampPrefab;

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
    int scoreTmp;
    public int goal;
    public int save;
    public int punch;
    public int counter;

    public bool replayCancel;

    bool stampPlay;
    float stampTime;

    [SerializeField]Text replaySkipText;
    GameObject textObj;

    bool fall;

    void Start()
    {
        animator = GetComponent<Animator>();

        index = GameManager.instance.playerIndex;
       
        rb = GetComponent<Rigidbody>();

        // chargeSlider.fillAmount = 0f;
        playerName = GameManager.instance.playerList.Count;
    }

    private void FixedUpdate()
    {
        Debug.Log(replayCancel);
        if (ReplayRecorder.instance.isReplaying)
        {
            replaySkipText.text = playerName.ToString() + "P";
            if (textObj == null && !replayCancel)
            {
                textObj = Instantiate(replaySkipText.gameObject, GameObject.Find("ReplayCamera").transform.Find("Canvas/スキップタグ").transform);
            }
            

            if (Input.GetKey("joystick " + index + " button 0"))
            {
                replayCancel = true;
                Destroy(textObj);
            }
        }
        if (GetComponent<ReplayRecorder>().isReplaying) return;
        if (GameManager.instance.roundStart) { rb.isKinematic = true; }
        if (!GameManager.instance.roundStart) { rb.isKinematic = false; }
        if (fall)
        {
            if(transform.tag == "RedTeam") {transform.position = new Vector3(-5, 10, 2.5f); }
            if(transform.tag == "WhiteTeam") { transform.position = new Vector3(5, 10, 2.5f); }
            rb.velocity = Vector3.zero;
            
            fall = false;
          // return;
        }
        //ScoreBoard();
        nameText.text = playerName.ToString() + "P";
        //nameText.rectTransform.LookAt(Camera.main.transform);
        //nameText.rectTransform.Rotate(0, 180f, 0);

        OnMove();
        WhichDir();
        if (Input.GetKey("joystick " + index + " button 0"))
        {
            if (gameObject.transform.tag == "RedTeam")
                transform.DOLocalRotate(Vector3.zero, 0.5f);
            else
                transform.DOLocalRotate(Vector3.zero + new Vector3(0, 180, 0), 0.5f);
        }
        if (Input.GetKey("joystick " + index + " button 1") && !animPlay && transform.tag != "Player" || 
            Input.GetKey(KeyCode.Space)  && !animPlay && transform.tag != "Player" )
        {
            GetComponent<PlayerEffect>().ChargeEffect();
            chargeSlider.fillAmount += 0.005f;
            
        }
        else if (chargeSlider.fillAmount > 0)
        {
            
            animPlay = true;
            animator.SetBool("Walk", false);
            if (Smash)
            {
                animator.SetBool("Smash", true);
                if (!jump)
                {
                    //rb.AddForce(transform.up * 5f,ForceMode.Impulse);
                    jump = true;
                }
            }
            if (right) { animator.SetBool("Right", true); }
            if (left) { animator.SetBool("Left", true); }
           
            if (!right && !left && !Smash)
            { animator.SetBool("Front", true); }
        }
        if(!animPlay)
        {
            
            if(rb.velocity.magnitude > 0.01f)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        Vector3 center = GameObject.Find("court").transform.position;
        float dirX = center.x - transform.position.x;
        Vector3 dir = (center - transform.position).normalized;
        float velocityZ = rb.velocity.z;

        BoxCollider boxCollider = transform.Find("尻尾").GetComponent<BoxCollider>();
        if (transform.tag == "WhiteTeam")
        {
            nameText.transform.localRotation = Quaternion.Euler(0, 180, 0);
            chargeSlider.transform.localRotation = Quaternion.Euler(90,0, 162);
            scoreBoard.transform.localRotation = Quaternion.Euler(0,180, 0);
            if (chargeSlider.fillAmount >= 0.4f)
            {
                Smash = true;
                jump = false;
                chargeSlider.fillAmount = 0.4f;
                hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2);
                boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
            }
            else
            {
                hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.05f, 8 - chargeSlider.fillAmount * 10 - dirX / 3, transform.position.z + velocityZ / 2);
                boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
            }

        }

        if (transform.tag == "RedTeam")
        {
            nameText.transform.localRotation = Quaternion.Euler(0, 0, 0);
            chargeSlider.transform.localRotation = Quaternion.Euler(-90, 0, 162);
            scoreBoard.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if (chargeSlider.fillAmount >= 0.4f)
            {
                Smash = true;
                jump = false;
                chargeSlider.fillAmount = 0.4f;
                hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2 );
                boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
            }
            else
            {
                hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 8 - chargeSlider.fillAmount * 10 + dirX / 3, transform.position.z + velocityZ /2);
                boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
            }

        }

        
        var distance = (hitPoint.transform.position - center).normalized;

        if(distance.z <= -0.4f) 
        { 
            hitPoint.transform.SetParent(null);
            hitPoint.transform.SetParent(null);

            Vector3 pos = hitPoint.transform.position;
            pos.z = center.z - 5f; // 固定したいZの値にする
            hitPoint.transform.position = pos;
        }
        else if(distance.z >= 0.4f)
        {
            hitPoint.transform.SetParent(null);
            hitPoint.transform.SetParent(null);

            Vector3 pos = hitPoint.transform.position;
            pos.z = center.z + 5f; // 固定したいZの値にする
            hitPoint.transform.position = pos;
        }
        else
        {
            hitPoint.transform.SetParent(transform.Find("尻尾"));
        }

        if(transform.position.y <= -10)
        {
            fall = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "RedSetTeam")
        {
            gameObject.tag = "RedTeam";
            //gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (collision.gameObject.name == "WhiteSetTeam")
        {
            gameObject.tag = "WhiteTeam";
            //gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

        if (collision.gameObject.name == "Shuttle")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * 2;
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Shuttle")
    //    {
    //        var norm = (other.transform.position - transform.position).normalized;
    //        //var magnitude = (other.transform.position.z - transform.position.z);
    //        if (norm.z < -0.3f)
    //        { right = true; }
    //        if (norm.z > 0.3f) { left = true; }

            
    //    }
    //}
    private void WhichDir()
    {
        GameObject[] shuttle = GameObject.FindGameObjectsWithTag("Shuttle");
        if (shuttle == null) return;
        for (int i = 0,max = shuttle.Length; i < max; i++)
        {
            var norm = (shuttle[i].transform.position - transform.position).normalized;
            var distance = Vector3.Distance(shuttle[i].transform.position , transform.position);
            if (distance >= 6) return;
            if(norm.z <= -0.3f) { right = true; }
            if(norm.z >= 0.3f) { left = true; }
        }

    }

    private void OnMove()
    {
        float Speed = 8;
        if (rb.isKinematic) return;
        // InputManager で設定した名前を動的に作る
        string horizontalAxisL = "Horizontal_P" + index + "_L";
        string verticalAxisL = "Vertical_P" + index + "_L";

        float moveX = Input.GetAxisRaw(horizontalAxisL);
        float moveY = Input.GetAxisRaw(verticalAxisL);

        Vector3 moveDir = new Vector3(moveY, 0, -moveX).normalized;

        if (CommonModule.CheckHasChild("SpeedUpEffect(Clone)", this.transform)) {
            moveDir *= 2;
        }

        // プレイヤーを移動
        Vector3 conVelocity = rb.velocity;
        conVelocity.x = moveDir.x * Speed;
        conVelocity.z = moveDir.z * Speed;
        rb.velocity = conVelocity;

        string horizontalCrossAxisL = "HorizontalCross_P" + index + "_L";
        string verticalCrossAxisL = "VerticalCross_P" + index + "_L";

        if (!stampPlay)
        {
            if (Input.GetAxis(horizontalCrossAxisL) > 0)
            {
                var stampObj = Instantiate(stampPrefab[0], transform.Find("PlayerUI/スタンプ"));
                stampObj.transform.LookAt(Camera.main.transform.position);
                stampPlay = true;
            }
            if (Input.GetAxis(horizontalCrossAxisL) < 0)
            {
                //ここはエモートにしようかな？
            }

            if (Input.GetAxis(verticalCrossAxisL) > 0)
            {
                var stampObj = Instantiate(stampPrefab[2], transform.Find("PlayerUI/スタンプ"));
                stampObj.transform.LookAt(Camera.main.transform.position);
                stampPlay = true;
            }
            if (Input.GetAxis(verticalCrossAxisL) < 0)
            {
                var stampObj = Instantiate(stampPrefab[1], transform.Find("PlayerUI/スタンプ"));
                stampObj.transform.LookAt(Camera.main.transform.position);
                stampPlay = true;
            }
        }
        if (stampPlay)
        {
            stampTime += Time.deltaTime;
            if(stampTime > 2f)
            {
                stampTime = 0;
                stampPlay = false;
                Destroy(transform.Find("PlayerUI/スタンプ").GetChild(0).gameObject);
            }
        }

            //string horizontalAxisR = "Horizontal_P" + index + "_R";
            //string verticalAxisR = "Vertical_P" + index + "_R";

            //float rotationX = Input.GetAxisRaw(horizontalAxisR);
            //float rotationY = Input.GetAxisRaw(verticalAxisR);

            //Vector2 rotationNorm = new Vector3(rotationX, rotationY).normalized;

            //float angle = Mathf.Atan2(rotationNorm.x, rotationNorm.y) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //transform.rotation = Quaternion.AngleAxis(0, rotationNorm);

            //float moveKeyX = Input.GetAxisRaw("Hosrizontal");
            //float moveKeyY = Input.GetAxisRaw("Vertical");

            //Vector3 moveKeyDir = new Vector3(moveKeyX, 0, moveKeyY);

            //Vector3 keyVelocity = rb.velocity;
            //keyVelocity.x = moveKeyDir.x * Speed;
            //keyVelocity.z = moveKeyDir.z * Speed;
            //rb.velocity = keyVelocity;

            rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
    }

    public bool AnimEnd()
    {
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

    public bool OffAttack()
    {
        return attack = false;
    }

    public bool OnAttack()
    {
        return attack = true;
    }

    private void ScoreBoard()
    {
        if (GameManager.instance.state != GameManager.gameState.result)
        {
            scoreBoard.SetActive(false);
            scoreTmp = score;
            return;
        }

        score = scoreTmp + (goal * 200) + (save * 100) + (punch * 20) + (counter * 150);

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

    private void Update()
    {
        ScoreBoard();
    }
}