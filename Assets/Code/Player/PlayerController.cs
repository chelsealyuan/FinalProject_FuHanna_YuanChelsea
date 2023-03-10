using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Outlets
    Rigidbody2D _rigidbody;

    //configuration
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public float moveSpeed;

    SavePlayerPosition playerPositionData;

    void Awake()
    {
        instance = this;
       // playerPositionData.PlayerPositionSave();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        playerPositionData = FindObjectOfType<SavePlayerPosition>();
        playerPositionData.PlayerPositionLoad();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //move up
        if (Input.GetKey(keyUp))
        {
            _rigidbody.AddForce(Vector2.up * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        //move down
        if (Input.GetKey(keyDown))
        {
            _rigidbody.AddForce(Vector2.down * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        //move left
        if (Input.GetKey(keyLeft))
        {
            _rigidbody.AddForce(Vector2.left * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        //move right
        if (Input.GetKey(keyRight))
        {
            _rigidbody.AddForce(Vector2.right * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) //can enter the fight scene
    {
        if (collision.gameObject.name == "FightActivator")
        {
            playerPositionData.PlayerPositionSave();
            SceneManager.LoadScene("FightScene");
        }

        if (collision.gameObject.name == "FightActivatorTwo")
        {
            playerPositionData.PlayerPositionSave();
            SceneManager.LoadScene("FightSceneTwo");
        }

    }

    
}
