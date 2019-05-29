using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    GameObject tilePrefab, exitUiPrefab, playerStartGO;
    [SerializeField]
    Vector3 tileScale;
    [SerializeField]
    float playerYOffset;
    GridTile startTile;

    [SerializeField]
    TextAsset file;
    Dictionary<TilePosition, GridTile> tiles = new Dictionary<TilePosition, GridTile>();
    [SerializeField]
    int rowCount, columnCount;

    const char k_EMPTY_TILE = ' ';
    const char k_START = 's';
    const char k_EXIT = 'x';
    const char k_NORTH = 'N';
    const char k_SOUTH = 'S';
    const char k_EAST = 'E';
    const char k_WEST = 'W';



    protected LevelManager() { }

    // Start is called before the first frame update
    void Start()
    {
        BuildLevel(file);
    }

    void BuildLevel(TextAsset levelFile)
    {
        List<string> rows = new List<string>(file.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        rowCount = rows.Count - 1;
        columnCount = rows[0].Length;

        Vector3 tilePos = Vector3.zero;

        for (int z = 0; z < rows.Count - 1; ++z)
        {
            for (int x = 0; x < rows[z].Length; ++x)
            {
                if (!rows[z][x].Equals(k_EMPTY_TILE))
                {
                    GameObject cloneTile = (GameObject)Instantiate(tilePrefab, tilePos, Quaternion.identity, transform);

                    cloneTile.transform.localScale = tileScale;

                    cloneTile.GetComponent<GridTile>().SetWalls(GetNeighborsCoords(x, z, rows));

                    #region PLayer Start
                    if (rows[z][x].Equals(k_START))
                    {
                        playerStartGO.transform.position = tilePos;
                        playerStartGO.transform.Translate(0, playerYOffset, 0);

                        if (rows[rows.Count - 1][0].Equals(k_SOUTH))
                        {
                            playerStartGO.transform.Rotate(Vector3.up, 180f);
                        }
                        else if (rows[rows.Count - 1][0].Equals(k_EAST))
                        {
                            playerStartGO.transform.Rotate(Vector3.up, 90f);
                        }
                        else if (rows[rows.Count - 1][0].Equals(k_WEST))
                        {
                            playerStartGO.transform.Rotate(Vector3.up, 270f);
                        }
                        else if (!rows[rows.Count - 1][0].Equals(k_NORTH))
                        {
                            Debug.LogError("Bad player start facing");
                        }

                        startTile = cloneTile.GetComponent<GridTile>();
                    }
                    #endregion
                    #region Exit
                    else if (rows[z][x].Equals(k_EXIT))
                    {
                        GameObject cloneUI = (GameObject)Instantiate(exitUiPrefab, cloneTile.transform);

                        cloneUI.transform.localPosition = Vector3.zero;
                        cloneUI.transform.localScale = Vector3.one;

                        if (rows[rows.Count - 1][1].Equals(k_SOUTH))
                        {
                            cloneUI.transform.Rotate(Vector3.up, 180f);
                        }
                        else if (rows[rows.Count - 1][1].Equals(k_EAST))
                        {
                            cloneUI.transform.Rotate(Vector3.up, 90f);
                        }
                        else if (rows[rows.Count - 1][1].Equals(k_WEST))
                        {
                            cloneUI.transform.Rotate(Vector3.up, 270f);
                        }
                        else if (!rows[rows.Count - 1][1].Equals(k_NORTH))
                        {
                            Debug.LogError("Bad exit facing");
                        }

                        cloneTile.GetComponent<GridTile>().Ui = cloneUI;
                    }
                    #endregion

                    tiles.Add(new TilePosition(x, z), cloneTile.GetComponent<GridTile>());
                }

                tilePos.x += tileScale.x;
            }

            tilePos.x = 0;
            tilePos.z -= tileScale.z;
        }
    }

    CardinalCoordinates GetNeighborsCoords(int col, int row, List<string> rows)
    {
        CardinalCoordinates neighbors = 0;

        //	Check North
        if (row == 0 || rows[row - 1][col].Equals(k_EMPTY_TILE))
        {
            neighbors = neighbors | CardinalCoordinates.North;
        }

        //	Check South
        if (row == rows.Count - 2 || rows[row + 1][col].Equals(k_EMPTY_TILE))
        {
            neighbors = neighbors | CardinalCoordinates.South;
        }

        //	Check East
        if (col == rows[row].Length - 1 || rows[row][col + 1].Equals(k_EMPTY_TILE))
        {
            neighbors = neighbors | CardinalCoordinates.East;
        }

        //	Check West
        if (col == 0 || rows[row][col - 1].Equals(k_EMPTY_TILE))
        {
            neighbors = neighbors | CardinalCoordinates.West;
        }

        return neighbors;
    }

    public Vector3 GetLevelCenter()
    {
        Vector3 centerPos;

        centerPos.x = (rowCount / 2f * tileScale.x) - (tileScale.x / 2f);
        centerPos.y = tileScale.y;
        centerPos.z = -(columnCount / 2f * tileScale.z) + (tileScale.z / 2f);

        return centerPos;
    }
}
