using UnityEngine;
using System.Collections;

public class EndingCredit : MonoBehaviour {

    private float moveSpeed = 50.0f;

    public new Transform transform;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(0f, moveSpeed, 0f) * Time.deltaTime;
        Debug.Log(movement.y);
        transform.Translate(movement);
	}
}
