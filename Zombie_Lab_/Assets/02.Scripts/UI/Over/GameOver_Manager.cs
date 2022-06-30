using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_Manager : MonoBehaviour
{

    public static bool gameOver;
    private void OnMouseUpAsButton()
    {
        
    }
    public Button btn;

    // Start is called before the first frame update
    void Awake()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) {
           GetComponent<Button>().interactable=true;
           SceneManager.LoadScene("TownScene");
        }

    }
}
