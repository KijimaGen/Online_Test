using UnityEngine;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    RaycastHit[] hits;

    void Update()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Player");
        if (target != null)
        {
            for (int i = 0; i < target.Length; i++)
            {
                Shoot(target[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<ReplayRecorder>().StartReplay();
        }

    }



    private void Shoot(GameObject _Target)
    {
        Vector3 direction = (_Target.transform.position - transform.position).normalized;
        float radius = 0.1f;
        float maxDistance = 1f;

        // SphereCastAll
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, direction, maxDistance);

        // Debug用Rayをシーンに描画（赤い線）
        Debug.DrawRay(transform.position, direction * maxDistance, Color.red);

        // ヒットしたオブジェクトの名前を表示
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                rb.AddForce(direction * -2, ForceMode.Impulse);
                Quaternion targetRotation = Quaternion.LookRotation(rb.velocity.normalized);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 5f));
            }
        }

    }

    void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "赤床") 
        { 
            ScoreManager.instance.whiteScore++; 
            gameObject.GetComponent<Collider>().enabled = false; 
            gameObject.GetComponent<MeshRenderer>().enabled = false; 
        }

        if (collision.gameObject.name == "白床")
        {
            ScoreManager.instance.redScore++;
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

}
