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
    string tempAnimation;
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "arrow")
        {
            PlayHitAnimation();
            hp -= 1;
            if (hp <= 0) Die();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gate.health <= 0)
        {
            isAttacking = false;
            navigation.enabled = false;
            SetAnimation("Wave");
        }

        else if (Vector3.Distance(transform.position, target.transform.position) < 2)
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
        gate.health -= dmg;
        StartCoroutine(AttackStance());
    }

    void Attack()
    {
        isAttacking = true;
        SetAnimation("Attack");
        StartCoroutine(AttackStance());
    }
    IEnumerator Dying()
    {
        GetComponent<Collider>().enabled = false;
        SetAnimation("Death");
        navigation.ResetPath();
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = info.length;
        yield return new WaitForSeconds(animationLength);
        animator.enabled = false;
        navigation.isStopped = true;
        navigation.enabled = false;
        while (transform.position.y > -1)
        {
            transform.position += new Vector3(0, -0.002f, 0f);
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
    void Die()
    {
        StartCoroutine(Dying());
    }
    public void PlayHitAnimation()
    {
        animator.Play("Hit"); // Play the secondary animation
        StartCoroutine(ReturnToPrimaryAnimation());
    }

    private IEnumerator ReturnToPrimaryAnimation()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = info.length;

        yield return new WaitForSeconds(animationLength); // Wait for the animation to finish
        if (!isAttacking) animator.Play("Walk");
        else animator.Play("Attack");
    }
}
