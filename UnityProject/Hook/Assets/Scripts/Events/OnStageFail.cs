using System.Collections;
using System.Collections.Generic;
using MGS.EventManager;
using UnityEngine;

public class OnStageFail : IEvent {

    private int stage;

    public OnStageFail(int stage) {
        this.stage = stage;
    }

    public int Stage {
        get { return this.stage; }
    }
}
