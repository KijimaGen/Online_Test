using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonModule;

public class Cards : MonoBehaviour{

    [SerializeField] GameObject player;
    bool DontGetDamage =false;
    bool SpeedUp =false;

    private void Update() {
        if (player != null) {
            if (DontGetDamage) {
                SetActiveChildByName("NoStanImage", this.transform, true);
            }
            else {
                SetActiveChildByName("NoStanImage", this.transform, false);
            }
            if (SpeedUp) {
                SetActiveChildByName("SpeedUpImage", this.transform, true);
            }
            else {
                SetActiveChildByName("SpeedUpImage", this.transform, false);
            }

            DontGetDamage = CheckHasChild("NoStanEffect(Clone)", player.transform);
            SpeedUp = CheckHasChild("SpeedUpEffect(Clone)", player.transform);
        }
        else {
            this.gameObject.SetActive(false);
        }
    }

    public void ConnectPlayer(GameObject _Player) {
        player = _Player;
    }


}
