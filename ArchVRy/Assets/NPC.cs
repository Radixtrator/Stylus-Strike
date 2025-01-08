using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    Animator animator;
    NavMeshAgent navigation;
    GameObject target;
    public int hp = 1;
    public int dmg = 1;
    bool isAttacking = false;
    Gate gate;
    void Start()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        if (targets.Length > 0)
        {
            target = targets[Random.Range(0, targets.Length)];
        }
        gate = GameObject.FindGameObjectWithTag("Gate").GetComponent<Gate>();
        animator = GetComponent<Animator>();
        navigation = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navigation.destination = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 2)
        {
            if (!isAttacking) Attack();
        }
    }

    public void SetAnimation(string animation)
    {
        animator.Play(animation);
    }
    IEnumerator AttackStance()
    {
        navigation.enabled = false;
        yield return new WaitForSeconds(1);
        if (gate.health > 0)
        {
            gate.health -= dmg;
            StartCoroutine(AttackStance());
        }
        else
        {
            isAttacking = false;
            SetAnimation("Wave");
        }
    }
    void Attack()
    {
        isAttacking = true;
        SetAnimation("Attack");
        StartCoroutine(AttackStance());
    }
    void Die()
    {
        SetAnimation("Die");
        Destroy(gameObject);
    }
}
