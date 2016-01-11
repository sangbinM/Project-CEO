using UnityEngine;
using System.Collections;

public class PlayerController : FSMBase {

    private string playerName;

    private float characterAltitude;
    private float jumpSpeed;
    private float maxSpeed = 20f;
    private float characterMass = 10f;

    private float _elapsedTime;
    private float skillTime = 4.0f;

    private int _layermask;

    private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열

    private ObstacleController _obstacle;

    protected override void Awake()
    {
        base.Awake();

        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기
        characterAltitude = transform.position.y;
        SetName("Team9");
    }
    
    // 장애물 이랑 충돌하면 이벤트 함수 발생, other값은 player와 충돌한 객체(장애물)
    void OnCollisionEnter2D(Collision2D other)
    {
        // 공격해서 장애물이 없어지는 것인지 아니면 장애물이랑 부딪힌 건지
        // 공격해서 없어지는 거는 공격 애니메이션 이벤트에서 처리해주고 거기서 collision 체크를 해주면 된다
        // 플레이어 움직임 정지 + 거리 bar 정지 + 배경 정지 + 장애물 정지 해야됨
        //print(other.gameObject.name);
        //Destroy(other.gameObject);
    }

    protected override void Update()
    {
        base.Update();

        // 충돌 체크 위한 예제 코드 나중에 혜림언니가 장애물 추가 하면 삭제해도 됨
        /*
        Vector3 movement = new Vector3(-7.0f, 0f, 0f) * Time.deltaTime;
        foreach (GameObject obstacle in Obstacles)  
        {

            obstacle.transform.Translate(movement);
        }
        */

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
        if (state == State.Run)
        {
            _elapsedTime = 0.0f;
            SetState(State.Skill);
        }
    }

    public void DoAttack()
    {
        if (state == State.Run)
        {
            SetState(State.Attack);
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
        if (Vector3.Distance(transform.position, _obstacle.transform.position) <= 2f)
        {
            //_obstacle.Damage();
        }

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
