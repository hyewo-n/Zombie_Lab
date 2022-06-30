using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    // 좀비의 상태를 표현하기 위한 열거형 변수 정의 
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    // 상태를 저장할 변수 
    public State state = State.PATROL;

    // 플레이어의 위치를 저장할 변수 
    private Transform playerTr;
    // 좀비의 위치를 저장할 변수 
    private Transform enemyTr;
    // Animator 컴포넌트를 저장할 변수
    private Animator animator;

    //공격 사정거리 
    public float attackDist = 0.1f;
    //추적 사정거리 
    public float traceDist = 8.0f;

    // 사망 여부를 판단할 변수 
    public bool isDie = false;

    // 코루틴에서 사용한 지연시간 변수 
    private WaitForSeconds ws;
    // 이동을 제어하는 MoveAgent 클래스를 저장할 변수 
    private MoveAgent moveAgent;

    // 애니메이터 컨트롤러에 정의한 파라미터의 해시값을 미리 추출 
   // private readonly int hashMove = Animator.StringToHash("IsMove");
    //private readonly int hashSpeed = Animator.StringToHash("speed");

    // 공격을 제어하는 ZombieAttack 클래스를 저장할 변수 
    private ZombieAttack zombieAttack;

    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDieIdx = Animator.StringToHash("DieIdx");
    private readonly int hashOffset = Animator.StringToHash("Offset");
    private readonly int hashWalkSpeed = Animator.StringToHash("WalkSpeed");

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        // 플레이어 게임오브젝트 추출
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        // 플레이어의 Transform 컴포넌트 추출 
        if (player != null)
            playerTr = player.GetComponent<Transform>();

        // 좀비의 Transform 컴포넌트 추출 
        enemyTr = GetComponent<Transform>();

        // Animator 컴포넌트 추출 
        animator = GetComponent<Animator>();

        // 이동을 제어하는 MoveAgent 클래스를 추출
        moveAgent = GetComponent<MoveAgent>();

        // 공격을 제어하는 ZombieAttack 클래스를 추출
        zombieAttack = GetComponent<ZombieAttack>();

        //코루틴의 지연시간 생성 
        ws = new WaitForSeconds(0.3f);

        // Cycly offset 값을 불규칙하게 변경
        //animator.SetFloat(hashOffset, Random.Range(0.0f, 1.0f));
        // Speed 값을 불규칙하게 변경 
        //animator.SetFloat(hashWalkSpeed, Random.Range(0.5f, 1.0f));
    }

    void OnEnable()
    {
        // CheckState 코루틴 함수 실행 
        StartCoroutine(CheckState());
        // Action 코루틴 함수 실행 
        StartCoroutine(Action());
    }

    // 좀비의 상태를 검사하는 코루틴 함수 
    IEnumerator CheckState()
    {
        // 좀비가 사망하기 전까지 도는 무한루프
        while (!isDie)
        {
            // 상태가 사망이면 코루틴 함수를 종료시킴
            if (state == State.DIE) yield break;

            // 플레이어와 좀비 간의 거리를 계산
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            // 공격 사정거리 이내인 경우
            if (dist <= attackDist)
            {
                state = State.ATTACK;
            } //추적 사정거리 이내인 경우 
            else if (dist <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            // 0.3초동안 대기하는 동안 제어권을 양보 
            yield return ws;
        }
    }

    IEnumerator Action()
    {
        //좀비가 사망할 때까지 무한루프
        while (!isDie)
        {
            yield return ws;
            // 상태에 따라 분기 처리
            switch (state)
            {
                case State.PATROL:
                    // 공격 정지
                    zombieAttack.isAttack = false;

                    // 이동 모드를 활성화
                    moveAgent.patrolling = true;
                    break;

                case State.TRACE:
                    // 공격 정지
                    zombieAttack.isAttack = false;

                    // 플레이어의 위치를 넘겨 추적 모드로 변경 
                    moveAgent.traceTarget = playerTr.position;
                    break;

                case State.ATTACK:
                    // 이동 및 추적을 정지 
                    moveAgent.Stop();

                    // 공격 시작
                    if (zombieAttack.isAttack == false)
                        zombieAttack.isAttack = true;
                    break;

                case State.DIE:
                    isDie = true;
                    zombieAttack.isAttack = false;
                    // 이동 및 추적을 정지 
                    moveAgent.Stop();

                    // 사망 애니메이션 실행 
                    animator.SetInteger(hashDieIdx,Random.Range(0,2));
                    animator.SetTrigger(hashDie);

                    // Capsule Collider 비활성화 
                    GetComponent<CapsuleCollider>().enabled = false;
                    StartCoroutine("FadeIn");
                    break;
            }
        }
        IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(1);
            while (_spriteRenderer.color.a > 0)
            {
                var color = _spriteRenderer.color;
                //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
                color.a -= (.5f * Time.deltaTime);

                _spriteRenderer.color = color;
                //wait for a frame
                yield return null;
            }
            Destroy(gameObject);
        }

    }

   /* private void Update()
    {
        // Speed 파라미터에 이동 속도를 전달 
        animator.SetFloat(hashSpeed, moveAgent.speed);
    } */
}
