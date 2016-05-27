using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

    public EncounterManager encounter;

    public int moves;

    public int fireBlocksMatched;
    public int iceBlocksMatched;
    public int bubbleBlocksMatched;
    public int lightningBlocksMatched;

    //Reset after every time the board is refilled with new blocks
    public int[] fireMatches = new int[8];
    public int[] lightningMatches = new int[8];
    public int[] iceMatches= new int[8];
    public int[] bubbleMatches = new int[8];

    public int totalDroppedBlocks = 0;

    public Text fireCount;
    public Text bubbleCount;
    public Text iceCount;
    public Text lightningCount;
    public Text matchCount;

    void Start()
    {
        encounter = GameObject.FindGameObjectWithTag(Tags.encounterManager).GetComponent<EncounterManager>();
        UpdateScoreDisplay();
    }

    public void AddToMoveCount()
    {
        moves++;
    }

    public void AddBlockScore(int horizontalDistance, int verticalDistance, BlockType type)
    {
        switch (type)
        {
            case BlockType.Fire:
                fireMatches[verticalDistance]++;
                fireMatches[horizontalDistance]++;
                fireBlocksMatched++;
                break;
            case BlockType.Ice:
                iceMatches[verticalDistance]++;
                iceMatches[horizontalDistance]++;
                iceBlocksMatched++;
                break;
            case BlockType.Bubble:
                bubbleMatches[verticalDistance]++;
                bubbleMatches[horizontalDistance]++;
                bubbleBlocksMatched++;
                break;
            case BlockType.Lightning:
                lightningMatches[verticalDistance]++;
                lightningMatches[horizontalDistance]++;
                lightningBlocksMatched++;
                break;
        }

        CalculateMatchCounts();
    }

    /// <summary>
    /// Add all matched made at the last filled board state to the match tracker
    /// </summary>
    public void AddNewMatches()
    {
        for (int i = 3; i < 8; i++)
        {
            int fireCount = fireMatches[i] / i;
            int lightningCount = lightningMatches[i] / i;
            int iceCount = iceMatches[i] / i;
            int bubbleCount = bubbleMatches[i] / i;

            AddMatchesOfType(BlockType.Fire, BlockDamageValues.FireMatchDamage[i], BlockDamageValues.FireStunChance[i], fireCount);
            AddMatchesOfType(BlockType.Lightning, BlockDamageValues.LightningMatchDamage[i], BlockDamageValues.LightningStunChance[i], lightningCount);
            AddMatchesOfType(BlockType.Ice, BlockDamageValues.IceMatchDamage[i], BlockDamageValues.IceStunChance[i], iceCount);
            AddMatchesOfType(BlockType.Bubble, BlockDamageValues.BubbleMatchDamage[i], BlockDamageValues.BubbleStunChance[i], bubbleCount);
        }

        ResetMatchedBlocks();
    }

    private void AddMatchesOfType(BlockType type, int damage, float stun, int count)
    {
        if (count == 0) return;
        for (int i = 0; i < count; i++)
        {
            //Debug.Log("Added " + type + " match for " + damage + " damage!");
            encounter.matchesThisTurn.Add(new Match(type, damage, stun));
        }
    }

    public void ResetMatchedBlocks()
    {
        fireMatches = new int[8];
        lightningMatches = new int[8];
        iceMatches = new int[8];
        bubbleMatches = new int[8];
    }

    private void CalculateMatchCounts()
    {
        //matchesOf3 = blocksInMatch3 / 3;
        //matchesOf4 = blocksInMatch4 / 4;
        //matchesOf5 = blocksInMatch5 / 5;
        //matchesOf6 = blocksInMatch6 / 6;
        //matchesOf7 = blocksInMatch7 / 7;

        //int droppedBlocks = 0;

        //droppedBlocks += blocksInMatch3 % 3;
        //droppedBlocks += blocksInMatch4 % 4;
        //droppedBlocks += blocksInMatch5 % 5;
        //droppedBlocks += blocksInMatch6 % 6;
        //droppedBlocks += blocksInMatch7 % 7;

        //totalDroppedBlocks = droppedBlocks;

        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        fireCount.text = fireBlocksMatched.ToString();
        bubbleCount.text = bubbleBlocksMatched.ToString();
        iceCount.text = iceBlocksMatched.ToString();
        lightningCount.text = lightningBlocksMatched.ToString();
        matchCount.text = "";
    }
}
