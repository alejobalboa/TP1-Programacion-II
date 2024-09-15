using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory
{

    public void Create(Vector2 dungeonSize, List<Cell> board, GameObject[] rooms, Vector2 offset)
    {
        for (int i = 0; i < dungeonSize.x; i++)
        {
            for (int j = 0; j < dungeonSize.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * dungeonSize.x)];

                if (currentCell.visited)
                {
                    int randomRoom = Random.Range(0, rooms.Length);

                    GameObject newRoom = Object.Instantiate(rooms[randomRoom], new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            } 
        }
    }
}
