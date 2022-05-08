using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firepointController : MonoBehaviour
{
    public bool touchingWall;
    // Start is called before the first frame update
    void Start()
    {
        touchingWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Handles collisions between firepoint and other GameObjects
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            Debug.Log("Touching wall");
            touchingWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Deezys
        if(other.tag == "Ground")
        {
            Debug.Log("Stopped touching wall");
            touchingWall = false;
        }
    }
}
