﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Update2 : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (TimeOver_Manager.gameOver)
        {
            //animator.SetBool("IsGameOver", true);
            animator.SetBool("IsTimeOver", true);
        }
    }
}