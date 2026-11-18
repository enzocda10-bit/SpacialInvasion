using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Boss : MonoBehaviour
{
    private const string SpawnParam = "spawn";             // [ANIM] Trigger
    private const string PrepareJumpParam = "prepareJump"; // [ANIM] Trigger
    private const string AirborneParam = "isAirborne";     // [ANIM] Bool

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private float startBattleDistance = 25f;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 2f;

    [SerializeField] private float timeBeforeJump = 8f;

    [Header("Jump Settings")]
    [SerializeField] private float prepareJumpTime = 1.8f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float jumpDuration = 0.8f;
    [SerializeField] private float impactRadius = 3f;
    [SerializeField] private int damage = 20;
    [SerializeField] private float restTimeAfterJump = 1.5f;

    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        StartCoroutine(BossCycle());
    }

    private IEnumerator BossCycle()
    {
        while (Vector3.Distance(player.position, transform.position) > startBattleDistance)
        {
            yield return null;
        }

        while (true)
        {
            float time = 0f;

            while (time < timeBeforeJump)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(spawnInterval);
                time += spawnInterval;
            }

            yield return StartCoroutine(JumpAttack());
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        animator.SetTrigger(SpawnParam);

        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private IEnumerator JumpAttack()
    {
        if (player == null) yield break;

        Vector3 startPos = transform.position;
        Vector3 targetPos = player.position;

        targetPos.y = startPos.y;

        transform.LookAt(new Vector3(targetPos.x, transform.position.y, targetPos.z));

        animator.SetTrigger(PrepareJumpParam); // [ANIM] agacha e toma impulso -> estado "PrepareJump"

        yield return new WaitForSeconds(prepareJumpTime);

        SetAirborne(true); // [ANIM] saiu do chão -> estado "Jump" (fica em loop no ar)

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime / jumpDuration;
            Vector3 position = Vector3.Lerp(startPos, targetPos, time);
            position.y += Mathf.Sin(time * Mathf.PI) * jumpHeight;
            transform.position = position;
            yield return null;
        }

        transform.position = targetPos;

        SetAirborne(false); // [ANIM] encostou no chão -> estado "Land" (impacto)

        ApplyDamage();

        yield return new WaitForSeconds(restTimeAfterJump);
    }

    private void SetTrigger(string param) // [ANIM]
    {
        if (animator == null) return;  // [ANIM]
        animator.SetTrigger(param);    // [ANIM]
    }

    private void SetAirborne(bool airborne) // [ANIM]
    {
        if (animator == null) return;                    // [ANIM]
        animator.SetBool(AirborneParam, airborne);       // [ANIM]
    }

    private void ApplyDamage()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= impactRadius)
        {
            if (player.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, startBattleDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }
}
