using UnityEngine;
using System.Collections;

public class BackGroundMove : MonoBehaviour {

    //private float moveSpeed = 50.0f;

    public new Transform transform;
    private Transform[] background;


    // Use this for initialization
    void Start()
    {
        transform = GetComponent<Transform>();
       // background = GameObject.FindGameObjectsWithTag("Background")
    }
    // Update is called once per frame
    void Update () {
        

        //Vector3 movement = new Vector3(0f, moveSpeed, 0f) * Time.deltaTime;
        //Debug.Log(movement.y);
        //transform.Translate(movement);
    }
}
