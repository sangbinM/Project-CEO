﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class ObstacleController_Level2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    
    protected ObstacleController _obstacle;

    private GameObject[] Obstacles;
    private GameObject[] obsPoints;
    private float init_time;
    private PlayerController _player;

    void Start()
    {
        init_time = Time.time;
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        obsPoints = new GameObject[4];
        obsPoints[0] = GameObject.FindGameObjectWithTag("obstaclePos2"); //down
        obsPoints[1] = GameObject.FindGameObjectWithTag("obstaclePos3"); //mid
        obsPoints[2] = GameObject.FindGameObjectWithTag("obstaclePos4"); //up
        obsPoints[3] = GameObject.FindGameObjectWithTag("obstaclePos1");

        foreach (GameObject obstacle in Obstacles)
        {
            if (obstacle.transform.position.y == obsPoints[2].transform.position.y)
                obstacle.SetActive(false);
        }
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _player.level1to4Sound.Play();
    }


    void Update()
    {
        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;

        if (isTiming(Time.time))
        {
            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                // x 좌표가 -8 보다 작으면 오브젝트 SetActive False
                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    int num = rand.Next(2);

                    //Invoke 역할 random 시간 지연주기
                    obstacle.transform.position = obsPoints[num].transform.position;
                    obstacle.SetActive(true);
                }
            }

        }
        else {

            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                // x 좌표가 -8 보다 작으면 오브젝트 SetActive False
                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    int num = rand.Next(3);

                    //Invoke 역할 random 시간 지연주기
                    obstacle.transform.position = obsPoints[num].transform.position;
                    obstacle.SetActive(true);
                }

            }


        }
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

    bool isTiming(float currentTime)
    {
        float elapsedTime = currentTime - init_time;
        if (elapsedTime < 60.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
