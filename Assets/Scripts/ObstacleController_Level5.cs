using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class ObstacleController_Level5 : MonoBehaviour
{
    public float moveSpeed = 2f;
    
    protected ObstacleController _obstacle;

    private GameObject[] Obstacles;
    private GameObject[] obsPoints;
    private float init_time;
    private GameObject npc;

    void Start()
    {
        init_time = Time.time;
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        obsPoints = new GameObject[4];
        obsPoints[0] = GameObject.FindGameObjectWithTag("obstaclePos2"); //down
        obsPoints[1] = GameObject.FindGameObjectWithTag("obstaclePos3"); //mid
        obsPoints[2] = GameObject.FindGameObjectWithTag("obstaclePos4"); //up
        obsPoints[3] = GameObject.FindGameObjectWithTag("obstaclePos1");
        npc = GameObject.FindGameObjectWithTag("npc");

        npc.SetActive(false);
    }


    void Update()
    {
        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;
        Vector3 movement2 = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime / 2;

        if (isTiming(Time.time, 40.0f))
        {
            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    int num = rand.Next(3);

                    obstacle.transform.position = obsPoints[num].transform.position;
                    obstacle.SetActive(true);
                }
            }
        }
        else if(isTiming(Time.time, 100.0f))
        {
            npc.SetActive(true);
            npc.transform.Translate(movement2);

            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                    int num = rand.Next(3);

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
