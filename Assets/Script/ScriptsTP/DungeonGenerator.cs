using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] _rooms;
    [SerializeField] Vector2 _offset; //separación entre los rooms

    [SerializeField] Vector2 _dungeonSize; //tamaño del dungeon
    [SerializeField] int _startPos = 0; //posicion inicial

    private List<Cell> _board; //tablero

    private RoomFactory roomFactory;
    private PathGeneration pathGen;

    void Start()
    {
        roomFactory = new RoomFactory();
        pathGen = new PathGeneration();
        MazeGenerator();
    }

    public void MazeGenerator()
    {
        //Create Dungeon _board
        _board = new List<Cell>();

        float _boardLenght = _dungeonSize.x * _dungeonSize.y;

        for (int i = 0; i < _boardLenght; i++)
        {
            _board.Add(new Cell());
        }

        //Creación del pathing del dungeon 
        _board = pathGen.CreatePath(_dungeonSize, _board, _startPos);

        //Instanciar habitaciones (ESTO LO PONGO ACÁ O NO LO PONGO ACÁ) - La parte del for más que nada, la instanciación va en la factory claramente.
       /* for (int i = 0; i < _dungeonSize.x; i++)
        {
            for (int j = 0; j < _dungeonSize.y; j++)
            {
                Cell currentCell = _board[Mathf.FloorToInt(i + j * _dungeonSize.x)];

                if (currentCell.visited)
                {
                    int randomRoom = Random.Range(0, _rooms.Length);

                    GameObject newRoom = Object.Instantiate(_rooms[randomRoom], new Vector3(i * _offset.x, 0f, -j * _offset.y), Quaternion.identity) as GameObject;
                    RoomBehaviour rb = newRoom.GetComponent<RoomBehaviour>();
                    rb.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }*/

        //Instantiate rooms
        roomFactory.Create(_dungeonSize, _board, _rooms, _offset);
    }

/*TODO LIST*/
//FALTA VER LO DE LAS HABITACIONES DE INICIO Y FINAL
//FALTA VER QUE FUNCIONE BIEN EL PATHING Y QUE LA CONDICION DE SALIDA DEL WHILE ESTE CORRECTA
//VER LO DEL SAVE LAYOUT DEL DUNGEON, LLEGO??
}
