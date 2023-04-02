using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public int x;
    [Range(1,20)]
    public int y;
    public float maxY = 7.01f;
    public float minY = 3.03f;
    public string node = "f";
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.y != minY)
            transform.position = new Vector3(transform.position.x, minY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(node))
        //{
        //    if (transform.position.y < maxY)
        //        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        //}
        //else if (transform.position.y > minY )
        //    transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
    }

    public void reallmove()
    {
        print("reallmove is play");
        while(transform.position.y < maxY)
        if (/*Input.GetKey(node)*/true)
        {
            if (transform.position.y < maxY)
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        }
        //else if (transform.position.y > minY)
        //    transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
    }
}
