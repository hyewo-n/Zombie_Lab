﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHpBar : MonoBehaviour
{

    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHp;

    [HideInInspector] public Vector3 offsert = Vector3.zero;
    [HideInInspector] public Transform targetTr;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    
    void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offsert);
        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }
        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);
        rectHp.localPosition = localPos;
    }
}
