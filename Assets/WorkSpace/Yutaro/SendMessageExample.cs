using UnityEngine;
using UnityEngine.InputSystem;

public class SendMessageExample : MonoBehaviour
{
    private PlayerInput newControls;

    // 通知を受け取るメソッド名は「On + Action名」である必要がある
    private void OnMove(InputValue value)
    {
        newControls = new PlayerInput();

        newControls.Enable();
        
    }

    private void Update()
    {
        // オブジェクト移動
        
    }
}