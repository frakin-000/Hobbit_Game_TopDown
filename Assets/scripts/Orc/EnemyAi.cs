using UnityEngine;
using UnityEngine.AI;
using Battle.Utils;
public class EnemyAi : MonoBehaviour
{
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimeMax = 2f;
    [SerializeField]private State startingState;

    private NavMeshAgent navMeshAgent;
    private State state;
    private float roamingTime;
    private Vector3 roamingPos;
    private Vector3 startPos;

    private enum State
    {
        Roaming
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        state = startingState;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimeMax;
                }
                break;
        }
    }

    private void Roaming()
    {
        roamingPos = GetRoamingPosition();
        ChangeFaceDirection(startPos, roamingPos);
        navMeshAgent.SetDestination(roamingPos);
    }

    private Vector3 GetRoamingPosition()
    {
        return startPos + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin,roamingDistanceMax);
    }

    private void ChangeFaceDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x)
            transform.rotation = Quaternion.Euler(0, -180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
