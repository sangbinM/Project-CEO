using UnityEngine;
using System.Collections;
using UnityEditor.SceneManagement;


// 한 스테이지 당 거리 600
// 1초 당 movespeed만큼 움직임
public class PlayerController : FSMBase {

    private string playerName;

    private float characterAltitude;
    private float jumpSpeed;
    private float maxSpeed = 19f;
    private float characterMass = 14f;

    private float _elapsedTime;
    private float skillTime = 4.0f;
    private float stopWaitTime = 1.0f;

    private float timer;

    private int _layermask;

    public float distance;
    public float max_distance;
    public string sceneToLoad;

    //private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열

    private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열
    private ObstacleController _obstacle;
    public BackGroundMove bgm;

    // 장애물
    private bool skillFlag;
    

    protected override void Awake()
    {
        base.Awake();

        //Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기

        max_distance = 60;
        distance = max_distance;
        skillFlag = false;
        _obstacle = GameObject.FindGameObjectWithTag("obstacle").GetComponent<ObstacleController>();
        bgm = GameObject.FindGameObjectWithTag("BackgroundRoot").GetComponent<BackGroundMove>();
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기
        characterAltitude = transform.position.y;
        SetName("Team9");
    }
    
    // 장애물 이랑 충돌하면 이벤트 함수 발생, other값은 player와 충돌한 객체(장애물)
    void OnTriggerEnter2D(Collider2D other)
    {
        // 공격해서 장애물이 없어지는 것인지 아니면 장애물이랑 부딪힌 건지
        // 공격해서 없어지는 거는 공격 애니메이션 이벤트에서 처리해주고 거기서 collision 체크를 해주면 된다
        // 플레이어 움직임 정지 + 거리 bar 정지 + 배경 정지 + 장애물 정지 해야됨
        print("Collision");
        if (skillFlag)  // 스킬을 사용했을 때
        {
            other.gameObject.SetActive(false);

        } else {
            /*
            if (state == State.Attack)
            {
                print("Attack and damage");
                other.gameObject.SetActive(false);
                SetState(State.Run);
            }
            */
            timer = 0;
            bgm.moveFlag = false;

        }

        //print(other.gameObject.name);
        //Destroy(other.gameObject);
    }


    void gameClear() {


       // SceneManager.LoadScene(sceneToLoad);


    }

    protected override void Update()
    {
        base.Update();

        // 스킬이면

        //Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;
        //foreach (GameObject obstacle in Obstacles)  
        //{

        //    obstacle.transform.Translate(movement);
        //}

        if (distance <= 0)
        {
            gameClear();
            //print("Game End");

        }

        distance -= bgm.moveSpeed * Time.deltaTime;
        timer += Time.deltaTime;

        if (!skillFlag && timer >= 1 && !bgm.moveFlag)    // 배경 정지 1초만 하고 움직이기
        {
            bgm.moveFlag = true;
        } else if (skillFlag && timer >= skillTime) {

            bgm.moveSpeed = bgm.fixedMoveSpeed;

        }

        if (state == State.Dead)
            return;

        //ProcessInput();
    }

    /*
    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (state == State.Run)
            {
                jumpSpeed = maxSpeed;
                SetState(State.Jump);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetState(State.Attack);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetState(State.Skill);
        }
    }
    */

    public void DoSkill()
    {
        skillFlag = true;
        timer = 0;
        bgm.moveFlag = true;
        bgm.moveSpeed = bgm.fixedMoveSpeed * 2;
        /*
        if (state == State.Run)
        {
            _elapsedTime = 0.0f;
            SetState(State.Skill);
        }
        */
    }

    public void DoAttack()
    {
        if (state == State.Run)
        {
            SetState(State.Attack);
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
        /*
        if (Vector3.Distance(transform.position, _obstacle.transform.position) <= 2f)
        {
            //_obstacle.Damage();
        }
        */
        //_obstacle.Damage();
        SetState(State.Run);
    }

    protected virtual void Jump()
    {

        Debug.Log("Jump");
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
