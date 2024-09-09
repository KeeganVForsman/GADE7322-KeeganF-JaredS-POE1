using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform movePostionTransform;

    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    private void Update()
    {
        agent.destination = movePostionTransform.position;
    }
}