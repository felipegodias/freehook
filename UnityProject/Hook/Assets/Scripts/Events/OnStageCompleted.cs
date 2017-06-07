using System.Collections;
using System.Collections.Generic;
using MGS.EventManager;
using UnityEngine;

public class OnStageCompleted : IEvent {

    private int stage;

    public OnStageCompleted(int stage) {
        this.stage = stage;
    }

    public int Stage {
        get { return this.stage; }
    }
}
