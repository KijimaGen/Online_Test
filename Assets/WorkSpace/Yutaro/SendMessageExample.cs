using UnityEngine;
using UnityEngine.InputSystem;

public class SendMessageExample : MonoBehaviour
{
    private PlayerInput newControls;

    // �ʒm���󂯎�郁�\�b�h���́uOn + Action���v�ł���K�v������
    private void OnMove(InputValue value)
    {
        newControls = new PlayerInput();

        newControls.Enable();
        
    }

    private void Update()
    {
        // �I�u�W�F�N�g�ړ�
        
    }
}