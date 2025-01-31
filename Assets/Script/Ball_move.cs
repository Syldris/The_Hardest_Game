using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ball_move : MonoBehaviour
{
    Vector2 endPos;
    Vector2 startPos;
    Vector2 Temp;
    public float curTime;
    
    public float lerpTime;

    public float speed;
    private void Start()
    {
        speed = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().MovementSpeed;
        startPos = transform.position;
        endPos = transform.GetChild(0).gameObject.transform.position;
        lerpTime = (startPos - endPos).magnitude * 1/speed;
    }

    void FixedUpdate()
    {
        curTime += Time.deltaTime;

        if (curTime >= lerpTime)
        {
            Temp = startPos;
            startPos = endPos;
            endPos = Temp;
            curTime = 0;
        }

        transform.position = Vector3.Lerp(startPos, endPos, curTime / lerpTime);
    }
}
