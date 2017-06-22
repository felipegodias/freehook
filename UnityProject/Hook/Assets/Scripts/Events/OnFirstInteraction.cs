using MGS.EventManager;

public class OnFirstInteraction : IEvent {

    private readonly FirstInteraction firstInteraction;

    public OnFirstInteraction(FirstInteraction firstInteraction) {
        this.firstInteraction = firstInteraction;
    }

    public FirstInteraction FirstInteraction {
        get { return this.firstInteraction; }
    }

}