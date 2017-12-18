using System.Collections;
using UnityEngine;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

/****************************************************
 * This class defines the behavior of Atalante
 * *************************************************/

public class AtalanteBehavior : MonoBehaviour
{
    public float maxSpeed = 8f;				// The fastest the player can travel in the x axis
    public float acceleration = 0.1f;		// The acceleration Atatante gets
    public float decceleration = 0.1f;     // Decceleration for when Atalante dies, for example
    public float jumpForce = 125f;			// Amount of force added when Atalante jumps.

    public Canvas winCanvas;

    public Canvas loseCanvas;

    private bool endLongHole = false;
    private Rigidbody2D rb;
    private Animator anim;
    private string state = "idle";
    private Trap trap;
    private int cpt = 10;
    public AudioSource source;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        state = "run";
    }

    // When Atalante collides with sthg
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "EndTrap":
                if(state == "climb")
                {
                    rb.gravityScale = 1;
                    cpt = 0;
                }
                endLongHole = (state == "swim");
                state = "run";
                break;
            case "Lose":
                Dead();
                break;
            case "LongHole":
                CheckLongHole(other);
                break;
            case "DeepHole":
                CheckDeepHole(other);
                break;
            case "Vine":
                CheckVine(other);
                break;
            case "Win":
                state = "win";
                rb.velocity = new Vector2(0, 0);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case "dead":
                Dead();
                StartCoroutine("LoadLoseCanvas");
                break;
            case "run":
                anim.SetTrigger("Run");
                Run();
                break;
            case "swim":
                anim.SetTrigger("Swim");
                Run();
                break;
            case "jump":
                anim.SetTrigger("Jump");
                Run();
                break;
            case "climb":
                anim.SetTrigger("Climb");
                Climb();
                break;
            case "win":
                anim.SetTrigger("Win");
                StartCoroutine("LoadWinCanvas");
                break;
            default:
                break;
        }
    }

    // How to behave when on run
    void Run()
    {
        rb.rotation = 0;

        if (cpt < 10)
        {
            transform.Translate(new Vector3(0.1f, 0, 0));
            cpt++;
        }

        if (endLongHole)
        {
            rb.AddForce(new Vector2(-50, 300));
            endLongHole = false;
        }

        Vector2 movement = new Vector2(rb.velocity.x + acceleration, rb.velocity.y);

        if (state == "swim")
        {
            if (movement.x > maxSpeed / 2)
                movement.x = maxSpeed / 2;
        }
        else
        {
            if (movement.x > maxSpeed)
            {
                movement.x = maxSpeed;
            }
        }

        rb.velocity = movement;
    }

    // How to behave when on climb
    void Climb()
    {
        rb.velocity = new Vector2(0, maxSpeed / 2f); ;
    }

    // How to behave when dead
    void Dead()
    {
        // Set dead to true if first call
        if (state != "dead")
        {
            state = "dead";
            anim.SetTrigger("Die");
            source.Play();
        }
        else
        {
            if (rb.velocity.x > decceleration)
            {
                rb.velocity = new Vector2(rb.velocity.x - decceleration, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            rb.rotation = 0;
        }
    }

    // Check if the trap is solved, set the state
    void CheckLongHole(Collider2D currentTrap)
    {
        if (CheckTrap(currentTrap.gameObject))
        {
            anim.SetTrigger("Swim");
            state = "swim";
            rb.AddForce(new Vector2(0, 100));
            currentTrap.offset = currentTrap.offset - new Vector2(0, 7f);
            currentTrap.isTrigger = false;
        }
        else
        {
            Dead();
        }
    }

    void CheckDeepHole(Collider2D currentTrap)
    {
        if (CheckTrap(currentTrap.gameObject))
        {
            anim.SetTrigger("Jump");
            state = "jump";

            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, jumpForce));
        }
    }

    void CheckVine(Collider2D currentTrap)
    {
        if (CheckTrap(currentTrap.gameObject))
        {
            anim.SetTrigger("Climb");
            state = "climb";
            rb.gravityScale = 0;
        }
    }

    bool CheckTrap(GameObject currentTrap)
    {
        trap = currentTrap.GetComponent<Trap>();
        return trap.isSolved;
    }

    IEnumerator LoadLoseCanvas()
    {
        yield return new WaitForSeconds(3);
        loseCanvas.gameObject.SetActive(true);
    }


    IEnumerator LoadWinCanvas()
    {
        yield return new WaitForSeconds(3);
        winCanvas.gameObject.SetActive(true);
    }
}