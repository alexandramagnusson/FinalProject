using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlatformerController2D : Controller2D
{
    public float jumpforce;
    public int lives = 5;
    private float inputX;
    private SpriteRenderer sRenderer;
    private bool invulnerable = false;

    public AudioClip jumpsound;
    public AudioClip hitsound;
    public AudioClip coinsound;
    public AudioClip killsound;
    private AudioSource audioSource;

    public override void Start()
    {
        base.Start();
        sRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        HeartsUI.SetLives(lives);
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal") * speed;
        Vector2 vel = rb2d.velocity;
        vel.x = inputX;
        relativeVelocity = vel;

        UpdateGrounding();
        if(onMovingPlatform != null)
        {
            vel.x += onMovingPlatform.rb2d.velocity.x;
        }

        bool inputJump = Input.GetKeyDown(KeyCode.Space);
        if (inputJump && grounded) {
            AudioSource.PlayClipAtPoint(jumpsound, transform.position);
            vel.y = jumpforce;
            //Debug.Log("jump");
            relativeVelocity.y = vel.y;
        }
        rb2d.velocity = vel;
    }

    protected override void Hurt(Vector3 impactDirection)
    {
        if (Mathf.Abs(impactDirection.x) > Mathf.Abs(impactDirection.y)) {
            TakeDamage();
        } else {
            if(impactDirection.y > 0.0f) {
                TakeDamage();
            }
            AudioSource.PlayClipAtPoint(killsound, transform.position);
            Vector2 vel = rb2d.velocity;
            vel.y = jumpforce;
            rb2d.velocity = vel;
        }
    }

    public void TakeDamage()
    {
        if (invulnerable) {
            return;
        }
        AudioSource.PlayClipAtPoint(hitsound, transform.position);
        lives--;
        HeartsUI.RemoveHeart();
        if(lives <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        StartCoroutine(Invulnerability(1));
    }

        public override void Heal(Vector3 impactDirection)
        {
            if (invulnerable) {
                return;
            }

            lives++;
            HeartsUI.AddHeart();
        }

    public void CollectCoin()
    {
        AudioSource.PlayClipAtPoint(coinsound, transform.position);
    }

    IEnumerator Invulnerability(float time)
    {
        Debug.Log("Invulnerable " + Time.time);
        invulnerable = true;
        for(int i = 0; i < time/0.2f; i++) {
            sRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        invulnerable = false;
    }
}
