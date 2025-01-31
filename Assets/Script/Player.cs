using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Vector2 movement = new Vector2();
    Rigidbody2D rb;
    public float MovementSpeed = 3.0f;
    GameObject Safe_Zone;
    public int MaxCoin;
    public int MaxSafe;
    public List<GameObject> nowCoinList = new List<GameObject>();
    public int nowCoin;
    public GameObject nowKey;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MaxCoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        MaxSafe = GameObject.FindGameObjectsWithTag("Safe").Length;
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Safe"))
        {
            MaxSafe = MaxSafe - 1;
            Safe_Zone = collision.gameObject;
            collision.gameObject.tag = "Recycle Safe";
            MaxCoin -= nowCoin;
            nowCoin = 0;
            nowKey = null;
            nowCoinList.Clear();
            Next();
        }
        if (collision.CompareTag("Recycle Safe"))
        {
            Safe_Zone = collision.gameObject;
            MaxCoin -= nowCoin;
            nowCoin = 0;
            nowKey = null;
            nowCoinList.Clear();
            Next();
        }
        if (collision.CompareTag("Ball"))
        {
            gameObject.transform.position = Safe_Zone.transform.position;
            nowCoin = 0;
            if(nowKey != null)
            {
                nowKey.GetComponent<Key>().Retry();
            }
            for(int i =0; i <nowCoinList.Count; i++)
            {
                nowCoinList[i].gameObject.SetActive(true);
            }
            
        }

        if (collision.CompareTag("Coin"))
        {
            nowCoin += 1;
            nowCoinList.Add(collision.gameObject);
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Key"))
        {
            collision.gameObject.GetComponent<Key>().Eat();
            nowKey = collision.gameObject;
        }
    }

    public void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = movement * MovementSpeed;
    }

    void Next()
    {
        if(MaxSafe == 0 && MaxCoin == 0)
        {
            Gamemanager.instance.stagenumber += 1;
            SceneManager.LoadScene(Gamemanager.instance.stagenumber);
        }
    }
}
