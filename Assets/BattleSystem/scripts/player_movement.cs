using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{
    public GameObject start;
    public float speed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject Interaction;
    private string currentBush;
    private int i;
    public GameObject Object;
    public GameObject gm;
    private GameObject go;
    public GameManager GM;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        start.SetActive(true);
        Object.SetActive(true);
        Interaction.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //dismisses the start screen text
        if (Input.GetKeyDown(KeyCode.X))
            start.SetActive(false);
        //this just makes sure that we arent in combat so you dont go walking around
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("BattleScene"))
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Hor", movement.x);
            animator.SetFloat("Vert", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        //if the message is up this lets the player burn the bush
        if (Interaction == true)
        {
            go = GameObject.Find(currentBush);

            if (Input.GetKeyDown(KeyCode.B))
            {
                
               
                if (GM.team.Contains("Ignum"))
                {
                Destroy(go);
                i++;
                }
            }
        }        
        
        
    }

    void  FixedUpdate()
    {
        
       //more movement            
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        
        //removes the tree line blocking the way when all the bushes are burned
        if (i >= 3)
        {
            Object.SetActive(false);
        }

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //if you run into a bush it tells you to burn it
        if (col.gameObject.tag == "bush")
        {
           
            currentBush = col.gameObject.name;
           
            Interaction.SetActive(true);
            
        }

        //if you run into an enemy it starts combat
        if (col.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene("BattleScene");
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        //if you leave the bush you can no longer burn it
        if (col.gameObject.tag == "bush")
        {
            Interaction.SetActive(false);
        }
    }
}
