using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

    public int moves;

    public int matchesOf3;
    public int matchesOf4;
    public int matchesOf5;
    public int matchesOf6;
    public int matchesOf7;

    public int blocksInMatch3;
    public int blocksInMatch4;
    public int blocksInMatch5;
    public int blocksInMatch6;
    public int blocksInMatch7;

    public int fireBlocksMatched;
    public int iceBlocksMatched;
    public int bubbleBlocksMatched;
    public int lightningBlocksMatched;

    public int totalDroppedBlocks = 0;

    public Text fireCount;
    public Text bubbleCount;
    public Text iceCount;
    public Text lightningCount;
    public Text matchCount;

    public void AddToMoveCount()
    {
        moves++;
    }

    public void AddBlockScore(int horizontalDistance, int verticalDistance, BlockType type)
    {
        switch (horizontalDistance)
        {
            case 3:
                blocksInMatch3++;
                break;
            case 4:
                blocksInMatch4++;
                break;
            case 5:
                blocksInMatch5++;
                break;
            case 6:
                blocksInMatch6++;
                break;
            case 7:
                blocksInMatch7++;
                break;
        }

        switch (verticalDistance)
        {
            case 3:
                blocksInMatch3++;
                break;
            case 4:
                blocksInMatch4++;
                break;
            case 5:
                blocksInMatch5++;
                break;
            case 6:
                blocksInMatch6++;
                break;
            case 7:
                blocksInMatch7++;
                break;
        }

        switch (type)
        {
            case BlockType.Fire:
                fireBlocksMatched++;
                break;
            case BlockType.Ice:
                iceBlocksMatched++;
                break;
            case BlockType.Bubble:
                bubbleBlocksMatched++;
                break;
            case BlockType.Lightning:
                lightningBlocksMatched++;
                break;
        }

        CalculateMatchCounts();
    }

    private void CalculateMatchCounts()
    {
        matchesOf3 = blocksInMatch3 / 3;
        matchesOf4 = blocksInMatch4 / 4;
        matchesOf5 = blocksInMatch5 / 5;
        matchesOf6 = blocksInMatch6 / 6;
        matchesOf7 = blocksInMatch7 / 7;

        int droppedBlocks = 0;

        droppedBlocks += blocksInMatch3 % 3;
        droppedBlocks += blocksInMatch4 % 4;
        droppedBlocks += blocksInMatch5 % 5;
        droppedBlocks += blocksInMatch6 % 6;
        droppedBlocks += blocksInMatch7 % 7;

        totalDroppedBlocks = droppedBlocks;

        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        fireCount.text = fireBlocksMatched.ToString();
        bubbleCount.text = bubbleBlocksMatched.ToString();
        iceCount.text = iceBlocksMatched.ToString();
        lightningCount.text = lightningBlocksMatched.ToString();
        matchCount.text = (matchesOf3 + matchesOf4 + matchesOf5 + matchesOf6 + matchesOf7).ToString();
    }
}
