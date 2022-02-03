namespace Complete
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(fileName = "BehaviourTree", menuName = "BehaviourTree/Tree")]
    public class BehaviourTree : ScriptableObject, ICloneable
    {
        [SerializeField] private Blackboard _blackboard;

        [SerializeField] private BTNode _entryNode;
        
        public Blackboard Blackboard => _blackboard;

        private bool bIsRunning = false;
        public bool IsRunning => bIsRunning;

        public void EvaluateTree()
        {
            _entryNode.Evaluate();
        }

        public object Clone()
        {
            BehaviourTree behaviourTree = CreateInstance<BehaviourTree>();
            behaviourTree._blackboard = (Blackboard)_blackboard.Clone();
            behaviourTree._entryNode = (BTNode)_entryNode.Clone();
            behaviourTree._entryNode.InitNode(behaviourTree._blackboard);
            behaviourTree.bIsRunning = true;
            return behaviourTree;
        }
    }
}
