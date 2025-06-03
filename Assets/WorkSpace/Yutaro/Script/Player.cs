using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public List<GameObject> skillObject;
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;

    [SerializeField] public GameObject hitPoint;
    [SerializeField] public Image chargeSlider;
    [SerializeField] private Text nameText;
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private Image[] stampPrefab;
    [SerializeField] private List<GameObject> fashionList;

    [SerializeField] private GameObject demonSowrd;

    public bool attack;

    public int playerName;

    int index;
    Rigidbody rb;
    Animator animator;
    bool animPlay;

    bool Smash;
    bool right;
    bool left;

    public int score;
    int scoreTmp;
    public int goal;
    public int save;
    public int punch;
    public int counter;

    public bool replayCancel;
    public bool ready;
    public bool nextGame;

    bool stampPlay;
    float stampTime;

    [SerializeField]Text replaySkipText;
    GameObject textObj;

    bool fall;

    int fashionCount;
    bool onButton;

    int roundSetCount;
    int colorSetCount;

    [SerializeField] private List<Material> playerMaterial;

    bool enemyCamp;

    float skillGaugeAmountMax = 100;
    Image skillGauge;
    float duration = 0.2f;
    float currentRate = 0.0f;
    float skillTime;
    public bool useSkill;
    public enum SkillType
    {
        Normal,
        Pirate,
        Demon,
        Winter,

        Max
    }
    public SkillType skillType;
    GameObject skillEffect;
    public float Speed = 0;

    bool onSkillItem;

    void Start()
    {
        animator = GetComponent<Animator>();

        index = GameManager.instance.playerIndex;

        rb = GetComponent<Rigidbody>();

        // chargeSlider.fillAmount = 0f;
        playerName = GameManager.instance.playerList.Count;
        SkinnedMeshRenderer[] skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer renderer in skinnedRenderers)
        {
            renderer.material = playerMaterial[playerName - 1];
            if(renderer.gameObject.name == "球") { renderer.material = playerMaterial[4]; }
            if(renderer.gameObject.name == "球.001") { renderer.material = playerMaterial[4]; }
        }
        
        skillGauge?.transform?.DOKill();
    }

    private void FixedUpdate()
    {
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
        
        if (GameManager.instance.state == GameManager.gameState.standBy)
        {
            GameObject card = GameObject.Find("Card" + playerName).gameObject;
            Text readyText = transform.Find("PlayerUI/準備完了").GetComponent<Text>();
            if (transform.tag != "Player")
            {

                if (Input.GetKey("joystick " + index + " button 7"))
                {
                    ready = true;
                    readyText.text = "Ready";
                    readyText.color = Color.yellow;
                }
                if (Input.GetKey("joystick " + index + " button 6") || !ready)
                {
                    ready = false;
                    readyText.text = "UnReady";
                    readyText.color = new Color32(90, 90, 90, 255);
                }
                card.transform.Find("NotTeam").GetComponent<Text>().enabled = false;
            }
            else
            {
                card.transform.Find("NotTeam").GetComponent<Text>().enabled = true;
            }

            useSkill = false;
            score = 0;
            goal = 0;
            save = 0;
            punch = 0;
            counter = 0;
            readyText.enabled = true;
            currentRate = 0;
        }
        else
        {
            Text readyText = transform.Find("PlayerUI/準備完了").GetComponent<Text>();
            readyText.enabled = false;
        }
        if (GetComponent<ReplayRecorder>().isReplaying) return;
        if (GameManager.instance.roundStart) { rb.isKinematic = true; ready = false; }
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
        

        OnMove();
        WhichDir();
        if (Input.GetKey("joystick " + index + " button 0"))
        {
            if (gameObject.transform.tag == "RedTeam")
                transform.DORotate(Vector3.zero, 0.5f);
            else
                transform.DORotate(Vector3.zero + new Vector3(0, 180, 0), 0.5f);
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
            }
            if (right) { animator.SetBool("Right", true); }
            if (left) { animator.SetBool("Left", true); }
           
            if (!right && !left && !Smash)
            { animator.SetBool("Front", true); }
        }

        if(chargeSlider.fillAmount < 0)
        {
            chargeSlider.fillAmount = 0;
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
            chargeSlider.transform.localRotation = Quaternion.Euler(90, 0, 162);
            scoreBoard.transform.localRotation = Quaternion.Euler(0, 180, 0);
            if (transform.position.x <= -1)
            {
                hitPoint.transform.localPosition = new Vector3(transform.position.x - 2, 1, transform.position.z);
                
                enemyCamp = true;
            }
            else
            {
                enemyCamp = false;
            }
            if (!enemyCamp)
            {
                
                if (chargeSlider.fillAmount >= 0.4f)
                {
                    Smash = true;
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
                    if (useSkill)
                    {
                        hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2);
                    }
                }

            }
        }

        if (transform.tag == "RedTeam")
        {
            nameText.transform.localRotation = Quaternion.Euler(0, 0, 0);
            chargeSlider.transform.localRotation = Quaternion.Euler(-90, 0, 162);
            scoreBoard.transform.localRotation = Quaternion.Euler(0, 0, 0);
            if(transform.position.x >= -1)
            {
                hitPoint.transform.localPosition = new Vector3(transform.position.x + 2, 1, transform.position.z);
                enemyCamp = true;
            }
            else
            {
                enemyCamp = false;
            }
            if (!enemyCamp)
            {
                
                if (chargeSlider.fillAmount >= 0.4f)
                {
                    Smash = true;
                    chargeSlider.fillAmount = 0.4f;
                    hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2);
                    boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                    boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
                }

                else
                {
                    hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 8 - chargeSlider.fillAmount * 10 + dirX / 3, transform.position.z + velocityZ / 2);
                    boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                    boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
                    if (useSkill)
                    {
                        hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.05f, 4f + dirX * 0.1f, transform.position.z + velocityZ / 2);
                    }
                }
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
        if (GameManager.instance.state != GameManager.gameState.standBy) return;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SlowChage")
        {
            chargeSlider.fillAmount -= 0.001f;
        }


        if (GameManager.instance.state != GameManager.gameState.standBy) return;

        if (other.gameObject.tag == "Fashion")
        {
            if (Input.GetKey("joystick " + index + " button 4") && !onButton)
            {
                GameObject fashion = transform.Find("アーマチュア/ボーン.001/衣装").gameObject;               
                if(fashionCount >= fashionList.Count) 
                {
                    fashionCount = 0;
                    Destroy(fashion.transform.GetChild(0).gameObject);
                    Instantiate(fashionList[fashionCount], fashion.transform);
                    fashionCount++;
                }
                else
                {
                    Destroy(fashion.transform.GetChild(0).gameObject);
                    Instantiate(fashionList[fashionCount], fashion.transform);
                    fashionCount++;
                }
               
                onButton = true;
            }
            if (!Input.GetKey("joystick " + index + " button 4"))
            {
                onButton = false;
            }
            Debug.Log(fashionCount);
        }

        if (other.gameObject.tag == "Time")
        {
            if (Input.GetKey("joystick " + index + " button 4") && !onButton)
            {
                roundSetCount++;
                if(roundSetCount == 1) { GameManager.instance.roundTime = 30; }
                if(roundSetCount == 2) { GameManager.instance.roundTime = 120; }
                if(roundSetCount == 3) { GameManager.instance.roundTime = 60; roundSetCount = 0; }

                onButton = true;
            }
            if (!Input.GetKey("joystick " + index + " button 4"))
            {
                onButton = false;
            }
        }

        if (other.gameObject.tag == "Color")
        {
            if (Input.GetKey("joystick " + index + " button 4") && !onButton)
            {
                colorSetCount++;
                if (colorSetCount == 1) { playerMaterial[playerName - 1].color = Color.red; }
                if (colorSetCount == 2) { playerMaterial[playerName - 1].color = new Color32(250, 250, 250, 255); }
                if (colorSetCount == 3) { playerMaterial[playerName - 1].color = new Color32(50, 50, 50, 255); }
                if (colorSetCount == 4) { playerMaterial[playerName - 1].color = new Color32(180, 100, 200, 255); }
                if (colorSetCount == 5) { playerMaterial[playerName - 1].color = new Color32(100, 100, 255, 255); }
                if (colorSetCount == 6) { playerMaterial[playerName - 1].color = new Color32(160, 240, 255, 255); }
                if (colorSetCount == 7) { playerMaterial[playerName - 1].color = new Color32(255, 255, 100, 255); colorSetCount = 0; }

                onButton = true;
            }
            if (!Input.GetKey("joystick " + index + " button 4"))
            {
                onButton = false;
            }
        }
    }
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

        //skillEffect = transform.Find("アーマチュア/ボーン.001/衣装").GetChild(0).GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        skillEffect = transform.Find("アーマチュア/ボーン.001/衣装").GetChild(0).transform.Find("SkillEffect").GetChild(0).gameObject;
        
        if (useSkill)
        {
            SetLayerRecursively(skillEffect, 10);
            

            if (skillType == SkillType.Normal)
            {
                var main = skillEffect.transform.GetComponent<ParticleSystem>().main;
                main.startColor = playerMaterial[playerName - 1].color;
                Speed = 10;
            }

            if (skillType == SkillType.Demon)
            {
                if (!onSkillItem)
                {
                    if (transform.tag == "WhiteTeam")
                    {
                        GameObject sword = Instantiate(demonSowrd, new Vector3(-5, 56, 3.36f), Quaternion.Euler(180,0,0));
                        skillObject.Add(sword);
                    }
                    if (transform.tag == "RedTeam")
                    {
                        GameObject sword = Instantiate(demonSowrd, new Vector3(5, 56, 3.36f), Quaternion.Euler(180, 0, 0));
                        skillObject.Add(sword);
                    }
                    onSkillItem = true;
                }
                


                Speed = 10;
            }

            if (skillType == SkillType.Winter)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20);
                foreach (Collider hit in colliders)
                {
                    if (ReplayRecorder.instance.isReplaying) return;
                    if (hit.GetComponent<Player>() != null)
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();
                        if (!hit.GetComponent<Player>().useSkill && hit.GetComponent<Player>().skillType != SkillType.Normal)
                        {
                            // 移動速度を遅くする
                            rb.velocity *= 0.5f;
                        }
                    }
                }
                Speed = 10;
            }

        }
        else
        {
            onSkillItem = false;
            for (int i = 0; i < skillObject.Count; i++)
            {
                Destroy(skillObject[i]);
            }
            SetLayerRecursively(skillEffect, 11);
            Speed = 8;
        }

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

    void SetLayerRecursively(GameObject obj, int layer)
    {
        if (obj == null) return;

        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
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
        if (GameManager.instance.state == GameManager.gameState.result)
        {
            nameText.transform.localPosition = new Vector3(1.86f,10f,0);
            nameText.color = Color.black;
            if (Input.GetKey("joystick " + index + " button 1"))
            {
                nextGame = true;
                nameText.text = "READY FOR THE NEXT GAME";
            }
        }
        else
        {
            nameText.transform.localPosition = new Vector3(1.86f, 6.66f, 0);
            nameText.text = playerName.ToString() + "P";
            if (gameObject.tag == "RedTeam") { nameText.color = Color.red; }
            if (gameObject.tag == "WhiteTeam") { nameText.color = Color.white; }
        }

        ScoreBoard();
        SkillGauge();
    }

    public void SkillGauge()
    {
        if (GameManager.instance.state != GameManager.gameState.start || rb.isKinematic) return;

        skillGauge = GameObject.Find("Card" + playerName).transform.Find("SkillGauge").GetComponent<Image>();
        if (!useSkill)
        {
            float targetRate = currentRate + 1f / skillGaugeAmountMax;
            skillGauge.fillAmount = targetRate;
            currentRate = targetRate;
        }
       
        if (currentRate >= 1)
        {
            currentRate = 1;
            if (Input.GetKey("joystick " + index + " button 3"))
            {
                currentRate = 0;
                skillGauge?.DOFillAmount(currentRate, duration);
                useSkill = true;
                if(transform.Find("アーマチュア/ボーン.001/衣装").GetChild(0).name == "衣装通常(Clone)")
                {
                    skillType = SkillType.Normal;
                }
                else if(transform.Find("アーマチュア/ボーン.001/衣装").GetChild(0).name == "衣装海賊(Clone)")
                {
                    skillType = SkillType.Pirate;
                }
                else if (transform.Find("アーマチュア/ボーン.001/衣装").GetChild(0).name == "衣装悪魔(Clone)")
                {
                    skillType = SkillType.Demon;
                }
                else if (transform.Find("アーマチュア/ボーン.001/衣装").GetChild(0).name == "衣装冬(Clone)")
                {
                    skillType = SkillType.Winter;
                }
            }
        }
        if (currentRate <= 0)
        {
            currentRate = 0;
        }

        if(useSkill)
        {
            skillTime += Time.deltaTime;
            if(skillTime > 10)
            {
                skillTime = 0;
                useSkill = false;
            }
        }
    }
}