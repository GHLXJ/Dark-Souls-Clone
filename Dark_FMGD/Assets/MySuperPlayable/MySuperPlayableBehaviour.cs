using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class MySuperPlayableBehaviour : PlayableBehaviour
{
    public ActorManager am;
    public float myFloat;
    PlayableDirector pd;
    public override void OnPlayableCreate(Playable playable)
    {

    }
    public override void OnGraphStart(Playable playable) 
    {
       /* pd = (PlayableDirector)playable.GetGraph().GetResolver();
        foreach (var track in pd.playableAsset.outputs) {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(true);
            }
        }*/
    }//Debug.Log (pd);//Debug.Log (track.streamName);//Debug.Log (pd.GetGenericBinding (track.sourceObject));ActorManager am = (ActorManager)pd.GetGenericBinding (track.sourceobject);am.TestEcho ();
    public override void OnGraphStop(Playable playable) 
    {
        /*foreach (var track in pd.playableAsset.outputs)
        {
            if (track.streamName == "Attacker Script" || track.streamName == "Victim Script")
            {
                ActorManager am = (ActorManager)pd.GetGenericBinding(track.sourceObject);
                am.LockUnlockActorController(false);
            }
        }*/
    }
    public override void OnBehaviourPlay(Playable playable, FrameData info) 
    {
        //UnityEngine.Debug.Log ("Benaviour piay...");
        
    }
    public override void OnBehaviourPause(Playable playable, FrameData info) 
    {
        //UnityEngine.Debug.Log("Behaviour pause...");
        if(am!= null)
        {
            am.LockUnlockActorController(false);
            //UnityEngine.Debug.Log(am.name);
        }
    }
    public override void PrepareFrame(Playable playable, FrameData info) 
    {
        //UnityEngine.Debug.Log("Prepare Frame");
        if(am!= null)
        {
            am.LockUnlockActorController(true);
        }
        
    } 
}
