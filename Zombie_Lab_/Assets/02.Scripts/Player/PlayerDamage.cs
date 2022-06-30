using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private const string attackTag = "Zombie";
    private float initHp = 100.0f;
    public float currHp;
    //bool End;
    //private bool hashIsGameOver = Animator.StringToHash("IsGameOver");

    // HP Bar Image를 저장하기 위한 함수
    public Image hpBar;

    public Image bloodScreen;
    public Image gameOverScreen;
    public Image gameOver;

   

   // Animator animator;

    // 생명 게이지의 처음 색상(녹색)
    //private readonly Color initColor = new Vector4(0, 1.0f, 0.0f, 1.0f);
    // private Color currColor;


    
    // Start is called before the first frame update
    void Start()
    {
        currHp = initHp;
        // 생명 게이지의 초기 색상을 설정 
        //hpBar.color = initColor;
        //currColor = initColor;
    }

    // 충돌한 Collider의 IsTrigger 옵션이 체크됐을 때 발생
    void OnTriggerEnter(Collider coll)
    {
        // 충돌한 Collider의 태그가 Zombie이면 Player의 hp를 차감 
        if(coll.tag == attackTag)
        {
            Debug.Log("@@");
            currHp -= 20.0f;

            // 생명 게이지의 색상 및 크기 변경 함수를 호출
            DisplayHpbar();

            StartCoroutine(ShowBloodScreen());

            //Player의 생명이 0 이하이면 사망 처리 
             if(currHp <= 0.0f)
            {
                Debug.Log("!!");
                //End = true;
                Time.timeScale = 1;
               
                PlayerDie();
            }
        }
    }

    IEnumerator ShowBloodScreen()
    {
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.3f);
        bloodScreen.color = Color.clear;
    }

    //Player의 사망 처리 루틴 
    void PlayerDie()
    {
        Debug.Log("Player Die!");
        GameOver_Manager.gameOver = true;

    }

    void DisplayHpbar()
    {
        // 생명 수치가 50%일 때까지는 녹색에서 노란색으로 변경 
       // if ((currHp / initHp) > 0.5f)
        //  currColor.r = (1 - (currHp / initHp)) * 2.0f;
       //else //생명 수치가 0%일 때까지는 노란색에서 빨간색으로 변경
        //    currColor.g = (currHp / initHp) * 2.0f;

        // HpBar의 색상변경
       //hpBar.color = currColor;
        // HpBar의 크기 변경
        hpBar.fillAmount = (currHp / initHp);
    }



    // Update is called once per frame
    void Update()
    {
       
    }
}
