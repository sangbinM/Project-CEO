using UnityEngine;
using System.Collections;


// 한 스테이지 당 거리 600
// 1초 당 movespeed만큼 움직임
public class PlayerController : FSMBase {

   /*  공격을 체크하기 위하여  */
    public GameObject attackedObstacle;


    private string playerName;

    private float characterAltitude;
    private float jumpSpeed;
    private float maxSpeed = 19f;
    private float characterMass = 14f;
    private int attackCheck = 0;

    public string str;
    private float _elapsedTime;
    private float skillTime = 4.0f;
    private float stopWaitTime = 1.0f;

    private float timer;

    private int _layermask;

    public float distance;
    public float max_distance;

    //private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열

    private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열
    private PlayerController _player;
    public BackGroundMove bgm;

    // 장애물
    private bool skillFlag;

    public Canvas ourCanvas;
    public UIInterface ourInterface;

    protected override void Awake() {
        base.Awake();

        //Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기

        max_distance = 300;
        distance = max_distance;
        skillFlag = false;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bgm = GameObject.FindGameObjectWithTag("BackgroundRoot").GetComponent<BackGroundMove>();
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기
        characterAltitude = transform.position.y;
        SetName("Team9");
        ourInterface = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().GetComponent<UIInterface>();

    }
    
    // 장애물 이랑 충돌하면 이벤트 함수 발생, other값은 player와 충돌한 객체(장애물)
    void OnTriggerEnter2D(Collider2D other) {
        if (skillFlag)  // 스킬을 사용했을 때
        {
            other.gameObject.SetActive(false);

        } else {

            print("origin crush state : " + state);
            timer = 0;
            bgm.moveFlag = false;
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
        
    }
    

    public float GetSkillGaugeValue() {
        if (attackCheck > 5)
        {
            attackCheck = 0;
        }

        if (attackCheck == 0)
            return 0.0f;
        else
            return attackCheck / 5.0f;
    }

    protected override void Update()
    {
        base.Update();
        

        if (distance <= 0 && distance != -1000)
        {
            //ourCanvas.GetComponent<UIInterface>();
            ourInterface.gameClear();
            distance = -1000;
            print("Clear");
        }

        distance -= bgm.moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (!skillFlag && timer >= 1 && !bgm.moveFlag)    // 배경 정지 1초만 하고 움직이기
        {
            bgm.moveFlag = true;
        } else if (skillFlag && timer >= skillTime) {   // 스킬 끝

            bgm.moveSpeed = bgm.fixedMoveSpeed;
            skillFlag = false;
            SetState(State.Run);
        }
        

        //ProcessInput();
    }

    public void DoSkill()
    {
        skillFlag = true;
        attackCheck = 0;
        timer = 0;
        bgm.moveFlag = true;
        bgm.moveSpeed = bgm.fixedMoveSpeed * 2;
        
        if (state == State.Run)
        {
            _elapsedTime = 0.0f;
            SetState(State.Skill);
        }
        
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
        
        print("Attack function work");
        

        //_obstacle.Damage();
        /*
        foreach (GameObject obstacle in Obstacles)
        {
            if ((obstacle.transform.position.x - _player.transform.position.x) < 3.0f)
            {
                print("Attack and damage");
                obstacle.gameObject.SetActive(false);
                //obstacle.transform.position = new Vector3 ()
                attackCheck += 1;
                if (attackCheck > 5)
                {
                    attackCheck = 5;
                }
                print(attackCheck);
            }
        }*/
        
        SetState(State.Run);
    }

    protected virtual void Jump()
    {


        jumpSpeed += characterMass * Physics.gravity.y * Time.deltaTime * 1/2;
        Vector3 movement = new Vector3(0f, jumpSpeed * Time.deltaTime);
        //Debug.Log(jumpSpeed * Time.deltaTime);
        //_cc.Move(movement);
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
        //Debug.Log(jumpSpeed * Time.deltaTime);
        //_cc.Move(movement);
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
        //_obstacle.DoSkill(skillTime);

    }

    public void SetName(string name)
    {
        playerName = name;
    }

    public string GetName()
    {
        return playerName;
    }
}
