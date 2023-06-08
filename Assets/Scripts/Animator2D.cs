using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator2D : MonoBehaviour
{
    public enum AnimationState    // Enum representing different animation states
    {
        Idle,
        Walk,
        Jump,
        Climb
    }

    public float animationFPS;    // Frames per second for animation playback
    public Sprite[] idleAnimation;    // Array of sprites for idle animation
    public Sprite[] walkAnimation;    // Array of sprites for walk animation
    public Sprite[] jumpAnimation;    // Array of sprites for jump animation
    public Sprite[] climbAnimation;   // Array of sprites for climb animation

    private Controller2D controller;   // Reference to the Controller2D script
    private SpriteRenderer sRenderer;  // Reference to the SpriteRenderer component
    private PlayerClimb climbing;      // Reference to the PlayerClimb script

    private float frameTimer = 0;      // Timer to control animation frame rate
    private int frameIndex = 0;        // Index of the current frame being displayed
    private AnimationState state = AnimationState.Idle;   // Current animation state
    private Dictionary<AnimationState, Sprite[]> animationAtlas;   // Dictionary to map animation states to sprite arrays

    void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>();
        animationAtlas.Add(AnimationState.Idle, idleAnimation);
        animationAtlas.Add(AnimationState.Walk, walkAnimation);
        animationAtlas.Add(AnimationState.Jump, jumpAnimation);
        animationAtlas.Add(AnimationState.Climb, climbAnimation);

        sRenderer = GetComponent<SpriteRenderer>();    // Get the SpriteRenderer component
        controller = GetComponent<Controller2D>();       // Get the Controller2D component
        climbing = GetComponent<PlayerClimb>();          // Get the PlayerClimb component
    }

    void Update()
    {
        AnimationState newState = GetAnimationState();   // Determine the new animation state
        if (state != newState)
        {
            TransitionToState(newState);   // Transition to the new animation state if it has changed
        }

        frameTimer -= Time.deltaTime;   // Update the frame timer
        if (frameTimer <= 0.0f)
        {
            frameTimer = 1 / animationFPS;   // Reset the frame timer based on the desired frame rate
            Sprite[] anim = animationAtlas[state];   // Get the sprite array for the current animation state
            frameIndex %= anim.Length;   // Wrap the frame index within the valid range
            sRenderer.sprite = anim[frameIndex];   // Set the sprite to the current frame
            frameIndex++;   // Move to the next frame
        }

        if (controller.relativeVelocity.x < -0.01f)
        {
            sRenderer.flipX = true;   // Flip the sprite horizontally if the player is moving left
        }

        if (controller.relativeVelocity.x > 0.01f)
        {
            sRenderer.flipX = false;   // Reset the sprite's horizontal flip if the player is moving right
        }
    }

    void TransitionToState(AnimationState newState)
    {
        frameTimer = 0.0f;   // Reset the frame timer
        frameIndex = 0;     // Reset the frame index
        state = newState;   // Set the new animation state
    }

    AnimationState GetAnimationState()
    {
        if (!controller.grounded && !climbing.isClimbing)
        {
            return AnimationState.Jump;   // If the player is in the air and not climbing, return the jump animation state
        }
        if (Mathf.Abs(controller.relativeVelocity.x) > 0.1f)
        {
            return AnimationState.Walk;   // If the player's horizontal velocity is significant, return the walk animation state
        }
        if (climbing.isClimbing)
        {
            return AnimationState.Climb;  // If the player is climbing, return the climb animation state
        }
        return AnimationState.Idle;      // Otherwise, return the idle animation state
    }
}