using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRemove : MonoBehaviour
{

    // 충돌이 시작할 때 발생하는 이벤트 
    private void OnCollisionEnter(Collision coll)
    {
        // 충돌한 게임오브젝트의 태그값 비교 
        if (coll.collider.tag == "BULLET")
        {
            // 충돌한 게임 오브젝트 삭제
            Destroy(coll.gameObject);
        }
    }
}