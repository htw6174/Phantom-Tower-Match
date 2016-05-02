using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public GridPosition gridPos;

    public BlockType type;

    //this is being set to the opposite of what it should be for some reason, need to fix dat shit
    public bool inMotion = false;

    public void SetPosition(int newX, int newY)
    {
        gridPos.x = newX;
        gridPos.y = newY;
    }

    /// <summary>
    /// Tell a block to move from its current position to newPosition, at a rate of [speed] units per second
    /// </summary>
    /// <param name="newPosition"></param>
    /// <param name="speed">Numver of units the block moves in one second</param>
    /// <param name="delay">Number of seconds to wait until motion starts</param>
    public IEnumerator MoveBlock(Vector3 newPosition, float speed = 1f, float delay = 0f)
    {
        WaitForSeconds frameDelay = new WaitForSeconds(1f / 60f);
        WaitForSeconds fallDelay = new WaitForSeconds(delay);

        inMotion = true;

        yield return fallDelay;

        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, newPosition);
        int steps = (int)(distance * (60f / speed));
        for (int i = 0; i < steps; i++)
        {
            transform.position = Vector3.Lerp(startPosition, newPosition, (float)i / steps);
            yield return frameDelay;
        }
        transform.position = newPosition;

        inMotion = false;
    }

    public void DestroyBlock(float time = 1f, int steps = 4)
    {
        StartCoroutine(ShrinkBlock(time, steps));
        Destroy(gameObject, time);
    }

    private IEnumerator ShrinkBlock(float time, int steps)
    {
        //WaitForSeconds frameDelay = new WaitForSeconds(1f / 60f);
        //int steps = (int)(60f / speed);
        WaitForSeconds stepTime = new WaitForSeconds(time / steps);
        for (int i = 0; i < steps; i++)
        {
            transform.localScale = Vector3.one * (1f - ((float)i / steps));
            yield return stepTime;
        }
    }
}
