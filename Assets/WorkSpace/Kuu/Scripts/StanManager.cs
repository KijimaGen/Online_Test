using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using static UnityEngine.UI.Image;

public class StanManager : MonoBehaviour
{
    [SerializeField] GameObject clone;

    // スタンのフラグ
    bool stanflag;
    // スタン時間
    float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        //var player = GetComponent<Player>();
        //player.enabled = false;
        stanflag = false;
        waitTime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (stanflag = true)
        {
            //async Task UniTask.Delay(TimeSpan.FromSeconds(waitTime));
            stanflag = false;
        }

        // シャトルを出す(作業用)
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton5) == true) {
            Instantiate(clone, new Vector3(0, 5, -10), Quaternion.identity);
        }
    }

    public void OnTriggerStay(Collider collider) {
        // 相手のタグがWhiteTeamだったら
        if (collider.gameObject.CompareTag("WhiteTeam")) {
                Debug.Log("hit");
            if (Input.GetKey(KeyCode.Space)) {
                //chargeSlider.fillAmount += 0.005f;
                Debug.Log("atack");
                stanflag = true;
            }
        }
    }
}
