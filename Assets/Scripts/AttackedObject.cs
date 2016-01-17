using UnityEngine;
using System.Collections;

public class AttackedObject : MonoBehaviour {


    private PlayerController _player;

    void Awake()
    {

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    // 장애물 이랑 충돌하면 이벤트 함수 발생, other값은 player와 충돌한 객체(장애물)
    void OnTriggerEnter2D(Collider2D other)
    {
        print("AttackedObject");
        print("AttackedObject : " + _player.state);
        _player.attackedObstacle = other.gameObject;
        _player.checkAttackOrCrush();
        
    }


}
