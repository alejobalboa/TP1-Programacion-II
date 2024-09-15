using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] _rooms;
    [SerializeField] Vector2 _offset; //separaci�n entre los rooms

    [SerializeField] Vector2 _dungeonSize; //tama�o del dungeon
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

        //Creaci�n del pathing del dungeon 
        _board = pathGen.CreatePath(_dungeonSize, _board, _startPos);

        //Instanciar habitaciones (ESTO LO PONGO AC� O NO LO PONGO AC�) - La parte del for m�s que nada, la instanciaci�n va en la factory claramente.
        for (int i = 0; i < _dungeonSize.x; i++)
        {
            for (int j = 0; j < _dungeonSize.y; j++)
            {
                Cell currentCell = _board[Mathf.FloorToInt(i + j * _dungeonSize.x)];

                if (currentCell.visited)
                {
                    int randomRoom;
                    Vector2 ubicacion = new Vector2(i, j);

                    if (i==0 && j == 0) //Habitaci�n Inicial
                    {
                        
                        randomRoom = 0;
                    }
                    else if (i== _dungeonSize.x - 1 && j == _dungeonSize.y -1 ) //Habitaci�n Final
                    {
                        //Elijo un room random
                        randomRoom = _rooms.Length - 1;
                    }
                    else
                    {
                        //Elijo un room random
                        randomRoom = Random.Range(1, _rooms.Length - 1); 
                    }
                    //Instancio el Room
                    roomFactory.CreateRoom(_rooms[randomRoom], _offset, ubicacion, currentCell);
                }
            }
        }       
    }

/*TODO LIST*/
//FALTA VER LO DE LAS HABITACIONES DE INICIO Y FINAL
//FALTA VER QUE FUNCIONE BIEN EL PATHING Y QUE LA CONDICION DE SALIDA DEL WHILE ESTE CORRECTA
//VER LO DEL SAVE LAYOUT DEL DUNGEON, LLEGO??
}
