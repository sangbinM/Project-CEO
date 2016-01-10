using UnityEngine;
using System.Collections;

public class BackGroundMove : MonoBehaviour {

    private float moveSpeed = 3.0f;
    
    private Transform[] background;
    private int flag;   // 화면 체인지 해줘야 하는 값
    public float xSize; // Scene에서 Setting 해줘야 하는 값

    // Use this for initialization
    void Awake()
    {
        flag = 0;
        
        GameObject[] gameobjects = GameObject.FindGameObjectsWithTag("Background");

        background = new Transform[gameobjects.Length];
        

        background[0] = gameobjects[0].GetComponent<Transform>();
        background[0].position = new Vector3(0, 0, background[0].position.z);
        background[1] = gameobjects[1].GetComponent<Transform>();
        background[1].position = new Vector3(xSize, 0, background[1].position.z);


    }
    // Update is called once per frame
    void Update () {
        
        
        Vector3 movement = new Vector3(-moveSpeed, 0f, 0f) * Time.deltaTime;
        background[0].Translate(movement);
        background[1].Translate(movement);

        if (background[0].position.x <= 0 && flag == 1) {
            background[1].position = new Vector3(xSize, 0, background[1].position.z);
            flag = 0;
        } else if (background[1].position.x <= 0 && flag == 0){
            background[0].position = new Vector3(xSize, 0, background[0].position.z);
            flag = 1;
        }


    }
}
