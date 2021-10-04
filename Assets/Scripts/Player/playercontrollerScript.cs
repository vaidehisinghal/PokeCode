using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playercontrollerScript : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;
    private bool isMoving;
    private Vector2 input;
    public Text countText;
    private int count;
    private int pokeCount;
    public AudioSource PokeTheme;
    public AudioSource BallSound;
    public AudioSource BombSound;

    private Animator animator;
    private void Start()
    {     
        //PlayerPrefs.SetInt("Lives", 3);
        count = PlayerPrefs.GetInt("Lives", 3);
        countText.text = "Life : " + count.ToString();
        Debug.Log(count);
        pokeCount = 9;
        // BallSound = GetComponent<AudioSource>();
        // BombSound = GetComponent<AudioSource>();
        // PokeTheme = GetComponent<AudioSource>();
        PokeTheme.Play();
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //remove diagonal movement

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                if (IsWalkable(targetPos)){
                    StartCoroutine(Move(targetPos));
                }
                
            }
        }
        animator.SetBool("isMoving", isMoving);
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

    private void CheckForEncounters()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
            if(Random.Range(1,101) <= 10)
            {
                Debug.Log("Encountered a wild pokemon");
            }
        }
    }

    private void SetCountText()
    {
        countText.text = "Life : " + count.ToString();
        if(count <= 0)
        {
            SceneManager.LoadScene("Bye Bye");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //function complete
        if (collision.gameObject.tag == "PokeBall")
        {
            collision.gameObject.SetActive(false);
            PokeTheme.Pause();
            BallSound.Play();
            SceneManager.LoadScene("Battle");
        }
            
        else if (collision.gameObject.tag == "Bomb")
        {
            Debug.Log("Life Lost");
            
            collision.gameObject.SetActive(false);
            PokeTheme.Pause();
            BombSound.Play();
            PokeTheme.UnPause();
            count = count - 1;
            PlayerPrefs.SetInt("Lives", count);
            Debug.Log(count);
            SetCountText();
        }

    }
   
}
