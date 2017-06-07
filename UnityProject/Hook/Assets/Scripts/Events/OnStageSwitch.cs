using System.Collections;
using System.Collections.Generic;
using MGS.EventManager;
using UnityEngine;

public class OnStageSwitch : IEvent {

    private readonly int increment;

    public OnStageSwitch(int increment) {
        this.increment = increment;
    }

    public int Increment {
        get { return this.increment; }
    }

}
