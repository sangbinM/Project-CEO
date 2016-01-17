using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class ObstacleController_Level1 : MonoBehaviour
{
    

    public float moveSpeed = 2f;

    private GameObject[] Obstacles;
    private GameObject[] obsPoints;

    void Start()
    {
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        obsPoints = new GameObject[3];
        obsPoints[0] = GameObject.FindGameObjectWithTag("obstaclePos2"); //down
        obsPoints[1] = GameObject.FindGameObjectWithTag("obstaclePos3"); //mid
        obsPoints[2] = GameObject.FindGameObjectWithTag("obstaclePos1");
    }


    void Update()
    {
        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;

        foreach (GameObject obstacle in Obstacles)
        {
            obstacle.transform.Translate(movement);

            // x 좌표가 -8 보다 작으면 오브젝트 SetActive False
            if (obstacle.transform.position.x < obsPoints[2].transform.position.x)
            {
                System.Random rand = new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                int num = rand.Next(2);

                //Invoke 역할 random 시간 지연주기
                obstacle.transform.position = obsPoints[num].transform.position;
                print("obstacle name is " + obstacle.gameObject.name);
                obstacle.SetActive(true);
            }
        }
    }


    
   
}
