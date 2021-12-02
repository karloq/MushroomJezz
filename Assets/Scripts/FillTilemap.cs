using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class FillTilemap : MonoBehaviour
{
    [SerializeField]
    private Tilemap wallsTilemap;
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private TextMeshProUGUI textMesh;

    [SerializeField] private GameObject balls;
    
    public List<Vector3> wallsPosition;
    public List<Vector3> vacantPosition;
    public int maxTiles;
    public float coverage;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var pos in groundTilemap.cellBounds.allPositionsWithin)
        {   
            var worldPosition = new Vector3Int(pos.x, pos.y, pos.z);
            var place = groundTilemap.CellToWorld(worldPosition);
            if (groundTilemap.HasTile(worldPosition))
            {
                vacantPosition.Add(place);
            }
        }

        maxTiles = vacantPosition.Count;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var pos in wallsTilemap.cellBounds.allPositionsWithin)
        {   
            var worldPosition = new Vector3Int(pos.x, pos.y, pos.z);
            var place = wallsTilemap.CellToWorld(worldPosition);
            if (wallsTilemap.HasTile(worldPosition))
            {
                wallsPosition.Add(place);
            }
        }

        foreach (var pos in vacantPosition)
        {
            var index = wallsPosition.IndexOf(pos);
            if (index > -1)
            {
                vacantPosition.Remove(pos);
            }
        }

        coverage = (vacantPosition.Count / maxTiles) * 100;
        
        textMesh.SetText(coverage.ToString() + " %");
    }
    
}
