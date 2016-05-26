using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public EncounterManager encounter;
    public int maxHealth;
    public int currentHealth;

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
