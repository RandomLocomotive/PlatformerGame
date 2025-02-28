using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float staringHealth;
    public float currentHealth{ get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFrames;
    [SerializeField] private float NoOfFlashes;
    private SpriteRenderer SpriteRenderer;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = staringHealth;
        anim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, staringHealth);

        if (currentHealth > 0 )
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invurnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("Die");

                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, staringHealth);
    }

    private IEnumerator Invurnerability()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        for (int i = 0; i < NoOfFlashes; i++)
        {
            SpriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrames / (NoOfFlashes * 2));
            SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFrames / (NoOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(9, 10, false);

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}