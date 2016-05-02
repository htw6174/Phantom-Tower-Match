using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlockGrid
{

    public int width;
    public int height;

    private Block[,] blocks;

    public BlockGrid(int width, int height)
    {
        InitializeGrid(width, height);
    }

    public void InitializeGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        blocks = new Block[width, height];
    }

    /// <summary>
    /// Returns the Block at position; Returns new block with BlockType of 'none' if out of range
    /// </summary>
    /// <param name="x">Horizontal postion</param>
    /// <param name="y">Vertical position</param>
    /// <returns></returns>
    public Block GetBlock(int x, int y, bool logError = false)
    {
        if (x < 0 || y < 0 || x > width - 1 || y > height - 1)
        {
            if (logError)
            {
                Debug.Log("Tried to select block at " + x + ", " + y + ", which is outside of the grid!");
            }
            return null;
        }
        else
        {
            //x = Mathf.Clamp(x, 0, width - 1);
            //y = Mathf.Clamp(y, 0, height - 1);
            return blocks[x, y];
        }
    }

    /// <summary>
    /// Sets block at [x, y] to newBlock and returns true on success
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="newBlock"></param>
    /// <returns></returns>
    public bool SetBlock(int x, int y, Block newBlock, bool logError = false)
    {
        if (x < 0 || y < 0 || x > width - 1 || y > height - 1)
        {
            if (logError)
            {
                Debug.Log("Tried to set block at " + x + ", " + y + ", which is outside of the grid!");
            }
            return false;
        }
        else
        {
            blocks[x, y] = newBlock;
            return true;
        }
    }

    public BlockType GetBlockType(int x, int y)
    {
        Block returned = GetBlock(x, y);
        if (returned == null) return BlockType.None;
        else return returned.type;
    }
}
