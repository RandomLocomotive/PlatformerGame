using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float staringHealth;
    public float currentHealth{ get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = staringHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, staringHealth);

        if (currentHealth > 0 )
        {
            anim.SetTrigger("Hurt");
            //iframes
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Die");
                GetComponent<PlayerControl>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, staringHealth);
    }
}

// kontynuuj od minuty 14:30 w filmie 7