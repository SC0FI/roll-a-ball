using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{

    [SerializeField]
    private Transform player;

    private NavMeshAgent navMeshAgent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
