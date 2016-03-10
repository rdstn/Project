using UnityEngine;
using System.Collections;
using System;

public class Fail : BehaviourTree
{

    float time;

    public Fail(EventManager eventManager, float time, GameObject target) : base(eventManager)
    {
        this.time = time;
        Debug.Log("Well, this doesn't quite work, does it?" + target);
        AddToBlackboard("destination_key", target);
        AddToBlackboard("shock", new Sentence(null, Sentence.Verb.Shocked, null));
    }

    public override void constructTree()
    {
        base.root = new Root(this, new Node[] {
            new Follow(this, "destination_key"),
            new Wait(this,3),
            new DisplaySentence(this, "shock", time),
            new Wait(this, 10),
        });
    }
}
