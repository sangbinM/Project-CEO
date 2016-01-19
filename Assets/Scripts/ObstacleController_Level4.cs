﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class ObstacleController_Level4 : MonoBehaviour
{
    public float moveSpeed = 2f;

    protected ObstacleController _obstacle;

    private GameObject[] Obstacles;
    private GameObject[] obsPoints;
    private float init_time;
    private GameObject npc;
    public Sprite[] npcSpeech;

    private int setActiveUpObject_flag = 0;
    private PlayerController _player;

    void Start()
    {
        init_time = Time.time;

        obsPoints = new GameObject[4];
        obsPoints[0] = GameObject.FindGameObjectWithTag("obstaclePos2"); //down
        obsPoints[1] = GameObject.FindGameObjectWithTag("obstaclePos3"); //mid
        obsPoints[2] = GameObject.FindGameObjectWithTag("obstaclePos4"); //up
        obsPoints[3] = GameObject.FindGameObjectWithTag("obstaclePos1");
        npc = GameObject.FindGameObjectWithTag("npc");
        
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obstacle in Obstacles)
        {
            if (obstacle.transform.position.y == obsPoints[2].transform.position.y)
                obstacle.SetActive(false);
        }

        npc.SetActive(false);
        setNPCSpeech();
        _player.level1to4Sound.Play();
    }

    void Update()
    {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        int num = rand.Next(2);
        int num2 = rand.Next(3);

        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;

        if (isTiming(Time.time, 10.0f))
        {
            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                // x 좌표가 -8 보다 작으면 오브젝트 SetActive False
                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    //Invoke 역할 random 시간 지연주기
                    obstacle.transform.position = obsPoints[num].transform.position;
                    obstacle.SetActive(true);
                }
            }

        }
        else if (isTiming(Time.time, 20.0f))
        {
            if (setActiveUpObject_flag == 0)
            {
                npc.SetActive(true);

                obsUpSetActive();
                setActiveUpObject_flag = 1;
            }

            npc.transform.Translate(movement/2);

            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    obstacle.transform.position = obsPoints[num].transform.position;
                    obstacle.SetActive(true);
                }
            }
        }
        else {
            if (npc.transform.position.x < obsPoints[3].transform.position.x)
            {
                npc.transform.Translate(movement/2);
                npc.SetActive(true);
            }


            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                // x 좌표가 -8 보다 작으면 오브젝트 SetActive False
                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    //Invoke 역할 random 시간 지연주기
                    obstacle.transform.position = obsPoints[num2].transform.position;
                    obstacle.SetActive(true);
                }

            }


        }
    }

    void obsUpSetActive()
    {
        foreach (GameObject obstacle in Obstacles)
        {
            if (obstacle.transform.position.y == obsPoints[2].transform.position.y)
            {
                obstacle.SetActive(true);
            }
        }
    }

    void setNPCSpeech()
    {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        int num = rand.Next(21);
        SpriteRenderer sr = npc.transform.GetChild(0).GetComponent<SpriteRenderer>();
        sr.sprite = npcSpeech[num];
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //충돌한 오브젝트의 이름이 Player일 경우
        if (coll.transform.name == "Player")
        {
            //coll.gameObject.SetActive(false);
            //SetState(State.Dead);
        }

    }

    bool isTiming(float currentTime, float appearTime)
    {
        float elapsedTime = currentTime - init_time;
        if (elapsedTime < appearTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
