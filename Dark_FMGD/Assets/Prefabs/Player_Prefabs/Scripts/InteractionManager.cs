using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionManager : IAcyorManagerInterface
{
    private CapsuleCollider interCol;

    public List<EventCasterManager> overlapEcastms = new List<EventCasterManager>();
    // Start is called before the first frame update
    void Start()
    {
        interCol = GetComponent<CapsuleCollider>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
    void OnTriggerEnter(Collider col)
    {
        //print(col.name);
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        if (ecastms.Length != 0)
        {
            if (col.gameObject.transform.parent.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                am.dm.victim = col.GetComponent<EventCasterManager>().am;
            }
        }
        foreach (var ecastm in ecastms)
        {
            if (!overlapEcastms.Contains(ecastm))
            {
                //Debug.Log(ecastm.eventName + "&&" + overlapEcastms.Count);
                overlapEcastms.Add(ecastm);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        EventCasterManager[] ecastms = col.GetComponents<EventCasterManager>();
        foreach (var ecastm in ecastms)
        {
            if (overlapEcastms.Contains(ecastm))
            {
                overlapEcastms.Remove(ecastm);
            }
        }
    }
}
