using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;

    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    private const string isChassing = "isChasing";
    private const string isAttacking = "isShooting";

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            Chase();
        }
        else
        {
            Idle();
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    private void Attack()
    {
        agent.ResetPath();
        LookAtPlayer();
        SetAnimation(false, true);
    }

    private void LookAtPlayer()
    {
        Vector3 target = player.position;
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    private void Idle()
    {
        agent.ResetPath();
        SetAnimation(false, false);
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
        SetAnimation(true, false);
    }

    private void SetAnimation(bool chasing, bool attacking)
    {
        animator.SetBool(isChassing, chasing);
        animator.SetBool(isAttacking, attacking);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
