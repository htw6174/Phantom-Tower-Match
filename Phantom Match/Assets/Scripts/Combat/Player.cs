using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public EncounterManager encounter;
    public int maxHealth;
    public int currentHealth;

    public GameObject fireSpell;
    public GameObject lightningSpell;
    public GameObject iceSpell;
    public GameObject bubbleSpell;

    void Start()
    {
        encounter = GameObject.FindGameObjectWithTag(Tags.encounterManager).GetComponent<EncounterManager>();
        currentHealth = maxHealth;
    }

    public void CastSpell(BlockType spellType)
    {
        switch (spellType)
        {
            case BlockType.Fire:
                Instantiate(fireSpell, transform.position, Quaternion.identity);
                break;
            case BlockType.Lightning:
                Instantiate(lightningSpell, transform.position, Quaternion.identity);
                break;
            case BlockType.Ice:
                Instantiate(iceSpell, transform.position, Quaternion.identity);
                break;
            case BlockType.Bubble:
                Instantiate(bubbleSpell, transform.position, Quaternion.identity);
                break;
        }
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
