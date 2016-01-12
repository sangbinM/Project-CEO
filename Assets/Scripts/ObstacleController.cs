﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class ObstacleController : FSMBase
{
    public enum ObstacleState
    {
        Idle, Move, Effect
    }

    public float moveSpeed = 2f;
    public float waitTime = 1f;

    protected PlayerController _player;
    protected ObstacleController _obstacle;

    private GameObject[] Obstacles;

    private GameObject obsPoint1;
    private GameObject obsPoint2;
   

    //public ObstacleState state = ObstacleState.Move;

    //[HideInInspector]
    //public new Transform transform;
    //protected Animator _animator;


    protected override void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        obsPoint1 = GameObject.FindGameObjectWithTag("obstaclePos1");
        obsPoint2 = GameObject.FindGameObjectWithTag("obstaclePos2");
    }


    protected override void Update()
    {
        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;

        foreach (GameObject obstacle in Obstacles)
        {
            obstacle.transform.Translate(movement);

            // x 좌표가 -8 보다 작으면 오브젝트 SetActive False
            if (obstacle.transform.position.x < obsPoint1.transform.position.x)
            {
                gameObject.SetActive(false);
                //Invoke("Respawn", 0f);

                //Invoke 역할 random 시간 지연주기
                obstacle.transform.position = obsPoint2.transform.position;
                gameObject.SetActive(true);
            }
        }
    }

    /*void Respawn()
    {
        obstacle.transform.position = obsPoint1.position;
        gameObject.SetActive(true);

        //SetState(State.Idle);
    }*/

    void OnTriggerEnter2D(Collider2D coll)
    {
        //충돌한 오브젝트의 이름이 Player일 경우
        if (coll.transform.name == "Player")
        {
            //Debug.Log("Collision!");
            UnityEngine.Debug.Log(coll.gameObject.name);
            print(gameObject.GetType());

            gameObject.SetActive(false); // 하나만 될 수 있게.

            gameObject.transform.position = obsPoint2.transform.position;
            gameObject.SetActive(true);
        }

    }

    void Damage()  //Player와 일정 range 안에 들어오면 없어지게
    {
        if((state == State.Attack) && 
            ((_player.transform.position.x - gameObject.transform.position.x) < 3f))
        {
            gameObject.SetActive(false);
        }
    }

    void DoSkill(float skillTime)  // 스킬 발동 되면 화면 내 obstacle 다 없어짐
    {
        var watch = Stopwatch.StartNew();

        watch.Stop();

        if(watch.ElapsedMilliseconds < 3000)
        {
            foreach(GameObject obstacle in Obstacles)
            {
                gameObject.SetActive(false);
            }

            watch.Start();
        }
        else
        {
            return;
        }
    }
}
