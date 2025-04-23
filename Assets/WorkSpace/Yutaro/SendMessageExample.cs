using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SendMessageExample : MonoBehaviour
{
    int index;
    Rigidbody rb;
    Animator animator;
    // �ʒm���󂯎�郁�\�b�h���́uOn + Action���v�ł���K�v������
    private void Start()
    {
        index = GameManager.instance.playerIndex;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnMove()
    {

        // InputManager �Őݒ肵�����O�𓮓I�ɍ��
        string horizontalAxisL = "Horizontal_P" + index + "_L";
        string verticalAxisL = "Vertical_P" + index + "_L";

        float moveX = Input.GetAxisRaw(horizontalAxisL);
        float moveY = Input.GetAxisRaw(verticalAxisL);

        Vector3 moveDir = new Vector3(moveY, 0, -moveX).normalized;

        // �v���C���[���ړ��i��FTransform�x�[�X�j
        transform.position += moveDir * 5 * Time.deltaTime;


        //string horizontalAxisR = "Horizontal_P" + index + "_R";
        //string verticalAxisR = "Vertical_P" + index + "_R";

        //float rotationX = Input.GetAxisRaw(horizontalAxisR);
        //float rotationY = Input.GetAxisRaw(verticalAxisR);

        //Vector2 rotationNorm = new Vector3(rotationX, rotationY).normalized;

        //float angle = Mathf.Atan2(rotationNorm.x, rotationNorm.y) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //transform.rotation = Quaternion.AngleAxis(0, rotationNorm);
    }


    private void Update()
    {
        OnMove();
        //if (GameManager.instance.playerList)
        if (Input.GetKeyDown("joystick 1 button 4"))
        {
            animator.SetBool("Attack", true);
        }
    }
}