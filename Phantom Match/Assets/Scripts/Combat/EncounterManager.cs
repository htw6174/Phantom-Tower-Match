using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EncounterManager : MonoBehaviour {

    public GameController gameController;

    public Slider playerHealth;
    public Slider enemyHealth;

    public bool matchingInProgress = true;
    public bool combatInProgress = false;
    public bool turnInProgress = true;

    public List<Match> matchesThisTurn;

    public Player player;
    public Monster activeMonster; //Instantiated prefab from encounterMonsters
    public Monster[] encounterMonsters; //Prefabs to spawn, in order, during the combat encounter
    public int activeMonsterIndex = 0;
    public Vector3 monsterSpawnPosition;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<GameController>();
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>();
        matchesThisTurn = new List<Match>();
        SpawnNextEncounterMonster(activeMonsterIndex);
        UpdatePlayerUI();
        UpdateMonsterUI();
    }

    public void TurnHasStarted()
    {
        turnInProgress = true;
        //Turn on in-turn UI features

        MatchingHasStarted();
    }

    public void MatchingHasStarted()
    {
        matchingInProgress = true;
    }

    public void MatchingHasEnded()
    {
        matchingInProgress = false;
        CombatHasStarted();
    }

    private void CombatHasStarted()
    {
        combatInProgress = true;
        StartCoroutine(CastPlayerSpells(0.5f));
    }

    private void CombatHasEnded()
    {
        combatInProgress = false;
        matchesThisTurn = new List<Match>();
        TurnHasEnded();
    }

    private void TurnHasEnded()
    {
        //tell GameController to save the game

        turnInProgress = false;
    }

    private IEnumerator CastPlayerSpells(float timeBetweenSpells)
    {
        Debug.Log("Casting " + matchesThisTurn.Count + " spells...");
        WaitForSeconds animationDelay = new WaitForSeconds(timeBetweenSpells);

        float totalStunChance = 0f;

        foreach (Match match in matchesThisTurn)
        {
            player.CastSpell(match.type);
            yield return animationDelay;
            DealDamageToMonster(match.damage);
            totalStunChance += match.stunChance;
        }
        DealStunToMonster(totalStunChance);
        MonsterCombatPhase();
    }

    private void MonsterCombatPhase()
    {
        bool monsterDead = CheckForMonsterDeath();
        if (monsterDead == false && activeMonster.isStunned == false) //Encounter manager should track and decrement monster stun
        {
            DealDamageToPlayer();
            CheckForPlayerDeath();
        }
        CombatHasEnded();
    }

    /// <summary>
    /// Takes in damage amount from activeMonster and applies it to player
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamageToPlayer()
    {
        int damage = activeMonster.attackDamage;
        player.TakeDamage(damage);
        UpdatePlayerUI();
    }

    /// <summary>
    /// Takes in total damage value of all matched blocks and deals it to activeMonster
    /// </summary>
    /// <param name="damage"></param>
    public void DealDamageToMonster(int damage)
    {
        activeMonster.TakeDamage(damage);
        UpdateMonsterUI();
    }

    /// <summary>
    /// Takes in total stun value of all stun blocks matched on a turn, and converts it to a % chance to stun activeMonster
    /// </summary>
    /// <param name="stun"></param>
    public void DealStunToMonster(float totalStunChance)
    {
        //Gotta figure out the calculations for this on paper
    }

    /// <summary>
    /// Checks if player health is less than 0; if true calls death animation on player and reports encounter loss to gameController
    /// </summary>
    public bool CheckForPlayerDeath()
    {
        if (player.currentHealth <= 0)
        {
            player.PlayDeathAnimation();
            return true;
        }
        else return false;
    }

    /// <summary>
    /// Checks if activeMonster health is less than 0; if true calls death animation on activeMonster and sets activeMonster to next object in encounterMonsters
    /// </summary>
    public bool CheckForMonsterDeath()
    {
        if (activeMonster.currentHealth <= 0)
        {
            activeMonster.PlayDeathAnimation();
            Destroy(activeMonster.gameObject, activeMonster.deathDuration);
            SpawnNextEncounterMonster(++activeMonsterIndex);
            return true;
        }
        else return false;
    }

    private void SpawnNextEncounterMonster(int index)
    {
        if(index > encounterMonsters.Length - 1)
        {
            //Tell gameController the player has won the encounter
            Debug.Log("You won the encounter!");
        }
        else
        {
            Monster newMonster = Instantiate(encounterMonsters[index]).GetComponent<Monster>();
            newMonster.transform.position = monsterSpawnPosition;
            activeMonster = newMonster;
            UpdateMonsterUI();
        }
    }

    private void UpdatePlayerUI()
    {
        playerHealth.minValue = 0;
        playerHealth.maxValue = player.maxHealth;
        playerHealth.value = player.currentHealth;
    }

    private void UpdateMonsterUI()
    {
        enemyHealth.minValue = 0;
        enemyHealth.maxValue = activeMonster.maxHealth;
        enemyHealth.value = activeMonster.currentHealth;
    }
}
