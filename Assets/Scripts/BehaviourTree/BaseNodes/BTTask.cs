using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Complete
{
    public abstract class BTTask : BTNode
    {
        protected Blackboard Blackboard;
        public override void InitNode(Blackboard blackboard)
        {
            Blackboard = blackboard;
        }
    }
}
