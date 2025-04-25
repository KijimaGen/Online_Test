using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
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

    bool Smash;
    bool right;
    bool left;
    bool jump;

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
        Debug.Log(right);
        if (GetComponent<ReplayRecorder>().isReplaying) return;
        if (GameManager.instance.roundStart) { rb.isKinematic = true; }
        if (!GameManager.instance.roundStart) { rb.isKinematic = false; }


        nameText.text = playerName.ToString() + "P";
        nameText.rectTransform.LookAt(Camera.main.transform);
        if (Input.GetKey(KeyCode.R))
        {
            GetComponent<ReplayRecorder>().StartReplay();
            //chargeSlider.fillAmount += 0.0005f;
        }

        //animator.SetBool("Attack", animPlay);

        OnMove();

        if (Input.GetKey("joystick " + index + " button 1") && !animPlay && transform.tag != "Player" || 
            Input.GetKey(KeyCode.Space)  && !animPlay && transform.tag != "Player" )
        {
            chargeSlider.fillAmount += 0.005f;
        }
        else if (chargeSlider.fillAmount > 0)
        {

            animPlay = true;
            animator.SetBool("Walk", false);
            if (right) { animator.SetBool("Right", true); }
            if (left) { animator.SetBool("Left", true); }
            if (Smash) 
            { 
                animator.SetBool("Smash", true);
                if (!jump)
                {
                    //rb.AddForce(transform.up * 5f,ForceMode.Impulse);
                    jump = true;
                }
            }
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

        float dirX = Vector3.zero.x - transform.position.x;
        float velocityZ = rb.velocity.z;

        BoxCollider boxCollider = transform.Find("尻尾").GetComponent<BoxCollider>();
        if (transform.tag == "WhiteTeam")
        {

            if (chargeSlider.fillAmount >= 0.4f)
            {
                Smash = true;
                jump = false;
                chargeSlider.fillAmount = 0.4f;
                hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.1f, 4f + dirX/4, transform.position.z + velocityZ);
                boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
            }
            else
            {
                hitPoint.transform.position = new Vector3(rb.velocity.x * -dirX * 0.1f, 8 - chargeSlider.fillAmount * 10 - dirX / 3, transform.position.z + velocityZ);
                boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
            }

        }

        if (transform.tag == "RedTeam")
        {
           
            if (chargeSlider.fillAmount >= 0.4f)
            {
                Smash = true;
                jump = false;
                chargeSlider.fillAmount = 0.4f;
                hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.1f, 4f + dirX * 0.1f, transform.position.z + velocityZ);
                boxCollider.center = new Vector3(boxCollider.center.x, 10, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 10, boxCollider.size.z);
            }
            else
            {
                hitPoint.transform.position = new Vector3(rb.velocity.x * dirX * 0.1f, 8 - chargeSlider.fillAmount * 10 + dirX / 3, transform.position.z + velocityZ);
                boxCollider.center = new Vector3(boxCollider.center.x, 1.18f, boxCollider.center.z);
                boxCollider.size = new Vector3(boxCollider.size.x, 5.37f, boxCollider.size.z);
            }

             //Debug.Log(dirX * 0.1f);
        }

        Vector3 center = GameObject.Find("court").transform.position;
        var distance = (hitPoint.transform.position - center).normalized;

       // Debug.Log(distance);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Shuttle")
        {
            var norm = (other.transform.position - transform.position).normalized;
            //var magnitude = (other.transform.position.z - transform.position.z);
            if (norm.z < -0.3f)
            { right = true; }
            if (norm.z > 0.3f) { left = true; }

            
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


        // プレイヤーを移動
        Vector3 conVelocity = rb.velocity;
        conVelocity.x = moveDir.x * Speed;
        conVelocity.z = moveDir.z * Speed;
        rb.velocity = conVelocity;


        //string horizontalAxisR = "Horizontal_P" + index + "_R";
        //string verticalAxisR = "Vertical_P" + index + "_R";

        //float rotationX = Input.GetAxisRaw(horizontalAxisR);
        //float rotationY = Input.GetAxisRaw(verticalAxisR);

        //Vector2 rotationNorm = new Vector3(rotationX, rotationY).normalized;

        //float angle = Mathf.Atan2(rotationNorm.x, rotationNorm.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //transform.rotation = Quaternion.AngleAxis(0, rotationNorm);

        //float moveKeyX = Input.GetAxisRaw("Horizontal");
        //float moveKeyY = Input.GetAxisRaw("Vertical");

        //Vector3 moveKeyDir = new Vector3(moveKeyX, 0, moveKeyY);

        //Vector3 keyVelocity = rb.velocity;
        //keyVelocity.x = moveKeyDir.x * Speed;
        //keyVelocity.z = moveKeyDir.z * Speed;
        //rb.velocity = keyVelocity;

        //rb.AddForce(Vector3.down * 9.81f, ForceMode.Acceleration);
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
}