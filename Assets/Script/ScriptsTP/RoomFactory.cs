using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFactory : IRoomFactory
{

    public void CreateRoom(GameObject room, Vector2 offset, Vector2 ubicacion, Cell cell)
    {
        GameObject newRoom = Object.Instantiate(room, new Vector3(ubicacion.x * offset.x, 0f, -ubicacion.y * offset.y), Quaternion.identity) as GameObject;
        RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
        rb.UpdateRoom(cell.status);

        newRoom.name += " " + ubicacion.x + "-" + ubicacion.y;
    }
}
