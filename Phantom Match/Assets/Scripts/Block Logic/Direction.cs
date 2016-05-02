public enum Direction
{
    None,
    Up,
    Down,
    Right,
    Left
}

public static class Directions
{
    public static int ToXInt(this Direction direction)
    {
        if (direction == Direction.Right) return 1;
        else if (direction == Direction.Left) return -1;
        else if (direction == Direction.Up || direction == Direction.Down) return 0;
        else return 0;
    }

    public static int ToYInt(this Direction direction)
    {
        if (direction == Direction.Up) return 1;
        else if (direction == Direction.Down) return -1;
        else if (direction == Direction.Right || direction == Direction.Left) return 0;
        else return 0;
    }
}