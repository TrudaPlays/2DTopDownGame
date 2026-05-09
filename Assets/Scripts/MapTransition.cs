using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapTransition : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundary;
    CinemachineConfiner confiner;
    [SerializeField] Direction direction;
    public float transitionDistance = 2f;

    enum Direction { Up, Down, Left, Right}

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch(direction)
        {
            case Direction.Up:
                newPos.y += transitionDistance;
                break;
            case Direction.Down:
                newPos.y -= transitionDistance;
                break;
            case Direction.Left:
                newPos.x += transitionDistance;
                break;
            case Direction.Right:
                newPos.x -= transitionDistance;
                break;
        }

        player.transform.position = newPos;
    }

}
