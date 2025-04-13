using Unity.Netcode;
using UnityEngine;
public class Player : NetworkBehaviour
{
    [SerializeField] float m_moveSpeed = 1;

    private Rigidbody m_rigidBody;
    private Vector2 m_moveInput = Vector2.zero;

    void Start()
    {
        // Rigidbody ���擾
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //owner�̏ꍇ
        if (IsOwner)
        {
            // �ړ����͂�ݒ�
            SetMoveInputServerRpc(
                    Input.GetAxisRaw("Horizontal"),
                    Input.GetAxisRaw("Vertical"));
        }

        //�T�[�o�[�i�z�X�g�j�̏ꍇ
        if (IsServer)
        {
            ServerUpdate();
        }
    }

    //=================================================================
    //RPC
    //=================================================================
    // �ړ����͂��Z�b�g����RPC
    [ServerRpc]
    private void SetMoveInputServerRpc(float x, float y)
    {
        m_moveInput = new Vector2(x, y);
    }

    //=================================================================
    //�T�[�o�[���ōs������
    //=================================================================
    // �T�[�o�[�����ŌĂяo��Update
    private void ServerUpdate()
    {
        //�ړ�
        var velocity = Vector3.zero;
        velocity.x = m_moveSpeed * m_moveInput.normalized.x;
        velocity.z = m_moveSpeed * m_moveInput.normalized.y;
        //�ړ�����
        m_rigidBody.AddForce(velocity);
    }
}