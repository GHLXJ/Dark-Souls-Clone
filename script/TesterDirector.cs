using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;//包含PlayableDirector类型的包
using UnityEngine.Timeline;//包含Timeline类型的包

public class TesterDirector : MonoBehaviour
{

    public PlayableDirector pd;

    public Animator attacker;
    public Animator victim;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            if(pd!=null)
            foreach (var track in pd.playableAsset.outputs)//不强转(TimelineAsset)，(TimelineAsset)不能迭代
            {
                //print(track.streamName);
                if(track.streamName == "Attacker Animation")
                {
                    pd.SetGenericBinding(track.sourceObject,attacker);
                }
                else if (track.streamName == "Victim Animation")
                {
                    pd.SetGenericBinding(track.sourceObject, victim);
                }
            }
            
            //pd.time = 0;
            //pd.Stop();
            //pd.Evaluate();//内插法，好像是计算评估出状态
            pd.Play();

        } 
    }
}
