using UnityEngine;

public class Ball : MonoBehaviour
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
        GameObject target = GameObject.Find("Player1");
        if (target != null)
            Shoot(target);
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
                //Debug.Log(hit.collider.GetComponent<Rigidbody>().velocity.normalized);
            }
        }

    }

}
