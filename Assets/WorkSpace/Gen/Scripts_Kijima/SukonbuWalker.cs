/**
 * @file SukonbuWalker.cs
 * @brief ��O�łȂ�ƂȂ���������Q�[���I�u�W�F�N�g�̃N���X
 * @author kijima
 * @date 2025/5/23
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SukonbuWalker : MonoBehaviour {
    [SerializeField]
    private GameObject[] wayPointer = null;
    private Vector3 wayPoint = Vector3.zero;
    [SerializeField]
    private float speed = 1.0f;
    int wayPointIndex = 0;

    bool wayPointPlus = true; 
    
    private void Start () {
        wayPointIndex = 1;
        wayPoint = wayPointer[wayPointIndex].transform.position;  
    }

    private void Update() {
        //�C���f�b�N�X�̍ő�
        if (wayPointIndex + 1 > wayPointer.Length - 1)
            wayPointPlus = false;
        if(wayPointIndex == 0)
            wayPointPlus = true;

        //��ɐi�H����������悤�ɂ���
        transform.LookAt(wayPointer[wayPointIndex].transform);
        transform.Rotate(0, transform.rotation.y + 90, 0);

        transform.position =  Vector3.MoveTowards(
                                transform.position,
                                wayPoint,
                                speed * Time.deltaTime);
        //�E�F�C�|�C���g�ɋ߂Â�����s������
        if(Vector3.Distance(wayPoint,transform.position) < 1) {
            if (wayPointPlus) {
                wayPointIndex++;
            }
            else {
                wayPointIndex--;
            }
            wayPoint = wayPointer[wayPointIndex].transform.position;   
        }
    }
}
