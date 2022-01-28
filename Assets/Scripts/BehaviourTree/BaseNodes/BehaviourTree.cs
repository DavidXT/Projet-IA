using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BehaviourTree : MonoBehaviour
{
    [SerializeField] private Blackboard _blackboard;

    [SerializeField] private BTNode _entryNode;
    
    public Blackboard Blackboard => _blackboard;

    private bool bIsRunning = false;
    private Coroutine BTCoroutine = null;

    private void Start()
    {
        _blackboard = ScriptableObject.CreateInstance<Blackboard>();
    }

    public void SetupTree(BTNode entryNode)
    {
        _entryNode = entryNode;
    }

    private void Update()
    {
        if (bIsRunning)
        {
            if(BTCoroutine == null)
                BTCoroutine = StartCoroutine(BehaviourCoroutine());
        }
    }

    public void StartTree()
    {
        bIsRunning = true;
    }

    private IEnumerator BehaviourCoroutine()
    {
        NodeStates state = _entryNode.Evaluate();

        while (state == NodeStates.NOTDEFINED)
        {
            Debug.Log("Test is running");
            yield return null;
        }

        BTCoroutine = null;
    }
}
