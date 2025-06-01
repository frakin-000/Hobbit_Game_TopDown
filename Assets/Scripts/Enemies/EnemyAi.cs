using UnityEngine;
using UnityEngine.AI;
using Battle.Utils;
using UnityEngine.InputSystem.XR.Haptics;
using System;
public class EnemyAi : MonoBehaviour
{
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimeMax = 2f;
    [SerializeField] private State startingState;

    private bool isChasingEnemy = true;
    private float chasingDistance = 4f;
    private float chasingSpeedMultiplier = 2f;

    private bool isAttackingEnemy = true;
    private float attackingDistance = 1.5f;
    private float attackingRate = 1f;
    private float nextAttackTime = 0;

    private NavMeshAgent navMeshAgent;
    private State currentState;
    private float roamingTimer;
    private Vector3 roamingPos;
    private Vector3 startPos;

    private float roamingSpeed;
    private float chasingSpeed;
    private float nextCheckDirectionTime = 0f;
    private float checkDirectionDuration = 0.01f;
    private Vector3 lastPosition;

    public event EventHandler OnEnemyAttack;



    private enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        currentState = startingState;
        roamingSpeed = navMeshAgent.speed;
        chasingSpeed = navMeshAgent.speed * chasingSpeedMultiplier;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();
    }

    public void SetDeathState()
    {
        navMeshAgent.ResetPath();
        currentState = State.Death;
    }
    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:

                roamingTimer -= Time.deltaTime;
                if (roamingTimer < 0)
                {
                    Roaming();
                    roamingTimer = roamingTimeMax;
                }
                CheckCurrentState();
                break;

            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }
    }
    private void ChasingTarget()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    public float GetRoamingAnimationSpeed()
    {
        return navMeshAgent.speed / roamingSpeed;
    }
    private void CheckCurrentState()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        var newState = State.Roaming;
        if (isChasingEnemy)
        {
            if (distanceToPlayer <= chasingDistance)
                newState = State.Chasing;
        }

        if (isAttackingEnemy)
        {
            if (distanceToPlayer <= attackingDistance)
                newState = State.Attacking;
        }

        if (newState != currentState)
        {
            if (newState == State.Chasing)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.speed = chasingSpeed;
            }
            else if (newState == State.Roaming)
            {
                roamingTimer = 0f;
                navMeshAgent.speed = roamingSpeed;
            }
            else if (newState == State.Attacking)
            {
                navMeshAgent.ResetPath();
            }
            currentState = newState;
        }
    }

    private void AttackingTarget()
    {
        if (Time.time > nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);
            nextAttackTime = Time.time + attackingRate;
        }
    }

    public bool IsRunning()
    {
        if (navMeshAgent.velocity == Vector3.zero)
            return false;
        return true;
    }

    private void Roaming()
    {
        startPos = transform.position;
        roamingPos = GetRoamingPosition();
        navMeshAgent.SetDestination(roamingPos);
    }

    private Vector3 GetRoamingPosition()
    {
        return startPos + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void MovementDirectionHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning())
                ChangeFaceDirection(lastPosition, transform.position);

            else if (currentState == State.Attacking)
                ChangeFaceDirection(transform.position, Player.Instance.transform.position);
        }


        lastPosition = transform.position;
        nextCheckDirectionTime = Time.time + checkDirectionDuration;
    }

    private void ChangeFaceDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
            transform.rotation = Quaternion.Euler(0, -180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
