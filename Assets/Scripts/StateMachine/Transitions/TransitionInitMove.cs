public class TransitionInitMove : ATransition
{
    public TransitionInitMove(AState nextState) : base(nextState)
    {
    }

    public override bool Check()
    {
        return true;
    }
}
