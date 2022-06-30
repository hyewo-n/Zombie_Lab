using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // 플레이어가 죽인 좀비 캐릭터 수
    [HideInInspector] public int killCount =0;

    // 전체 좀비 수 
    public int remainZombie = 13;

    // 좀비 캐릭터를 죽인 횟수를 표시할 텍스트  UI
    public Text killCountText;
    public static GameManager instance = null;

    public Image missionComplScreen;
    public Image missionCompl;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        //게임의 초기 데이터 로드
        LoadGameData();
    }

    // 게임의 초기 데이터 로드
    void LoadGameData()
    {
        //KILL_COUNT 키로 저장된 값을 로드
        killCount = PlayerPrefs.GetInt("KILL_COUNT", 0);
        // killCountText.text = "KILL " + killCount.ToString("0000");
        killCountText.text = string.Format("<color=#ff0000>{0}</color>/{1}", killCount+1, remainZombie);
    }

    // 좀비가 죽을 때마다 호출되는 함수
    public void IncKillCount()
    {
        ++killCount;
        // killCountText.text = "KILL " + killCount.ToString("0000");
        killCountText.text = string.Format("<color=#ff0000>{0}</color>/{1}", killCount+1, remainZombie);

        // 죽인 횟수를 저장
        //PlayerPrefs.SetInt("KILL_COUNT", killCount);
        if(killCount == remainZombie)
        {
            Mission_Manager.gameOver = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
