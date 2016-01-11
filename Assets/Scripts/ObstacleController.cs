using UnityEngine;
using System.Collections;

public class ObstacleController : FSMBase {
    
    public enum ObState
    {
        Run, Dead
    }
    
    public float moveSpeed = 2f;
    public float turnSpeed = 360f;

    public float attack = 5f;
    public float attackRange = 2f;
    public int currentHP = 30;

    private int _maxHP;

    public float waitTime = 1f;

    public float deadTime = 3f;

    public Transform waypointRoot;

    //protected Transform[] _waypoints;
    protected Camera _sight;
    protected PlayerController _player;

    private float _elapsedTime = 0f;
    private Transform _currentWayPoint;

    //    void Awake()
    //    {
    //        _obstacle = GetComponent<ObstacleController>();
    //    }
}
