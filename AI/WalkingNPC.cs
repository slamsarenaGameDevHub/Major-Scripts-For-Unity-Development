using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class WalkingNPC : MonoBehaviour
{
    internal enum Direction
    {
        Backward,
        Forward
    }
    [SerializeField] Direction _direction;

    NavMeshAgent agent;

    Animator animator;
    AudioSource source;


    [SerializeField]AnimatorOverrideController animatorOverride;

    [SerializeField] int activePath,currentWaypoint;
    [SerializeField] float switchDistance = 7;
    [SerializeField] Transform[] Path;
    Transform CurrentPath;
    List<Transform> nodes;


    Vector3 lastPos;
    [HideInInspector]
    public bool isEndangered=false,hasChangedPath=false;

    float Speed,nextTimeToChangePath;
    [SerializeField] float ChangeRate=50,minSpeed=0.9f,maxSpeed=2.5f,agentSpeed=1;
    [SerializeField] float walkAnimationThreshold;


    //Sound
    float nextTimeToPlayStep;
    [SerializeField] float walkRate=0.6f, runRate=0.4f;
    void Start()
    {
        GetCom();
        ChooseWaypoint();
    }
    void GetCom()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animatorOverride;
        source = GetComponent<AudioSource>();
        lastPos=transform.position;
    }
    void Update()
    {
        Speed = Vector3.Distance(transform.position, lastPos) / Time.deltaTime;
        lastPos = transform.position;
        PlayAnimation();
        Move();
        activePath=Random.Range(0,Path.Length);
        if(Time.time>=nextTimeToChangePath)
        {
            nextTimeToChangePath=Time.time+ChangeRate;
            ChooseWaypoint();
        }
        
    }
    void ChooseWaypoint()
    {
        for(int i=0;i<Path.Length;i++)
        {
            if(i==activePath)
            {
                CurrentPath=Path[i];
            }
        }
        GetNodes();
    }
    void GetNodes()
    {
        Transform[] waypoints=CurrentPath.GetComponentsInChildren<Transform>();
        nodes=new List<Transform>();
        for(int i=0;i<waypoints.Length;i++)
        {
            if(waypoints[i]!=CurrentPath)
            {
                nodes.Add(waypoints[i]);
            }
        }
        currentWaypoint=Random.Range(0,nodes.Count);
    }
    void Move()
    {
        if(nodes.Count<=0)return;
        if(isEndangered)
        {
            agentSpeed=maxSpeed;
        }
        else
        {
            agentSpeed=minSpeed;
        }

        agent.speed=agentSpeed;
        
        if (_direction == Direction.Forward)
        {
            if (Vector3.Distance(transform.position, nodes[currentWaypoint].position) < switchDistance)
            {
                if(currentWaypoint==nodes.Count-1)
                {
                    currentWaypoint = 0;
                }
                else
                {
                    currentWaypoint++;
                }
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, nodes[currentWaypoint].position) < switchDistance)
            {
                if (currentWaypoint ==0)
                {
                    currentWaypoint = nodes.Count-1;
                }
                else
                {
                    currentWaypoint--;
                }
            }
        }
        agent.SetDestination(nodes[currentWaypoint].position);
    }
    void PlayAnimation()
    {
        if(Speed>.1f && Speed<walkAnimationThreshold)
        {
            if(Time.time>=nextTimeToPlayStep)
            {
                nextTimeToPlayStep=Time.time+walkRate;
                source.Play();
            }
            animator.SetFloat("Motion", 1);
        }
        else if(Speed>=walkAnimationThreshold)
        {
            animator.SetFloat("Motion", 2);
            if (Time.time >= nextTimeToPlayStep)
            {
                nextTimeToPlayStep = Time.time + runRate;
                source.Play();
            }
        }
        else
        {
            animator.SetFloat("Motion", 0);
        }
    }
    
    
}
