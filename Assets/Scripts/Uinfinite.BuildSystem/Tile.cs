using System;
using System.Collections.Generic;
using UnityEngine;
 
namespace Uinfinite.BuildSystem
{
    public class Tile 
    {

        Action<Tile> cbTileTypeChange;
        GameObject myVisualTileGameObject;

        public enum TileType { Empty, Floor }

        TileType type = TileType.Empty;

        public TileType Type
        {
            get
            {
                return type;
            }
            set
            {
                TileType oldType = type;
                type = value;

                if(cbTileTypeChange != null && oldType != type)
                    cbTileTypeChange(this);
            }
        }

        LooseObject looseObject;
        InstallObject installObject;

        World World;

        public int X { get; protected set; }

        public int Y { get; protected set; }

        public Tile(World World, int x, int y)
        {
            this.World = World;
            this.X = x;
            this.Y = y;

        }

        public void RegisterTileTypeChangeCallback(Action<Tile> callback)
        {
            cbTileTypeChange += callback;
        }

        public void UnregisterTileTypeChangeCallback(Action<Tile> callback)
        {
            cbTileTypeChange -= callback;
        }
            
    }
}

