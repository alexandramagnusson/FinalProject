using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator2D : MonoBehaviour
{
    public enum AnimationState
    {
        Idle,
        Walk,
        Jump,
        Climb
    }

    public float animationFPS;
    public Sprite[] idleAnimation;
    public Sprite[] walkAnimation;
    public Sprite[] jumpAnimation;
    public Sprite[] climbAnimation;

    private Controller2D controller;
    private SpriteRenderer sRenderer;
    private PlayerClimb climbing;

    private float frameTimer = 0;
    private int frameIndex = 0;
    private AnimationState state = AnimationState.Idle;
    private Dictionary<AnimationState, Sprite[]> animationAtlas;

    void Start()
    {
        animationAtlas = new Dictionary<AnimationState, Sprite[]>();
        animationAtlas.Add(AnimationState.Idle, idleAnimation);
        animationAtlas.Add(AnimationState.Walk, walkAnimation);
        animationAtlas.Add(AnimationState.Jump, jumpAnimation);
        animationAtlas.Add(AnimationState.Climb, climbAnimation);

        sRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller2D>();
        climbing = GetComponent<PlayerClimb>();
    }

    void Update()
    {
        AnimationState newState = GetAnimationState();
        if(state != newState) {
            TransitionToState(newState);
        }

        frameTimer -= Time.deltaTime;
        if(frameTimer <= 0.0f) {
            frameTimer = 1 / animationFPS;
            Sprite[] anim = animationAtlas[state];
            frameIndex %= anim.Length;
            sRenderer.sprite = anim[frameIndex];
            frameIndex++;
        }

        if(controller.relativeVelocity.x < -0.01f) {
            sRenderer.flipX = true;
        }

        if(controller.relativeVelocity.x > 0.01f) {
            sRenderer.flipX = false;
        }
    }

    void TransitionToState(AnimationState newState)
    {
        frameTimer = 0.0f;
        frameIndex = 0;
        state = newState;
    }

    AnimationState GetAnimationState()
    {
        if (!controller.grounded && !climbing.isClimbing) {
            return AnimationState.Jump;
        }
        if(Mathf.Abs(controller.relativeVelocity.x) > 0.1f) {
            return AnimationState.Walk;
        }
        if (climbing.isClimbing)
        {
            return AnimationState.Climb;
        }
        return AnimationState.Idle;
    }
}
