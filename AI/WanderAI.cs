using UnityEngine;
using UnityEngine.AI;

namespace Believe.Games.Studios
{
    [RequireComponent(typeof(FriendlyHealth),typeof(NavMeshAgent))]
    public class WanderAI : MonoBehaviour
    {
        [Header("Components")]
        NavMeshAgent agent;
        Animator soldierAnimator;
        [Header("Movement Variables")]
        [SerializeField] float patrolRange = 100;
        [SerializeField] float minDelay = 40;
        [SerializeField] float maxDelay = 70;
        [SerializeField] float stopDistance = 4;
        float stateCountdown;
        bool canMove = false;
        Vector3 startPos;
        Vector3 destination;
        internal enum AllyStates
        {
            Idle,
            Wander
        }
        [SerializeField] AllyStates AllyState=AllyStates.Wander;
        
        private void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
            soldierAnimator = GetComponent<Animator>();
            startPos = transform.position;
            GenerateDestination();
            stateCountdown = Random.Range(minDelay, maxDelay);
        }
        private void Update()
        {
            ChangeState();
            ManageState();
        }
        void ChangeState()
        {
            stateCountdown -= Time.deltaTime;
            if(stateCountdown<=0)
            {
                switch(AllyState)
                {
                    case AllyStates.Idle:
                        AllyState = AllyStates.Wander;
                        stateCountdown = Random.Range(minDelay, maxDelay);
                        break;
                    case AllyStates.Wander:
                        AllyState = AllyStates.Idle;
                        stateCountdown = Random.Range(minDelay, maxDelay);
                        break;
                }
            }
        }
        void ManageState()
        {
            switch (AllyState)
            {
                case AllyStates.Idle:
                    Idle();
                    break;
                case AllyStates.Wander:
                    Wander();
                    break;
            }
            
        }
        void Idle()
        {
            agent.isStopped = true;
            soldierAnimator.SetFloat("Motion", 0);
        }
        void Wander()
        {
            if(canMove==false)
            {
                GenerateDestination();
            }

            agent.isStopped = false;
            agent.SetDestination(destination);
            if(agent.remainingDistance<=stopDistance)
            {
                canMove = false;
            }
            if(canMove)
            {
                soldierAnimator.SetFloat("Motion", 1);
            }
            else
            {
                soldierAnimator.SetFloat("Motion", 0);
            }
        }
        private void OnAnimatorMove()
        {
            agent.speed = (soldierAnimator.deltaPosition / Time.deltaTime).magnitude;
        }
        void GenerateDestination()
        {
            destination = new Vector3(Random.Range(-patrolRange, patrolRange),0,Random.Range(-patrolRange, patrolRange))+startPos;
            if(isValid(destination)==false)
            {
                return;
            }
            canMove = true;
        }
        bool isValid(Vector3 positionToCheck)
        {
            NavMeshHit hit;
            return NavMesh.SamplePosition(positionToCheck, out hit, 2, 3);
        }
    }
}
