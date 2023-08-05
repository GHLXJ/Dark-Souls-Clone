using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;//包含PlayableDirector
using UnityEngine.Timeline;//包含TimelineAsset
[RequireComponent(typeof(PlayableDirector))]//程序运行时自动添加一个PlayableDirector
public class DirectorManager : IAcyorManagerInterface
{
    public PlayableDirector pd;
    [Header("===Timeline assets===")]
    public TimelineAsset frontStab;

    [Header("=== Assets Settings ===")]
    public ActorManager attacker;
    public ActorManager victim;
    // Start is called before the first frame update
    void Start()
    {
        pd = GetComponent<PlayableDirector>();
        pd.playOnAwake = false;
        //pd.playableAsset = frontStab;
        //pd.playableAsset = Instantiate(frontStab);
        //foreach (var track in pd.playableAsset.outputs)//不强转(TimelineAsset)，(TimelineAsset)不能迭代
        //{
        //    //print(track.streamName);
        //    if (track.streamName == "Attacker Script")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, attacker);
        //    }
        //    else if (track.streamName == "Victim Script")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, victim);
        //    }
        //    else if (track.streamName == "Attacker Animation")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, attacker.ac.anim);
        //    }
        //    else if (track.streamName == "Victim Animation")
        //    {
        //        pd.SetGenericBinding(track.sourceObject, victim.ac.anim);
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q) && gameObject.layer == LayerMask.NameToLayer("Player") )
        //{
        //    pd.Play();
            
        //}
    }
    public void PlayFrontStab(string timelineName,ActorManager attacker,ActorManager victim)
    {
        //Debug.Log("PlayFrontStab" + attacker.name + "&&" + victim.name);
        /*if (pd.playableAsset != null)
        {
            return;
        }*/
        //Debug.Log("PlayFrontStab" + attacker.name +"&&" + victim.name);
        if(timelineName == "frontStab")
        {
            pd.playableAsset = Instantiate(frontStab);
            //if (pd != null) Debug.Log(pd.name);
            TimelineAsset timeline = (TimelineAsset)pd.playableAsset;//拿剧本

            


            foreach (var track in timeline.GetOutputTracks())
            {
                if (track.name == "Attacker Script")
                {
                    pd.SetGenericBinding(track, attacker);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        pd.SetReferenceValue(myclip.am.exposedName, attacker);
                    }
                }
                else if (track.name == "Victim Script")
                {
                    pd.SetGenericBinding(track, victim);
                    foreach (var clip in track.GetClips())
                    {
                        MySuperPlayableClip myclip = (MySuperPlayableClip)clip.asset;
                        MySuperPlayableBehaviour mybehav = myclip.template;
                        pd.SetReferenceValue(myclip.am.exposedName, victim);
                    }
                }
                else if (track.name == "Attacker Animation")
                {
                    pd.SetGenericBinding(track, attacker.ac.anim);
                }
                else if (track.name == "Victim Animation")
                {
                    pd.SetGenericBinding(track, victim.ac.anim);
                }
            }

            //foreach (var trackBinding in pd.playableAsset.outputs)//不强转(TimelineAsset)，(TimelineAsset)不能迭代
            //{
            //    //print(track.streamName);
            //    if (trackBinding.streamName == "Attacker Script")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, attacker);
            //    }
            //    else if (trackBinding.streamName == "Victim Script")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, victim);
            //    }
            //    else if (trackBinding.streamName == "Attacker Animation")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, attacker.ac.anim);
            //    }
            //    else if (trackBinding.streamName == "Victim Animation")
            //    {
            //        pd.SetGenericBinding(trackBinding.sourceObject, victim.ac.anim);
            //    }
            //}
            pd.Evaluate();
            pd.Play();
        }
    }
}
