using System;
using System.Collections.Generic;
using Uinfinite.BuildSystem;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public static WorldController Instance{ get; protected set; }

    public Sprite floorSprite;

    public World World { get; protected set; }

    void Start ()
    {

        Instance = this;
        
        World = new World();

        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile_data = World.GetTileAt(x, y);

                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform);

                tile_go.AddComponent<SpriteRenderer>();

                tile_data.RegisterTileTypeChangeCallback((tile) =>
                    {
                        OnTileTypeChange(tile, tile_go);
                    });
            }

            World.RandomizeTiles();
        }

    }

    void Update ()
    {
        
    }

    void OnTileTypeChange (Tile tile_data, GameObject tile_go)
    {
        if(tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChange - Unrecognizd tile type");
        }
    }

}
