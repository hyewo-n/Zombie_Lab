using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    // Animator 컴포넌트를 저장할 변수 
    private Animator animator;
    // 플레이어의 Transform 컴포넌트 
    private Transform playerTr;
    // 좀비의 Transform 컴포넌트 
    private Transform enemyTr;

    // 애니메이터 컨트롤러에 정의한 파라미터의 해시값을 미리 추출 
    private readonly int hashAttack = Animator.StringToHash("Attack");

    // 다음 공격할 시간 계산용 변수
    private float nextAttack = 0.0f;
    // 공격 간격 
    private readonly float attackRate = 1.0f;
    // 플레이어를 향해 회전할 속도 계수
    private readonly float damping = 10.0f;

    // 공격 여부를 판단할 변수 
    public bool isAttack = false;
    // 공격 사운드를 저장할 변수 
    //public AudioClip attackSfx;

    private float playerHp = 100.0f;
    public float currHp;

    void Start()
    {
        // 컴포넌트 추출 및 변수 저장 
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        //audio = GetComponent<AudioSource>();

        currHp = playerHp;
    }

    void Update()
    {
        if (isAttack)
        {
            // 현재 시간이 다음 공격 시간보다 큰지를 확인 
            if (Time.time >= nextAttack)
            {
                Attack();
                // 다음 공격 시간 계산 
                nextAttack = Time.time + attackRate + Random.Range(0.0f, 0.3f);
            }

            // 플레이어가 있는 위치까지의 회전 각도 계산 
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            // 보간함수를 사용해 점진적으로 회전 
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    void Attack()
    {
        animator.SetTrigger(hashAttack);
        // audio.PlayOneShot(attackSfx, 1.0f);

        currHp -= 5.0f;
        Debug.Log("HP =" + currHp.ToString());
    }
}
