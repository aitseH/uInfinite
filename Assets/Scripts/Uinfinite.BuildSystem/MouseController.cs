using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uinfinite.BuildSystem
{
    public class MouseController : MonoBehaviour {

        Vector3 lastFramePosition;
        Vector3 dragStartPosition;

    	// Use this for initialization
    	void Start () {
    		
    	}
    	
    	// Update is called once per frame
    	void Update () {

            Vector3 currentFramPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Tile tileUnderMouse = GetTileAtWorldCoord(currentFramPosition);


            if(Input.GetMouseButton(0))
            {
                dragStartPosition = currentFramPosition; 
            }

            if(Input.GetMouseButton(0))
            {
                int start_x = Mathf.FloorToInt(dragStartPosition.x);
                int end_x = Mathf.FloorToInt(currentFramPosition.x);
                if (end_x < start_x)
                {
                    int tmp = end_x;
                    end_x = start_x;
                    start_x = tmp;
                }

                int start_y = Mathf.FloorToInt(dragStartPosition.y);
                int end_y = Mathf.FloorToInt(currentFramPosition.y);
                if (end_x < start_x)
                {
                    int tmp = end_y;
                    end_y = start_y;
                    start_y = tmp;
                }

                for (int x = start_x; x <= end_x; x++)
                {
                    for (int y = start_y; y <= end_y; y++)
                    {
                        Tile t = WorldController.Instance.World.GetTileAt(x, y);
                        if(t != null)
                        {
                            t.Type = Tile.TileType.Floor;
                        }
                    }
                }
            }

            if(Input.GetMouseButton(1) || Input.GetMouseButton(2) )
            {
                Vector3 diff = lastFramePosition - currentFramPosition;
                Camera.main.transform.Translate(diff);
            }

            lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    	}

        Tile GetTileAtWorldCoord(Vector3 coord)
        {
            int x = Mathf.FloorToInt(coord.x);
            int y = Mathf.FloorToInt(coord.y);

            return WorldController.Instance.World.GetTileAt(x, y);
        }
    }
}