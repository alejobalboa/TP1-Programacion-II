using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{

    //Creación del pathing del dungeon
    public List<Cell> CreatePath(Vector2 dungeonSize, List<Cell> board, int startPos)
    {
        //Creación del path del dungeon
        int currentCell = startPos;

        //Generamos la Pila(Stack) donde armaremos el Laberinto
        Stack<int> path = new();

        while (currentCell != board.Count)
        {
            //marca la celda actual como visitada
            board[currentCell].visited = true;

            //si se alcanza la celda de salida
            //ser termina el bucle
            if (currentCell == board.Count - 1)
            {
                break;
            }

            //Check Neighbors cells
            List<int> neighbors = CheckNeighbors(dungeonSize, currentCell, board);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)]; //Randomización de dirección del path

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }

        return board;
    }

    List<int> CheckNeighbors(Vector2 dungeonSize, int cell, List<Cell> board)
    {
        List<int> neighbors = new List<int>();

        //check Up
        if (cell - dungeonSize.x >= 0 && !board[Mathf.FloorToInt(cell - dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - dungeonSize.x));
        }

        //check Down
        if (cell + dungeonSize.x < board.Count && !board[Mathf.FloorToInt(cell + dungeonSize.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + dungeonSize.x));
        }

        //check Right
        if ((cell + 1) % dungeonSize.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check Left
        if (cell % dungeonSize.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }

        return neighbors;
    }
}