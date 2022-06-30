using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour { 

     //private float h = 0.0f;
     //private float v = 0.0f;

    //private Transform tr;

    // 이동 속도 변수 
    public float moveSpeed = 5.0f;
    Vector3 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
       //tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            float x = Input.GetAxisRaw("Vertical");
            float y = Input.GetAxisRaw("Horizontal");
        
        //h = Input.GetAxis("Horizontal");
        //v = Input.GetAxis("Vertical");
        lookDirection = x * Vector3.forward + y * Vector3.right;

        this.transform.rotation = Quaternion.LookRotation(lookDirection);
            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime); 

            //전후좌우 이동 방향 벡터 계산 
            //Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

            //tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
            //tr.rotation = Quaternion.LookRotation(lookDirection);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
