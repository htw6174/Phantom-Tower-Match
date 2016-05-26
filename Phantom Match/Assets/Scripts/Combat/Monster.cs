using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {

    public EncounterManager encounter;
    public int maxHealth;
    public int currentHealth;
    public bool isStunned = false;
    public int turnsStunned = 0;
    public int attackDamage;

    void Start()
    {
        encounter = GameObject.FindGameObjectWithTag(Tags.encounterManager).GetComponent<EncounterManager>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void PlayDeathAnimation()
    {
        Debug.Log(name + " has died!");
    }
}
