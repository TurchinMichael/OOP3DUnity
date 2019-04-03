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
        public Vector3 targetVector;                                    // target to aim for
        public Transform target;                                    // target to aim for

       // AudioSource _stepNoiseSource;
        StepAudio _stepAudio;
        GameObject player;
        Vision _vision;



        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdAnimationPersonCharacter>();
            //_stepNoiseSource = GetComponent<AudioSource>();
            _stepAudio = GetComponent<StepAudio>();

            //agent.updateRotation = false;
            //agent.updatePosition = true;
            agent.updateRotation = true;
            agent.updatePosition = true;

            //target = Patrol.GenericPoint(1f);
            //agent.SetDestination(Patrol.GenericPoint(1f));
            createPointDestination();
        }

        void createPointDestination()
        {
            targetVector = Patrol.GenericPoint(1f);
            agent.SetDestination(targetVector);
        }

        public void StepPlay()
        {
            if (/*_stepNoiseSource && */_stepAudio)
            {
                _stepAudio.StepChangeAudio();
                //_stepNoiseSource.Play();
            }
        }

        private void Update()
        {
            if (targetVector != null)
            {
                //Debug.Log(Vector3.Distance(agent.transform.position, Main.Instance.Player.position));

                if (Vector3.Distance(agent.transform.position, Main.Instance.Player.position) <= 8 && 
                    Vector3.Angle(agent.transform.forward, Main.Instance.Player.position - agent.transform.position) <= 35)
                {
                    //Debug.Log("Close");
                    
                        character.AttackAnimation();

                }

                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    //character.Move(agent.desiredVelocity, false, false);
                    //character.(agent.desiredVelocity, false, false);
                    character.MoveAnimation();
                }
                else
                {
                    character.Move(Vector3.zero, false, false);

                    createPointDestination();
                    //Debug.Log("New Point Destination");
                }
            }
             
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
                    target.GetComponent<IDestination>().DestroySelfAfterCome(); // пришел
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