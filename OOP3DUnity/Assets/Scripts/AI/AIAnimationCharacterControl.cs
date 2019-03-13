using Geekbrains;
using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdAnimationPersonCharacter))]
    public class AIAnimationCharacterControl : MonoBehaviour, ISetTarget
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdAnimationPersonCharacter character { get; private set; } // the character we are controlling
        //public Vector3 target;                                    // target to aim for
        public Transform target;                                    // target to aim for



        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdAnimationPersonCharacter>();

            //agent.updateRotation = false;
            //agent.updatePosition = true;
            agent.updateRotation = true;
            //agent.updatePosition = false;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    //character.Move(agent.desiredVelocity, false, false);
                    //character.(agent.desiredVelocity, false, false);
                    character.MoveAnimation();
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                }

                if (Vector3.Distance(transform.position, target.position) < agent.stoppingDistance)//target.position - transform.position > agent.stoppingDistance)
                {
                    target.GetComponent<IDestination>().DestroySelfAfterCome(); // ������
                    //Debug.Log(name);
                }
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}