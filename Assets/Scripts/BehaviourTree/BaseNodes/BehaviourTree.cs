namespace Complete
{
    using System.Collections;
    using UnityEngine;

    [CreateAssetMenu(fileName = "BehaviourTree", menuName = "BehaviourTree/Tree")]
    public class BehaviourTree : ScriptableObject
    {
        private Blackboard _blackboard;

        [SerializeField] private BTNode _entryNode;
        
        public Blackboard Blackboard => _blackboard;

        private bool bIsRunning = false;
        public bool IsRunning => bIsRunning;
        
        public void SetupTree(BTNode entryNode)
        {
            _blackboard = ScriptableObject.CreateInstance<Blackboard>();
            _entryNode = entryNode;
            bIsRunning = true;
        }
        
        public void EvaluateTree()
        {
            _entryNode.Evaluate();
        }
    }
}
