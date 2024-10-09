using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth; //{ get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    [SerializeField] private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip[] deathSound;
    [SerializeField] private AudioClip[] hurtSound;

    [Header("Link to bar")]
    [SerializeField] private GameObject healthbar;

    public float _maxHp => startingHealth;

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            hurtSound = SoundHolder.Instance.PlayerHurt;
            deathSound = SoundHolder.Instance.PlayerDie;
        }
        else
        {
            hurtSound = SoundHolder.Instance.EnemyHurt;
            deathSound = SoundHolder.Instance.EnemyDie;
        }
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (!invulnerable)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

            if (healthbar != null)
            {
                healthbar.GetComponent<EnemyHealthbar>().UpdateHealthbar(currentHealth, startingHealth);
            }

            if (currentHealth > 0)
            {
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
                SoundHolder.Instance.PlaySound(hurtSound);
            }
            else
            {
                if (!dead)
                {
                    // ����������� ���� ������������ �����������
                    foreach (Behaviour component in components)
                        component.enabled = false;

                    anim.SetBool("grounded", true);
                    anim.SetTrigger("die");

                    dead = true;
                    SoundHolder.Instance.PlaySound(deathSound);
                }
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void SetHealth(float _value)
    {
        currentHealth = Mathf.Clamp(_value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        /*Physics2D.IgnoreLayerCollision(10, 11, true);*/
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        //Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private IEnumerator Immortality(int duration)
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        yield return new WaitForSeconds(duration);

        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    //Respawn
    public void Respawn()
    {
        SetHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());
        dead = false;

        //Activate all attached component classes
        foreach (Behaviour component in components)
            component.enabled = true;
    }

    // ��������� ����� Reset ��� ������ ���������
    public void Reset()
    {
        currentHealth = startingHealth;
        dead = false;
        invulnerable = false;
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
        anim.ResetTrigger("die");
        anim.Play("Idle");
        spriteRend.color = Color.white;
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    public void MakeImmortal(int duration)
    {
        StartCoroutine(Immortality(duration));
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
