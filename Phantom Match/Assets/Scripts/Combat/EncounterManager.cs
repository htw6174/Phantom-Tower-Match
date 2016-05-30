using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EncounterManager : MonoBehaviour {

    public GameController gameController;

    public Slider playerHealth;
    public Slider enemyHealth;

    public Text comboCounter;
    public Text stunCounter;

    public bool matchingInProgress = true;
    public bool combatInProgress = false;
    public bool turnInProgress = true;

    private float combo = 1;
    private float stunChance = 0f;

    private List<Match> matchesThisTurn;

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
        UpdateComboUI();
        UpdateStunUI();
    }

    public void AddMatch(Match match)
    {
        matchesThisTurn.Add(match);
        IncreaseCombo();
        IncreaseStun(match.stunChance);
    }
    
    //Increases the player combo 
    private void IncreaseCombo()
    {
        ToggleComboUI(true);
        combo += 0.1f;
        UpdateComboUI();
    }

    private void ResetCombo()
    {
        combo = 1;
        UpdateComboUI();
    }

    private void IncreaseStun(float stun)
    {
        if (stun > 0f)
        {
            ToggleStunUI(true);
            stunChance += stun;
            UpdateStunUI();
        }
    }

    private void ResetStun()
    {
        stunChance = 0f;
        UpdateStunUI();
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
        ResetCombo();
        ResetStun();
        ToggleComboUI(false);
        ToggleStunUI(false);
        turnInProgress = false;
    }

    private IEnumerator CastPlayerSpells(float timeBetweenSpells)
    {
        Debug.Log("Casting " + matchesThisTurn.Count + " spells...");
        WaitForSeconds animationDelay = new WaitForSeconds(timeBetweenSpells);


        foreach (Match match in matchesThisTurn)
        {
            player.CastSpell(match.type);
            yield return animationDelay;
            DealDamageToMonster((int)((float)match.damage * combo));
        }
        DealStunToMonster(stunChance);
        MonsterCombatPhase();
    }

    private void MonsterCombatPhase()
    {
        bool monsterDead = CheckForMonsterDeath();
        if (monsterDead == false && activeMonster.IsStunned == false) //Encounter manager should track and decrement monster stun
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
        if(activeMonster.IsStunned == false)
        {
            int damage = activeMonster.attackDamage;
            player.TakeDamage(damage);
            UpdatePlayerUI();
        }
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
        if (activeMonster.IsStunned == false)
        {
            Debug.Log(totalStunChance + "% chance to stun");
            float roll = Random.Range(0.01f, 1f);
            int stunCounter = 0;
            while (totalStunChance > roll)
            {
                stunCounter++;
                totalStunChance /= 2;
            }
            Debug.Log("Stunned for " + stunCounter + " turns");
            activeMonster.TakeStun(stunCounter);
        }
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
            gameController.Restart();
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

    //Called every time a set of matches is made
    private void UpdateComboUI()
    {
        comboCounter.text = string.Format("x{0:F2}", combo);
    }

    private void UpdateStunUI()
    {
        stunCounter.text = string.Format("{0:F2}%", stunChance);
    }

    private void ToggleComboUI(bool state)
    {
        comboCounter.enabled = state;
    }

    private void ToggleStunUI(bool state)
    {
        stunCounter.enabled = state;
    }
}
