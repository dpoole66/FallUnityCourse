// ClickToMove.cs
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class ClickToMove : MonoBehaviour {
    RaycastHit hitInfo = new RaycastHit();
    UnityEngine.AI.NavMeshAgent ThisAgent;
    Animator ThisAnimator;
    bool m_Patrol;
    public Transform PatrolDestination = null;
    Vector2 velocity = Vector2.zero;

    void Start() {
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        ThisAnimator = GetComponent<Animator>();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {     
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                ThisAgent.destination = hitInfo.point;
        }
    }

    void UpdateAnimator() {
        ThisAnimator.SetBool("Patrol", m_Patrol);
        ThisAnimator.SetFloat("VeloX", velocity.x);
        ThisAnimator.SetFloat("VeloY", velocity.y);
    }

    public void OnAnimatorMove() {
        Vector3 position = ThisAnimator.rootPosition;
        position = ThisAgent.nextPosition;
        transform.position = position;

        //Patrol
        if (m_Patrol == true) {
            ThisAgent.destination = PatrolDestination.position;
            transform.position = position;
        }

    }
}

