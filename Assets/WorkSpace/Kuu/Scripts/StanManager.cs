using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanManager : MonoBehaviour
{
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
            //await UniTask.Delay(TimeSpan.FromSeconds(waitTime));
            stanflag = false;
        }
    }

    public void OnTriggerStay(Collider collider) {
        // 相手のタグがWhiteTeamだったら
        if (collider.gameObject.CompareTag("WhiteTeam")) {
                Debug.Log("hit");
            if (Input.GetKey(KeyCode.Space)/* || Input.GetKey("joystick " + index + " button 1")*/) {
                //chargeSlider.fillAmount += 0.005f;
                Debug.Log("atack");
                stanflag = true;
            }
        }
    }
}
