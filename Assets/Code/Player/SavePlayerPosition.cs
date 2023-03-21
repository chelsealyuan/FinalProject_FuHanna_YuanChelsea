using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPosition : MonoBehaviour
{

    public GameObject player;

    private void Start()
    {

        if(PlayerPrefs.GetInt("Saved") == 1 && PlayerPrefs.GetInt("TimeToLoad") == 1)
        {
            float pX = player.transform.position.x;
            float pY = player.transform.position.y;

            pX = PlayerPrefs.GetFloat("p_x");
            pY = PlayerPrefs.GetFloat("p_y");

            player.transform.position = new Vector2(pX, pY);

            PlayerPrefs.SetInt("TimeToLoad", 0);

            PlayerPrefs.Save();
        }
        
    }

    public void PlayerPositionSave(Vector3 colliderPosition)
    {

       Vector3 shiftVector = Random.insideUnitCircle.normalized * 2;

        float adjusted_pX = colliderPosition.x + shiftVector.x;
        float adjusted_pY = colliderPosition.y + shiftVector.y;

        PlayerPrefs.SetFloat("p_x", adjusted_pX);
        PlayerPrefs.SetFloat("p_y", adjusted_pY);
        PlayerPrefs.SetInt("Saved", 1);

        PlayerPrefs.Save();
    }

    public void PlayerPositionLoad()
    {
        //Debug.Log("Loading player postition at: " + player.transform.position);
        PlayerPrefs.SetInt("TimeToLoad", 1);
        PlayerPrefs.Save();
    }
}
