using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanManager : MonoBehaviour
{
    //GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //var player = GetComponent<Player>();
        //player.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider collider) {
        // ‘ŠŽè‚Ìƒ^ƒO‚ªWhiteTeam‚¾‚Á‚½‚ç
        if (collider.gameObject.CompareTag("WhiteTeam")) {
                Debug.Log("hit");
            if (Input.GetKey(KeyCode.Space)/* || Input.GetKey("joystick " + index + " button 1")*/) {
                //chargeSlider.fillAmount += 0.005f;
                Debug.Log("atack");
                
            }
        }
    }
}
