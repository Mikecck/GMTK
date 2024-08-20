using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Apple : MonoBehaviour
{
    [SerializeField]
    private GameObject correctTilemap;

    private bool isCorrectTilemapActive = false;
    private bool isFalling = false;
    private TilemapRenderer tilemapRenderer;

    private void Awake()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    private void Update()
    {
        // Handle falling
        if (isFalling)
        {
            Fall();
        }
    }

    public void CheckTilemap(GameObject activeTilemap)
    {
        if (activeTilemap == correctTilemap)
        {
            if (!isCorrectTilemapActive)
            {
                isCorrectTilemapActive = true;
                StartFalling();
            }
        }
        else
        {
            isCorrectTilemapActive = false;
        }
    }

    private void StartFalling()
    {
        isFalling = true;
    }

    private void Fall()
    {
        transform.position += Vector3.down * Time.deltaTime * 5;
    }
}
