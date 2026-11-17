using UnityEngine;

// A wall that keeps appearing and disappearing.
public class TimedWall : MonoBehaviour
{
    public float showTime = 2f;   // seconds the wall stays visible
    public float hideTime = 2f;   // seconds the wall stays hidden

    float timer;
    bool isShown = true;
    Collider2D col;
    SpriteRenderer sr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        timer = showTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            isShown = !isShown;                        // flip: shown -> hidden, hidden -> shown
            col.enabled = isShown;                     // turn collision on / off
            sr.enabled = isShown;                      // turn the picture on / off
            timer = isShown ? showTime : hideTime;     // restart the countdown
        }
    }
}