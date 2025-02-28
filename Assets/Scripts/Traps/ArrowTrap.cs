using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float AttackCooldown;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private GameObject[] Arrows;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;

        Arrows[FindArrows()].transform.position = FirePoint.position;
        Arrows[FindArrows()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindArrows()
    {
        for (int i = 0; i < Arrows.Length; i++)
        {
            if (!Arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= AttackCooldown)
            Attack();
    }
}
// test