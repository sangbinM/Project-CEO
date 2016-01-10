using UnityEngine;
using System.Collections;

public class PlayerController : FSMBase {

    private string playerName;

    private float characterAltitude;
    private float jumpSpeed;
    private float maxSpeed = 20f;
    private float characterMass = 10f;


    private int _layermask;

    private ObstacleController _obstacle;

    protected override void Awake()
    {
        base.Awake();

        characterAltitude = transform.position.y;
        SetName("Team9");
    }

    protected override void Update()
    {
        base.Update();

        if (state == State.Dead)
            return;

        ProcessInput();
    }

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
        Debug.Log(jumpSpeed * Time.deltaTime);
        _cc.Move(movement);

        if (transform.position.y <= characterAltitude)
        {
            transform.position = new Vector3(transform.position.x, characterAltitude);
            SetState(State.Run);
        }
    }

    protected virtual void Dead()
    {

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
