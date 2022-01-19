using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject[] Dijkstra(Node begin, Node end)
    {
        Node tempBegin = begin;

        List<Node> tempToCheck = new List<Node>();
        List<Node> tempChecked = new List<Node>();
        tempToCheck.Add(tempBegin);

        Node[,] tempGrounds = Grid.Instance.grid;

        Node tempEndTile = end;

        while (tempToCheck.Count > 0)
        {
            Node tempTile = tempToCheck[0];
            tempToCheck.RemoveAt(0);

            //en haut
            if (tempTile.m_worldPosition.y < Grid.Instance.gridSizeY - 1)
            {
                Node tempSecondTile = new Node(true,new Vector3(0,0));
                tempSecondTile = tempGrounds[(int)tempTile.m_worldPosition.x, (int)tempTile.m_worldPosition.z + 1];
                //tempSecondTile.Coordonnee = tempSecondTile.transform.position;
                //tempSecondTile.Origin = tempTile;

                if (tempSecondTile == end)
                {
                    tempEndTile = tempSecondTile;
                    break;
                }

                if (!Node.Contains(tempToCheck, tempSecondTile) && !Node.Contains(tempChecked, tempSecondTile))
                {
                    tempToCheck.Add(tempSecondTile);
                    //tempSecondTile.HimSelf.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            //à droite
            if (tempTile.m_worldPosition.x < Grid.Instance.gridSizeX - 1)
            {
                Node tempSecondTile = new Node(true, new Vector3(0, 0));
                //tempSecondTile.HimSelf = tempGrounds[(int)tempTile.Coordonnee.x + 1, (int)tempTile.Coordonnee.z];
                //tempSecondTile.Coordonnee = tempSecondTile.HimSelf.transform.position;
                //tempSecondTile.Origin = tempTile;

                if (tempSecondTile == end)
                {
                    tempEndTile = tempSecondTile;
                    break;
                }


                if (!Node.Contains(tempToCheck, tempSecondTile) && !Node.Contains(tempChecked, tempSecondTile))
                {
                    tempToCheck.Add(tempSecondTile);
                    //tempSecondTile.HimSelf.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            //en bas
            if (tempTile.m_worldPosition.z > 0)
            {
                Node tempSecondTile = new Node(true, new Vector3(0, 0));
                //tempSecondTile.HimSelf = tempGrounds[(int)tempTile.Coordonnee.x, (int)tempTile.Coordonnee.z - 1];
                //tempSecondTile.Coordonnee = tempSecondTile.HimSelf.transform.position;
                //tempSecondTile.Origin = tempTile;

                if (tempSecondTile == end)
                {
                    tempEndTile = tempSecondTile;
                    break;
                }


                if (!Node.Contains(tempToCheck, tempSecondTile) && !Node.Contains(tempChecked, tempSecondTile))
                {
                    tempToCheck.Add(tempSecondTile);
                    //tempSecondTile.HimSelf.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            //à gauche
            if (tempTile.m_worldPosition.x > 0)
            {
                Node tempSecondTile = new Node(true, new Vector3(0, 0));
                //tempSecondTile.HimSelf = tempGrounds[(int)tempTile.Coordonnee.x - 1, (int)tempTile.Coordonnee.z];
                //tempSecondTile.Coordonnee = tempSecondTile.HimSelf.transform.position;
                //tempSecondTile.Origin = tempTile;

                if (tempSecondTile == end)
                {
                    tempEndTile = tempSecondTile;
                    break;
                }


                if (!Node.Contains(tempToCheck, tempSecondTile) && !Node.Contains(tempChecked, tempSecondTile))
                {
                    tempToCheck.Add(tempSecondTile);
                    //tempSecondTile.HimSelf.GetComponent<Renderer>().material.color = Color.red;
                }
            }

            tempChecked.Add(tempTile);

            //tempTile.gameobject.GetComponent<Renderer>().material.color = Color.green;
        }

        if (tempEndTile == null)
        {
            return null;
        }

        List<Node> tempResult = new List<Node>();
        while (tempEndTile != begin)
        {
            tempResult.Add(tempEndTile);
        }
        tempResult.Add(tempEndTile);

        //tempResult.Reverse();
        //return tempResult.ToArray();

        GameObject[] tempTilesResult = new GameObject[tempResult.Count];
        for (int i = 0; i < tempTilesResult.Length; ++i)
        {
            //tempTilesResult[i] = tempResult[tempResult.Count - i - 1];
        }

        return tempTilesResult;
    }
}
