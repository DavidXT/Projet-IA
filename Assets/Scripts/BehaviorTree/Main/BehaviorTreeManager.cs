using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeManager : MonoBehaviour
{
    BehaviorTree tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaviorTree>();

        var FoundTarget = ScriptableObject.CreateInstance<FoundTargetNode>();
        FoundTarget.Tank = this.gameObject;

        var MoveTo = ScriptableObject.CreateInstance<MoveNode>();
        MoveTo.Tank = this.gameObject;

        var sequence = ScriptableObject.CreateInstance<Sequencer>();
        sequence.children.Add(FoundTarget);
        sequence.children.Add(MoveTo);

        var loop = ScriptableObject.CreateInstance<RepeatNode>();
        loop.child = sequence;

        tree.root = loop;
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
