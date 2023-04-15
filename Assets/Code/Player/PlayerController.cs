using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    //Outlets
    Rigidbody2D _rigidbody;
    Animator _animator;

    //configuration
    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;
    public float moveSpeed;
    public TMP_Text moneyText;

    SavePlayerPosition playerPositionData;

    public bool isPaused;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        playerPositionData = FindObjectOfType<SavePlayerPosition>();
        playerPositionData.PlayerPositionLoad();

        DestroyObjects();
    }

    void Update()
    {
        float movementSpeed = _rigidbody.velocity.sqrMagnitude;

        _animator.SetFloat("speed", movementSpeed);
        if (movementSpeed > 0.1f)
        {
            _animator.SetFloat("movementX", _rigidbody.velocity.x);
            _animator.SetFloat("movementY", _rigidbody.velocity.y);
        }

        UpdateDisplay();
    }

    void DestroyObjects()
    {
        //Debug.Log("inside destroy objects");
        if (GlobalVariables.objectsDestroyed.Count != 0)
        {
            foreach (string destroyObj in GlobalVariables.objectsDestroyed) {
                //Debug.Log("destroying " + destroyObj);
                GameObject obj = GameObject.Find(destroyObj);
                obj.SetActive(false);
            }
        }
    }

    void UpdateDisplay()
    {
        int moneyNum = GlobalVariables.money;
        moneyText.text = "Breads: " + moneyNum.ToString();
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
        playerPositionData.PlayerPositionSave(collision.gameObject.transform.position);

        if (collision.gameObject.CompareTag("FightActivator"))
        {
            SceneManager.LoadScene("FightScene");
            GlobalVariables.SetEnemy(collision.gameObject.name);
        }

        if (collision.gameObject.CompareTag("Chest"))
        {
            GlobalVariables.money += 50;

            GlobalVariables.objectsDestroyed.Add(collision.gameObject.name);

            //Destroy(collision.gameObject);

            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.name == "Signpost")
        {
            MenuController.instance.Show();
        }


        if (collision.gameObject.CompareTag("Final Reward"))
        {
            PopupController.instance.ShowFinalPayment();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            PopupController.instance.ShowPayment();
            GlobalVariables.currentObstacle = collision.gameObject;
        }
    }

}
