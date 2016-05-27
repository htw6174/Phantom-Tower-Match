public struct Match {

    public BlockType type;
    public int damage;
    public float stunChance;

    public Match(BlockType type, int damage, float stunChance)
    {
        this.type = type;
        this.damage = damage;
        this.stunChance = stunChance;
    }
}
