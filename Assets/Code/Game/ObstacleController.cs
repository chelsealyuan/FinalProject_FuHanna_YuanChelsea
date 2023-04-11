using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public void RemoveObstacle()
    {
        gameObject.SetActive(false);
    }
}
