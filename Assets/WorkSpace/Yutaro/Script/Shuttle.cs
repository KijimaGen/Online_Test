using UnityEngine;

public class Shuttle : MonoBehaviour
{
    bool fallIN;
    bool fallOUT;

    Rigidbody rb;

    RaycastHit[] hits;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
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

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
