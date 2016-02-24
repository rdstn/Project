using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lockup : BehaviourTree
{

    float time;
    string receptionistKey;
    string receptionKey = "reception";

    public Lockup(EventManager eventManager, float time, GameObject room) : base(eventManager)
    {
        this.time = time;

        AddToBlackboard("room", room);
    }

    public override void constructTree()
    {
        base.root = new Root(this, new Node[] {
            new Sequence(new Node[] {
                new SetRandomRoom(this, "destination", "room"),
                new TravelTo(this, "destination"),
                new DisplaySentence(this, "shock", 40),
                new Wait(this, time),
            })
        });
    }
}
