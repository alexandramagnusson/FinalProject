using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{
    public enum AnimationState
    {
        Idle,
        WalkLeft,
        WalkRight,
        WalkUp,
        WalkDown
    }

    public GameObject bear; // The bear object
    public TextMeshProUGUI statusText; // The UI Text object

    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 2f;

    public GameObject objectToSpawn; // The GameObject to spawn
    public float spawnRate = 2f; // The rate to spawn the GameObject, in seconds

    private Vector2 currentTarget;

    public int totalCans; // total cans

    public SpriteRenderer spriteRenderer; // The SpriteRenderer component on the NPC
    public Sprite[] idleSprites;
    public Sprite[] walkLeftSprites;
    public Sprite[] walkRightSprites;
    public Sprite[] walkUpSprites;
    public Sprite[] walkDownSprites;

    private Dictionary<AnimationState, Sprite[]> animationAtlas;
    private AnimationState currentState;

    private int currentSpriteIndex;
    private float frameTimer;
    public float animationFPS;

    private void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>()
        {
            { AnimationState.Idle, idleSprites },
            { AnimationState.WalkLeft, walkLeftSprites },
            { AnimationState.WalkRight, walkRightSprites },
            { AnimationState.WalkUp, walkUpSprites },
            { AnimationState.WalkDown, walkDownSprites }
        };

        currentTarget = pointB;
        currentState = AnimationState.WalkRight;

        StartCoroutine(SpawnObject());
        StartCoroutine(PlayAnimation());
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        Vector2 oldPosition = transform.position;
        Vector2 newPosition = Vector2.MoveTowards(oldPosition, currentTarget, step);
        transform.position = newPosition;

        AnimationState newState;
        if (newPosition == oldPosition)
        {
            newState = AnimationState.Idle;
        }
        else if (newPosition.x < oldPosition.x)
        {
            newState = AnimationState.WalkLeft;
        }
        else if (newPosition.x > oldPosition.x)
        {
            newState = AnimationState.WalkRight;
        }
        else if (newPosition.y < oldPosition.y)
        {
            newState = AnimationState.WalkDown;
        }
        else // newPosition.y > oldPosition.y
        {
            newState = AnimationState.WalkUp;
        }

        if (newPosition == pointA)
        {
            currentTarget = pointB;
        }
        else if (newPosition == pointB)
        {
            currentTarget = pointA;
        }

        if (currentState != newState)
        {
            currentState = newState;
            frameTimer = 0f;
            currentSpriteIndex = 0;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Bear bear = collision.gameObject.GetComponent<Bear>();
            if (bear != null)
            {
                bear.TakeDamage(1);

                // Update the status text and then wait for 3 seconds before changing it back to empty
                statusText.text = $"Scared a camper, {bear.getLives()} lives left!";
                StartCoroutine(WaitAndClearText(3f));
            }
        }
    }

    IEnumerator SpawnObject()
    {
        // Loop indefinitely
        while (true)
        {
            // Instantiate the object at the NPC's current position
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            totalCans++;

            // Wait for spawnRate seconds
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator WaitAndClearText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        statusText.text = "";
    }

    IEnumerator PlayAnimation()
    {
        while (true)
        {
            if (frameTimer <= 0f)
            {
                Sprite[] currentSprites = animationAtlas[currentState];
                spriteRenderer.sprite = currentSprites[currentSpriteIndex];
                currentSpriteIndex = (currentSpriteIndex + 1) % currentSprites.Length;
                frameTimer = 1 / animationFPS;
            }
            frameTimer -= Time.deltaTime;
            yield return null;
        }
    }
}
