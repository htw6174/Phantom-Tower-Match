using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public EncounterManager encounter;
    public int maxHealth;
    public int currentHealth;
    private bool isStunned = false;
    public int turnsStunned = 0;
    public int attackDamage;

    public Animation deathAnimation;
    public float deathDuration;

    public bool IsStunned
    {
        get
        {
            return turnsStunned > 0;
        }

        set
        {
            isStunned = value;
        }
    }

    void Start()
    {
        encounter = GameObject.FindGameObjectWithTag(Tags.encounterManager).GetComponent<EncounterManager>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log(name + " took " + damage + " damage!");
        currentHealth -= damage;
    }

    public void TakeStun(int turns)
    {
        turnsStunned = turns;
    }

    public void PlayDeathAnimation()
    {
        Debug.Log(name + " has died!");
    }
}
