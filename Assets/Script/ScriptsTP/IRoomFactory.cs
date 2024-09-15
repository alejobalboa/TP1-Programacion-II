using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRoomFactory
{
    public void CreateRoom(GameObject room, Vector2 offset, Vector2 ubicacion, Cell cell);
}
