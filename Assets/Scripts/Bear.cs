using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class Bear : MonoBehaviour
{
    public TextMeshProUGUI waterStatusText; // Reference to the UI text element

    public int cansCollected = 0;
    public int extinguishedFires = 0;

    public bool water = false; //whether or not the bear has water

    public int lives = 5;
    public float invulnerabilityTime = 2f; // The duration of invulnerability in seconds

    private bool invulnerable = false;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    // Reference to the NPCController
    private NPCController npcController;

    // Declare npcControllers as an array of NPCController
    private NPCController[] npcControllersArray;

    //updates for stats
    public FireSpawner fireSpawner;
    public List<NPCController> npcControllersInScene;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        rb = GetComponent<Rigidbody2D>();

        // Find all NPCs and get their NPCController scripts
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        if (npcs.Length > 0)
        {
            // Clear the list in GameStatistics instance in case it already contains data
            GameStatistics.instance.npcControllers.Clear();

            for (int i = 0; i < npcs.Length; i++)
            {
                NPCController controller = npcs[i].GetComponent<NPCController>();
                if (controller != null)
                {
                    GameStatistics.instance.npcControllers.Add(controller);
                }
            }
        }

        // Assign fireSpawner to GameStatistics
        GameStatistics.instance.fireSpawner = fireSpawner;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        // Update the statistics in our Singleton
        GameStatistics.instance.UpdateStatistics(extinguishedFires, cansCollected);

    }

    public int getLives()
    {
        return lives;
    }

    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

void Move()
{
    rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

    if (moveDirection != Vector2.zero)
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f; // Subtract 90 degrees
        rb.rotation = angle;
    }
}
    void OnCollisionEnter2D(Collision2D collision)
    {
    // Check if the collided object name contains the string "RiverBoundry"
    if (collision.gameObject.name.Contains("RiverBoundry"))
    {
        water = true;
        waterStatusText.text = "Water: Filled"; // Change the UI text
    }
    else if (collision.gameObject.name.Contains("Fire"))
    {
        water = false;
        waterStatusText.text = "Water: Empty"; // Change the UI text
    }
    else if (collision.gameObject.CompareTag("NPC"))
    {
        TakeDamage(1);
    }
    else if (collision.gameObject.CompareTag("Can")) // If collided with a Can
    {
        cansCollected++; // Increment cansCollected
        GameStatistics.instance.IncreaseCansCollected();
    }
    else if (collision.gameObject.CompareTag("Fire") && water) // If collided with a Fire and water is true
    {
        extinguishedFires++; // Increment extinguishedFires
    }
    }

    public void TakeDamage(int damage)
    {
        if (!invulnerable)
        {
            lives -= damage;
            if (lives <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(BecomeInvulnerable());
            }
        }
    }

    System.Collections.IEnumerator BecomeInvulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        invulnerable = false;
    }
        void Die()
        {
            // Handle player death here
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // reloads the current scene
        }

}

