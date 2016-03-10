using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lockup : BehaviourTree
{

    float time;

    public Lockup(EventManager eventManager, float time, GameObject room) : base(eventManager)
    {
        this.time = time;
        AddToBlackboard("destination_key", room);
        AddToBlackboard("shock", new Sentence(null, Sentence.Verb.Shocked, null));
    }

    public override void constructTree()
    {

        object[] args = { this.eventManager, this.blackboard};

        base.root = new Root(this, new Node[] {
            new Sequence(new Node[] {
                new SetRandomRoom(this, "destination", "destination_key"),
                new TravelTo(this, "destination"),
                new DisplaySentence(this, "shock", 20),
                new Wait(this, time),
            })
        });
    }
}
