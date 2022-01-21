using UnityEngine;

public class Init : AState
{
    public Init(GameObject ai) : base (ai)
    {

    }

    public override void BeginState()
    {
        //start unity
    }

    public override void EndState()
    {
        //Ondestroy
    }

    public override void UpdateState()
    {
        //update unity
    }
}
