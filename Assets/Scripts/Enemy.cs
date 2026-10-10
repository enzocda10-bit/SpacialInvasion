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

    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    [SerializeField] private float timeBetweenAttacks;

    private float nextShootTime;

    // Update is called once per frame
    void Update()
    {
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

    private void Attack()
    {
        agent.ResetPath();
        LookAtPlayer();

        if (Time.time >= nextShootTime)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            nextShootTime = Time.time + timeBetweenAttacks;
        }
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
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
