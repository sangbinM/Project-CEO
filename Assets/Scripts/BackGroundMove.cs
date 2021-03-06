﻿using UnityEngine;
using System.Collections;

/* 플레이 화면에서 배경 두개가 움직임을 구현한 클래스 */
public class BackGroundMove : MonoBehaviour {

    public float moveSpeed;
    public float fixedMoveSpeed = 5.0f;
    
    // timer distance 지움

    private Transform[] background;
    private int flag;   // 화면 체인지 해줘야 하는 값
    public float xSize; // Scene에서 Setting 해줘야 하는 값
    public bool moveFlag;

    void Awake()
    {
        flag = 0;
        moveFlag = true;
        moveSpeed = fixedMoveSpeed;

        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Background");

        background = new Transform[gameobjects.Length];
        
        background[0] = gameobjects[0].GetComponent<Transform>();
        background[1] = gameobjects[1].GetComponent<Transform>();
    }

    void Update ()
    {
        if (moveFlag == true)
            MoveBackground();
    }

    void MoveBackground()
    {
        Vector3 movement = new Vector3(-moveSpeed, 0f, 0f) * Time.deltaTime;
        background[0].Translate(movement);
        background[1].Translate(movement);

        if (background[0].position.x <= 0 && flag == 0)
        {
            background[1].position = new Vector3(xSize, 0, background[1].position.z);
            flag = 1;
        }
        else if (background[1].position.x <= 0 && flag == 1)
        {
            background[0].position = new Vector3(xSize, 0, background[0].position.z);
            flag = 0;
        }
    }
}
