using UnityEngine;
using System.Collections;


// 한 스테이지 당 거리 600
// 1초 당 movespeed만큼 움직임
<<<<<<< HEAD
public class PlayerController : FSMBase {

   /*  공격을 체크하기 위하여  */
    public GameObject attackedObstacle;


    private string playerName;

=======
public class PlayerController : FSMBase
{
>>>>>>> origin/master
    private float characterAltitude;
    private float jumpSpeed;
    private float maxSpeed = 19f;
    private float characterMass = 14f;
    private int numAttack = 0;

    //public string str;
    private float _elapsedTime;
    private float skillTime = 4.0f;
    //private float stopWaitTime = 1.0f;

    private float timer;

    //private int _layermask;

    public float distance;
    public float max_distance = 30;

    private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열
    private GameObject obsPoint_mid;
    private PlayerController _player;
    public BackGroundMove bgm;

    private bool skillFlag;

    public UIInterface ourInterface;

    protected override void Awake() {
        base.Awake();

<<<<<<< HEAD
        //Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기

        max_distance = 300;
=======
        max_distance = 30;

>>>>>>> origin/master
        distance = max_distance;
        skillFlag = false;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bgm = GameObject.FindGameObjectWithTag("BackgroundRoot").GetComponent<BackGroundMove>();
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기
        characterAltitude = transform.position.y;
        ourInterface = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().GetComponent<UIInterface>();
<<<<<<< HEAD

=======
        obsPoint_mid = GameObject.FindGameObjectWithTag("obstaclePos3");
>>>>>>> origin/master
    }
    
    // 장애물 이랑 충돌하면 이벤트 함수 발생, other값은 player와 충돌한 객체(장애물)
    void OnTriggerEnter2D(Collider2D other) {
        if (skillFlag)  // 스킬을 사용했을 때
        {
            other.gameObject.SetActive(false);

<<<<<<< HEAD
        } else {
=======
        } /*  other.gameObject.SetActive(false);
            //other.gameObject.transform.Translate(+10.0f, 0, 0);
            print("Attack and damage");

        }*/ else {
>>>>>>> origin/master

            print("origin crush state : " + state);
            timer = 0;
            bgm.moveFlag = false;
<<<<<<< HEAD
            attackCheck = 0;

        }/*  other.gameObject.SetActive(false);
            //other.gameObject.transform.Translate(+10.0f, 0, 0);
            print("Attack and damage");

        } */

    }


    /*  공격   */
    public void checkAttackOrCrush() {

        if (state == State.Attack) {

            attackedObstacle.SetActive(false);

        }
        
=======
            numAttack = 0;
        }
        //print(other.gameObject.name);
        //Destroy(other.gameObject);
>>>>>>> origin/master
    }
    

<<<<<<< HEAD
    public float GetSkillGaugeValue() {
        if (attackCheck > 5)
        {
            attackCheck = 0;
        }
=======
    public float GetSkillGaugeValue()
    {
        if (numAttack > 5)
            numAttack = 0;
>>>>>>> origin/master

        if (numAttack == 0)
            return 0.0f;
        else
            return numAttack / 5.0f;
    }

    protected override void Update()
    {
        base.Update();
<<<<<<< HEAD
        
=======

        // 스킬이면
        print("state"+state);
>>>>>>> origin/master

        if (distance <= 0 && distance > -1000)
        {
            ourInterface.gameClear();
            distance = -1000;
        }

        // 여기서 충돌시에는 감소하지 않도록 만들어야...
        distance -= bgm.moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (!skillFlag && timer >= 1 && !bgm.moveFlag)    // 배경 정지 1초만 하고 움직이기
        {
            bgm.moveFlag = true;
<<<<<<< HEAD
        } else if (skillFlag && timer >= skillTime) {   // 스킬 끝

=======
        }
        else if (skillFlag && timer >= skillTime)
        {
>>>>>>> origin/master
            bgm.moveSpeed = bgm.fixedMoveSpeed;
            skillFlag = false;
            SetState(State.Run);
        }
<<<<<<< HEAD
        

        //ProcessInput();
=======

        if (state == State.Dead)
            return;
>>>>>>> origin/master
    }

    public void DoSkill()
    {
        skillFlag = true;
        numAttack = 0;
        timer = 0;
        bgm.moveFlag = true;
        bgm.moveSpeed = bgm.fixedMoveSpeed * 2;
        
        if (state == State.Run)
        {
            _elapsedTime = 0.0f;
            SetState(State.Skill);
        }
<<<<<<< HEAD
        
=======
>>>>>>> origin/master
    }

    public void DoAttack()
    {
        print("Do attack");
        if (state == State.Run)
        {
            print("state (Run) is : "+ state);
            SetState(State.Attack);
            print("state (Attack) is : " + state);
        }
    }

    public void DoJump2()
    {
        if (state == State.Jump)
        {
            jumpSpeed = maxSpeed -2f;
            SetState(State.Jump2);
        }
    }

    public void DoJump()
    {
        if (state == State.Run)
        {
            jumpSpeed = maxSpeed;
            SetState(State.Jump);
        }
    }

    protected virtual void Run()
    {
    }

    protected virtual void Attack()
    {
<<<<<<< HEAD
        
        print("Attack function work");
=======
        print("Attack function work");
        GameObject target = null;
        float minDist = 1000.0f;
        float dist;
>>>>>>> origin/master
        

        //_obstacle.Damage();
        /*
        foreach (GameObject obstacle in Obstacles)
        {
            dist = obstacle.transform.position.x - _player.transform.position.x;
            if (dist > 0 && dist < minDist)
            {
                minDist = dist;
                target = obstacle;
            }
<<<<<<< HEAD
        }*/
        
=======
        }

        if (minDist < 3.0f && target != null)
        {
            print("Attack and damage");
            target.gameObject.SetActive(false);
            //obstacle.transform.position = new Vector3 ()
            numAttack += 1;
            if (numAttack > 5)
            {
                numAttack = 5;
            }
            print(numAttack);
        }
>>>>>>> origin/master
        SetState(State.Run);
    }

    protected virtual void Jump()
    {
        jumpSpeed += characterMass * Physics.gravity.y * Time.deltaTime * 1/2;
        Vector3 movement = new Vector3(0f, jumpSpeed * Time.deltaTime);
        transform.Translate(movement);

        if (transform.position.y <= characterAltitude)
        {
            transform.position = new Vector3(transform.position.x, characterAltitude);
            SetState(State.Run);
        }
    }

    protected virtual void Jump2()
    {
        jumpSpeed += characterMass * Physics.gravity.y * Time.deltaTime * 1/2;
        Vector3 movement = new Vector3(0f, jumpSpeed * Time.deltaTime);
        transform.Translate(movement);

        if (transform.position.y <= characterAltitude)
        {
            transform.position = new Vector3(transform.position.x, characterAltitude);
            SetState(State.Run);
        }
    }

    protected virtual void Dead()
    {

    }

    protected virtual void Skill()
    {
        
    }
}
