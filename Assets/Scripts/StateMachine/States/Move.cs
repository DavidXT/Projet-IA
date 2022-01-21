using UnityEngine;
using UnityEngine.AI;

public class Move : AState
{
    private NavMeshAgent m_Agent;

    private Transform m_AITransform;
    private Vector3 m_PositionToGo;
    
    public Move(GameObject ai) : base(ai)
    {
        m_Agent = _ai.GetComponent<NavMeshAgent>();
        m_AITransform = _ai.transform;
    }

    public override void BeginState()
    {
        m_Agent.isStopped = false;
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(m_AITransform.position, m_PositionToGo) >= 10)
        {
            m_Agent.SetDestination(m_PositionToGo);
        }
        else
        {
            NavMeshHit navMeshHit;
            NavMeshPath path = new NavMeshPath();
            
            navMeshHit = GetRandomPointOnNavmesh();
            m_Agent.CalculatePath(navMeshHit.position, path);
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                m_PositionToGo = navMeshHit.position;
                m_Agent.SetDestination(navMeshHit.position);
            }
        }
        
    }

    public override void EndState()
    {
        m_Agent.isStopped = true;
    }

    private NavMeshHit GetRandomPointOnNavmesh()
    {
        NavMeshHit navMeshHit;
        
        bool foundPosition = NavMesh.SamplePosition(
            m_AITransform.position + Random.insideUnitSphere * 150.0f,
            out navMeshHit,
            150.0f,
            NavMesh.AllAreas
        );
        
        return navMeshHit;
    }
}
