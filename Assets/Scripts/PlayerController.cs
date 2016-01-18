using UnityEngine;
using System.Collections;


// 한 스테이지 당 거리 600
// 1초 당 movespeed만큼 움직임
public class PlayerController : FSMBase
{
    private float characterAltitude;
    private float jumpSpeed;
    private float maxSpeed = 19f;
    private float characterMass = 14f;
    private int numAttack = 0;
    private int numAttack_flag = 0;
    
    private float _elapsedTime;
    private float skillTime = 4.0f;

    private float timer;

    //private int _layermask;

    public float distance;
    public float max_distance;

    private GameObject[] Obstacles; // Obstacle 받아오기 위한 변수 배열
    private GameObject collidedObstacle;
    private GameObject obsPos_mid;
    private PlayerController _player;
    public BackGroundMove bgm;

    private bool skillFlag;

    public UIInterface ourInterface;

    protected override void Awake()
    {
        base.Awake();
        max_distance = 300;
        distance = max_distance;
        skillFlag = false;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bgm = GameObject.FindGameObjectWithTag("BackgroundRoot").GetComponent<BackGroundMove>();
        Obstacles = GameObject.FindGameObjectsWithTag("obstacle");  // 장애물 모두 받아오기
        characterAltitude = transform.position.y;
        ourInterface = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().GetComponent<UIInterface>();
        obsPos_mid = GameObject.FindGameObjectWithTag("obstaclePos3");
    }

    // 장애물 이랑 충돌하면 이벤트 함수 발생, other값은 player와 충돌한 객체(장애물)
    void OnTriggerEnter2D(Collider2D other)
    {
        // 공격해서 장애물이 없어지는 것인지 아니면 장애물이랑 부딪힌 건지
        // 공격해서 없어지는 거는 공격 애니메이션 이벤트에서 처리해주고 거기서 collision 체크를 해주면 된다
        // 플레이어 움직임 정지 + 거리 bar 정지 + 배경 정지 + 장애물 정지 해야됨
        collidedObstacle = other.gameObject;

        if (skillFlag)  // 스킬을 사용했을 때
        {
            collidedObstacle.SetActive(false);
            //numAttack_flag = 1; // obstacle active false되면 더이상 numAttack 안올라 가게 체크 
        }
        else
        {
            timer = 0;
            bgm.moveFlag = false;

            numAttack = 0;
        }
    }

    public float GetSkillGaugeValue()
    {
        if (numAttack > 5)
            numAttack = 0;

        if (numAttack == 0)
            return 0.0f;
        else
            return numAttack / 5.0f;
    }

    protected override void Update()
    {
        base.Update();

        // 스킬이면

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
        }
        else if (skillFlag && timer >= skillTime)
        {
            bgm.moveSpeed = bgm.fixedMoveSpeed;
            skillFlag = false;
            SetState(State.Run);
        }

        if (collidedObstacle != null && collidedObstacle.transform.position.x < _player.transform.position.x)
            collidedObstacle = null;

        if (state == State.Dead)
            return;
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
            jumpSpeed = maxSpeed - 2f;
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
        GameObject target = null;
        float minDist = 1000.0f;
        float dist;

        foreach (GameObject obstacle in Obstacles)
        {
            dist = obstacle.transform.position.x - _player.transform.position.x;
            if (dist > 0 && dist < minDist)
            {
                minDist = dist;
                target = obstacle;
            }
        }

        if (collidedObstacle == target) {

        } else if (minDist < 2.5f && target != null) {
            if (target.transform.position.y == obsPos_mid.transform.position.y && target.activeSelf == true) {

                /*
                if (target.gameObject.name == "obstacle_down" && attack_num == 0) {

                    attack_num++;
                    print("Down!!");
                    target.gameObject.SetActive(false);

                }*/
                
                target.gameObject.SetActive(false);
                if( target.transform.position != null )
                {
                    numAttack += 1;
                }

                if (numAttack > 5) {
                    numAttack = 5;
                }
            }
        }
        else
        {
        }
        SetState(State.Run);
    }

    protected virtual void Jump()
    {
        jumpSpeed += characterMass * Physics.gravity.y * Time.deltaTime * 1 / 2;
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
        jumpSpeed += characterMass * Physics.gravity.y * Time.deltaTime * 1 / 2;
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
