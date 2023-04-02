using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openPlace : MonoBehaviour
{
    public move a;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        print(other.name);
        a.SendMessage("reallmove");
    }
}
