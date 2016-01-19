using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class ObstacleController_Level3 : MonoBehaviour
{
    public float moveSpeed = 2f;

    private GameObject[] Obstacles;
    private GameObject[] obsPoints;
    private float init_time;
    private GameObject npc;
    public Sprite[] npcSpeech;

    private int setActiveUpObject_flag;
    //private GameObject[] coupleObstacles;

    void Start()
    {
        init_time = Time.time;
        setActiveUpObject_flag = 0;

        obsPoints = new GameObject[4];
        obsPoints[0] = GameObject.FindGameObjectWithTag("obstaclePos2"); //down
        obsPoints[1] = GameObject.FindGameObjectWithTag("obstaclePos3"); //mid
        obsPoints[2] = GameObject.FindGameObjectWithTag("obstaclePos4"); //up
        obsPoints[3] = GameObject.FindGameObjectWithTag("obstaclePos1");
        npc = GameObject.FindGameObjectWithTag("npc");
        //coupleObstacles = new GameObject[2];
        //coupleObstacles = GameObject.FindGameObjectsWithTag("coupleObs");

        //foreach (GameObject cop in coupleObstacles)
        //{
        //    cop.SetActive(false);
        //}

        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (GameObject obstacle in Obstacles)
        {
            if (obstacle.transform.position.y == obsPoints[2].transform.position.y)
            {
                obstacle.SetActive(false);
            }
            if (obstacle.transform.name == "coupleObs")
                obstacle.SetActive(false);
        }

        npc.SetActive(false);
        setNPCSpeech();
    }


    void Update()
    {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        int num = rand.Next(2);
        int num2 = rand.Next(3);

        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;

        if (isTiming(Time.time, 10.0f)) //40
        {
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
        else if (isTiming(Time.time, 120.0f)) //80
        {
            if (setActiveUpObject_flag == 0)
            {
                npc.SetActive(true);
                npc.transform.Translate(movement / 2);

                obsUpSetActive();
                setActiveUpObject_flag = 1;
            }

            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
                    obstacle.transform.position = obsPoints[num2].transform.position;
                    obstacle.SetActive(true);
                }
            }

            //foreach (GameObject cop in coupleObstacles)
            //{
            //    //Vector3 v =  new Vector3(-7.0f, 0.0f, 0.0f) * Time.deltaTime;

            //    cop.transform.Translate(movement);

            //    if (cop.transform.position.x < obsPoints[3].transform.position.x)
            //    {
            //        cop.transform.position = new Vector3(obsPoints[num2].transform.position.x + 27.0f, 0.0f, 0.0f);
            //        cop.SetActive(true);
            //    }
            //}
        }
        else {
            foreach (GameObject obstacle in Obstacles)
            {
                obstacle.transform.Translate(movement);

                if (obstacle.transform.position.x < obsPoints[3].transform.position.x)
                {
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
            if (obstacle.transform.name == "coupleObs")
            {
                obstacle.SetActive(true);
            }
        }

        //foreach (GameObject cop in coupleObstacles)
        //{
        //    cop.SetActive(true);
        //}

        //Obstacles = GameObject.FindGameObjectsWithTag("obstacle");
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
