using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalGridView : MonoBehaviour
{
    static HexagonalGridView _instance = null;
    public static HexagonalGridView Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HexagonalGridView>();
            }

            return _instance;
        }
    }

    bool _firstSelectTileFlag = false;
    HexagonalTile _firstSelectTile = null;
    HexagonalTile _secondSelectTile = null;

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(_instance);
    }

    public void TileClickDelegate(HexagonalTile clickTile)
    {
        if (clickTile == null)
        {
            return;
        }

        _firstSelectTileFlag =! _firstSelectTileFlag;

        if (_firstSelectTileFlag)
        {
            ClearSelectTiles();

            SetFirstSelectTile(clickTile);
        }
        else
        {
            SetSecondSelectTile(clickTile);

            int selectedTileDistance = GetSelectedTilesDistance();
            Debug.LogFormat("Selectd tiles distance : {0}", selectedTileDistance);
        }
    }

    private void ClearSelectTiles()
    { 
        if(_firstSelectTile != null)
        {
            _firstSelectTile.SetSelectImageActive(false);
        }

        if(_secondSelectTile != null)
        {
            _secondSelectTile.SetSelectImageActive(false);
        }
    }

    private void SetFirstSelectTile(HexagonalTile selectTile)
    {
        _firstSelectTile = selectTile;
        _firstSelectTile.SetSelectImageActive(true);
    }

    private void SetSecondSelectTile(HexagonalTile selectTile)
    {
        _secondSelectTile = selectTile;
        _secondSelectTile.SetSelectImageActive(true);
    }

    private int GetSelectedTilesDistance()
    {
        if(_firstSelectTile == null)
        {
            return -1;
        }

        int selectedTileDistance = _firstSelectTile.Distance(_secondSelectTile);
        return selectedTileDistance;
    }
}