using UnityEngine;

public enum BlockType
{
    None,
    Fire,
    Bubble,
    Lightning,
    Ice
}

public static class BlockTypeFunctions
{
    public static int BlockTypeCount = 5;

    public static BlockType GetRandomBlockType()
    {
        return (BlockType)Random.Range(1, BlockTypeCount);
    }
}