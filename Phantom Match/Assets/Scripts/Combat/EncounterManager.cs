using UnityEngine;
using System.Collections;

public class EncounterManager : MonoBehaviour {

    public GameController gameController;

    public bool turnInProgress = true;

    public Player player;
    public Monster activeMonster; //Instantiated prefab from encounterMonsters
    public Monster[] encounterMonsters; //Prefabs to spawn, in order, during the combat encounter
    public int activeMonsterIndex = 0;
    public Vector3 monsterSpawnPosition;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>();
        activeMonster = SpawnNextEncounterMonster(activeMonsterIndex);
    }

    public void TurnHasStarted()
    {
        turnInProgress = true;
        //Turn on in-turn UI features
    }

    public void TurnHasEnded()
    {
        turnInProgress = false;
        //Apply all end-of-turn effects, cause combat damage to apply, tell GameController to save the game
        DealDamageToPlayer();
        CheckForPlayerDeath();
        DealDamageToMonster();
        CheckForMonsterDeath();
    }

    /// <summary>
    /// Takes in damage amount from activeMonster and applies it to player
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamageToPlayer()
    {
        int damage = activeMonster.attackDamage;
        player.TakeDamage(damage);
    }

    /// <summary>
    /// Takes in total damage value of all matched blocks and deals it to activeMonster
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamageToMonster()
    {
        int damage = 20;
        activeMonster.TakeDamage(damage);
    }

    /// <summary>
    /// Takes in total stun value of all stun blocks matched on a turn, and converts it to a % chance to stun activeMonster
    /// </summary>
    /// <param name="stun"></param>
    public void DealStunToMonster(int stun)
    {

    }

    /// <summary>
    /// Checks if player health is less than 0; if true calls death animation on player and reports encounter loss to gameController
    /// </summary>
    public void CheckForPlayerDeath()
    {
        if(player.currentHealth <= 0)
        {
            player.PlayDeathAnimation();
        }
    }

    /// <summary>
    /// Checks if activeMonster health is less than 0; if true calls death animation on activeMonster and sets activeMonster to next object in encounterMonsters
    /// </summary>
    public void CheckForMonsterDeath()
    {
        if(activeMonster.currentHealth <= 0)
        {
            activeMonster.PlayDeathAnimation();
            Destroy(activeMonster.gameObject);
            SpawnNextEncounterMonster(++activeMonsterIndex);
        }
    }

    private Monster SpawnNextEncounterMonster(int index)
    {
        Monster newMonster = Instantiate(encounterMonsters[index]).GetComponent<Monster>();
        newMonster.transform.position = monsterSpawnPosition;
        return newMonster;
    }
}
