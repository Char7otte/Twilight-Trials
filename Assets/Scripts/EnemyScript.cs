using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public float contactDistance;
    public float damageRate;

    private NavMeshAgent navMeshAgent;
    private GameObject player;

    private Rigidbody rb;
    private Animator anim;

    private Vector3 previousPosition;
    public float curSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = true;

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.GetIsGameOver()) 
        {
            navMeshAgent.isStopped = true;
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < contactDistance)
        {
            if (navMeshAgent.isStopped)
            {
                AudioManager.instance.PlayContactedSfx();
                navMeshAgent.isStopped = false;
            }

            navMeshAgent.SetDestination(player.transform.position);
        }
        else 
        {
            if (!navMeshAgent.isStopped)
            {
                navMeshAgent.isStopped = true;
                AudioManager.instance.PlayUnContactedSfx();
            }   
        }

        anim.SetBool("is_moving", false);

        //THERE IS NO RHYTHME OR REASON, BUT RB VELOCITY IS STUCK AT ZERO IN SCENE 1????????? 
        //IT MAKES NO SENSE
        //but this works so yay
        //and we're supposed to not need to code for this assignment tsk tsk tsk
        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        if (curSpeed > 0.0) {
            anim.SetBool("is_moving", true);
        }
        previousPosition = transform.position;

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Player")) 
        {
            GameManager.instance.MinusHealth(damageRate * Time.deltaTime);

            anim.SetTrigger("attack");
        }
    }
}
