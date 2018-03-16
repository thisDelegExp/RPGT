using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private float xmax, xmin, ymin, ymax;
    [SerializeField]
    private Tilemap tileMap;
    private Player player;
	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        player = target.GetComponent<Player>();
        Vector3 minTile = tileMap.CellToWorld(tileMap.cellBounds.min);
        Vector3 maxTile = tileMap.CellToWorld(tileMap.cellBounds.max);
        SetLimits(maxTile, minTile);
        player.SetLimits(minTile, maxTile);
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x,xmin,xmax), Mathf.Clamp(target.position.y, ymin, ymax),-10);
	}

    private void SetLimits(Vector3 maxTile,Vector3 minTile)
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        xmin = minTile.x + width / 2;
        xmax = maxTile.x - width / 2;

        ymin = minTile.y + height / 2;
        ymax = maxTile.y - height / 2;
    }
}
