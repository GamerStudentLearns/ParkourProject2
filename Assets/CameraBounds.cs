using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public GameObject player;
    public GameObject floor;
    private float minYPos;
    void Start()
    {
        Vector2 cameraBounds = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        minYPos = (floor.transform.position.y - floor.GetComponent<Renderer>().bounds.size.y / 2) + cameraBounds.y;
    }
    void LateUpdate()
    {
        Vector3 tempPosition = transform.position;
        if (player.transform.position.y < minYPos)
        {
            tempPosition.y = minYPos;
        }
        transform.position = tempPosition;
    }
}