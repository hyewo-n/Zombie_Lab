using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOver_Manager : MonoBehaviour
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
        if (gameOver)
        {
            GetComponent<Button>().interactable = true;
        }

    }
}
