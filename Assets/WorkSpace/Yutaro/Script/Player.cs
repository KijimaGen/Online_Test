using Unity.Netcode;
using UnityEngine;
public class Player : NetworkBehaviour
{
    [SerializeField] float m_moveSpeed = 1;

    private Rigidbody m_rigidBody;
    private Vector2 m_moveInput = Vector2.zero;

    void Start()
    {
        // Rigidbody を取得
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //ownerの場合
        if (IsOwner)
        {
            // 移動入力を設定
            SetMoveInputServerRpc(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"));
        }

        //サーバー（ホスト）の場合
        if (IsServer)
        {
            ServerUpdate();
        }
    }

    //=================================================================
    //RPC
    //=================================================================
    // 移動入力をセットするRPC
    [ServerRpc]
    private void SetMoveInputServerRpc(float x, float y)
    {
        m_moveInput = new Vector2(x, y);
    }

    //=================================================================
    //サーバー側で行う処理
    //=================================================================
    // サーバーだけで呼び出すUpdate
    private void ServerUpdate()
    {
        //移動
        var velocity = Vector3.zero;
        velocity.x = m_moveSpeed * m_moveInput.normalized.x;
        velocity.z = m_moveSpeed * m_moveInput.normalized.y;
        //移動処理
        m_rigidBody.AddForce(velocity);
    }
}