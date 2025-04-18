using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SendMessageExample : MonoBehaviour
{
    int index;
    Rigidbody rb;
    // 通知を受け取るメソッド名は「On + Action名」である必要がある
    private void Start()
    {
        index = GameManager.instance.playerIndex;
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove()
    {
        if (Input.GetKeyDown("joystick " + index.ToString() + " button 4"))
        {
            
        }

        // InputManager で設定した名前を動的に作る
        string horizontalAxisL = "Horizontal_P" + index + "_L";
        string verticalAxisL = "Vertical_P" + index + "_L";

        float moveX = Input.GetAxisRaw(horizontalAxisL);
        float moveY = Input.GetAxisRaw(verticalAxisL);

        Vector3 moveDir = new Vector3(moveY, 0, -moveX).normalized;

        // プレイヤーを移動（例：Transformベース）
        transform.position += moveDir * 5 * Time.deltaTime;


        string horizontalAxisR = "Horizontal_P" + index + "_R";
        string verticalAxisR = "Vertical_P" + index + "_R";

        float rotationX = Input.GetAxisRaw(horizontalAxisR);
        float rotationY = Input.GetAxisRaw(verticalAxisR);

        Vector2 rotationNorm = new Vector3(rotationX, rotationY).normalized;

        float angle = Mathf.Atan2(rotationNorm.x, rotationNorm.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //transform.rotation = Quaternion.AngleAxis(0, rotationNorm);
    }

    private void KeyMmove()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveY).normalized;
        transform.position += move * 2;
    }

    private void Update()
    {
        OnMove();
        //if (GameManager.instance.playerList)
        
    }
}