using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    bool redTeam;
    bool whiteTeam;
    bool independentTeam;

    [SerializeField] public GameObject hitPoint;
    [SerializeField] public Image chargeSlider;
    [SerializeField] private Text nameText;

    public bool attack;

    int playerName;

    int index;
    Rigidbody rb;
    Animator animator;
    bool animPlay;

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
        nameText.text = playerName.ToString() + "P";
        nameText.rectTransform.LookAt(Camera.main.transform);
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<ReplayRecorder>().StartReplay();
            //chargeSlider.fillAmount += 0.0005f;
        }
        



        
        animator.SetBool("Attack",animPlay);
        OnMove();

        if (Input.GetKey(KeyCode.Space) || Input.GetKey("joystick " + index + " button 1") && !animPlay)
        {
            chargeSlider.fillAmount += 0.005f;
        }
        else if(chargeSlider.fillAmount > 0)
        {
           
            animPlay = true;
        }
        if (chargeSlider.fillAmount >= 0.4f)
        {
            chargeSlider.fillAmount = 0.4f;
        }

        var dirX = Vector3.zero.x - transform.position.x;
        hitPoint.transform.position = new Vector3(0,10 - chargeSlider.fillAmount * 10 + dirX / 4, transform.position.z);

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

    private void OnMove()
    {

        // InputManager で設定した名前を動的に作る
        string horizontalAxisL = "Horizontal_P" + index + "_L";
        string verticalAxisL = "Vertical_P" + index + "_L";

        float moveX = Input.GetAxisRaw(horizontalAxisL);
        float moveY = Input.GetAxisRaw(verticalAxisL);

        Vector3 moveDir = new Vector3(moveY, 0, -moveX).normalized;

        // プレイヤーを移動（例：Transformベース）
        transform.position += moveDir * 5 * Time.deltaTime;


        //string horizontalAxisR = "Horizontal_P" + index + "_R";
        //string verticalAxisR = "Vertical_P" + index + "_R";

        //float rotationX = Input.GetAxisRaw(horizontalAxisR);
        //float rotationY = Input.GetAxisRaw(verticalAxisR);

        //Vector2 rotationNorm = new Vector3(rotationX, rotationY).normalized;

        //float angle = Mathf.Atan2(rotationNorm.x, rotationNorm.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //transform.rotation = Quaternion.AngleAxis(0, rotationNorm);

        float moveKeyX = Input.GetAxisRaw("Horizontal");
        float moveKeyY = Input.GetAxisRaw("Vertical");

        Vector3 moveKeyDir = new Vector3(moveKeyX, 0, moveKeyY).normalized;

        transform.position += moveKeyDir * 5 * Time.deltaTime;
    }

    
    public bool AnimEnd()
    {
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
}