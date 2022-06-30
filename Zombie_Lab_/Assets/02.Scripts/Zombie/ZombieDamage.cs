using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieDamage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    // 생명 게이지 
    private float hp = 100.0f;
    // 초기 생명 수치
    private float initHp = 100.0f;

    // 피격시 사용할 혈흔 효과
    private GameObject bloodEffect;

    // 생명 게이지 프리팹을 저장할 변수
    public GameObject hpBarPrefab;
    // 생명 게이지의 위치를 보정할 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 3.3f, 0);
    // 부모가 될 Canvas 객체
    private Canvas uiCanvas;
    // 생명 수치에 따라 fillAmount 속성을 변경할 Image
    private Image hpBarImage;


    void Start()
    {
        // 혈흔 효과 프리팹을 로드
        bloodEffect = Resources.Load<GameObject>("BulletImpactFleshBigEffect");

        // 생명 게이지의 생성 및 초기화
        SetHpBar();
       
    }

    void SetHpBar()
    {
        
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        // 생명 게이지가 따라가야 할 대상과 오프셋 값 설정
        var _hpBar = hpBar.GetComponent<ZombieHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offsert = hpBarOffset;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.tag == bulletTag)
        {
            // 혈흔 효과를 생성하는 함수 호출 
            ShowBloodEffect(coll);
            // 총알 삭제 
            Destroy(coll.gameObject);
            // 생명 게이지 차감 
            hp -= coll.gameObject.GetComponent<BulletCtrl>().damage;
            // 생명 게이지의 fillAmount 속성을 변경
            hpBarImage.fillAmount = hp / initHp;

            if (hp <= 0.0f)
            {
                // 좀비의 상태를 DIE로 변경
                GetComponent<ZombieAI>().state = ZombieAI.State.DIE;
                // 좀비가 사망한 이후 생명 게이지를 투명 처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;


                // 좀비의 사망 횟수를 누적시키는 함수 호출
                GameManager.instance.IncKillCount();
                
                GetComponent<CapsuleCollider>().enabled = false;

            }
        }
    }

    // 혈흔 효과를 생성하는 함수 
    void ShowBloodEffect(Collision coll)
    {
        // 총알이 충돌한 지점 산출 
        Vector3 pos = coll.contacts[0].point;
        // 총알이 충돌했을 때 법선 벡터
        Vector3 _normal = coll.contacts[0].normal;
        // 총알의 충돌 시 방향 벡터의 회전값 계산
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);

        // 혈흔 효과 생성 
        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1.5f);
    }
}

