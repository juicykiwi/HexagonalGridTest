// odd-q” vertical layout. shoves odd columns down.
// opposite : even-q” vertical layout. shoves even columns down
#define ODD_Q_VERTICAL_LAYOUT 

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalTileFactory
{
    public Vector2 TileSize { get; set; }

    public GameObject TileParent { get; set; }
    public GameObject TemplateTile { get; set; }

    public HexagonalTile CreateTile(int columnIndex, int rowIndex)
    {
        GameObject newTileObject = NGUITools.AddChild(TileParent, TemplateTile);
        if (newTileObject == null)
        {
            return null;
        }

        HexagonalTile newTile = newTileObject.GetComponent<HexagonalTile>();
        if (newTile == null)
        {
            return null;
        }

        newTile.SetOffsetIndex(columnIndex, rowIndex);
        newTile.SetPosition(columnIndex, rowIndex);
        newTile.UpdateOffsetPosition(TileSize);

        return newTile;
    }
}

public class HexagonalTile : MonoBehaviour
{
    public int PosX { get; private set; }
    public int PosY { get; private set; }
    public int PosZ { get; private set; }

    public int ColumnIndex { get; private set; }
    public int RowIndex { get; private set; }

    [SerializeField]
    UITexture _backgroundImage = null;

    [SerializeField]
    UITexture _selectImage = null;

    [SerializeField]
    Action<HexagonalTile> _clickDelegate = null;

    private void Awake()
    {
        _selectImage.gameObject.SetActive(false);
        _backgroundImage.gameObject.SetActive(true);
    }

    public void SetOffsetIndex(int columnIndex, int rowIndex)
    {
        ColumnIndex = columnIndex;
        RowIndex = rowIndex;
    }

    public void SetPosition(int columnIndex, int rowIndex)
    {
        PosX = columnIndex;
#if ODD_Q_VERTICAL_LAYOUT
        PosY = rowIndex - (columnIndex + (columnIndex & 1)) / 2;
#else
        PosY = rowIndex - (columnIndex - (columnIndex & 1)) / 2;
#endif
        PosZ = -PosX - PosY;
    }

    public void UpdateOffsetPosition(Vector2 tileSize)
    {
        float posX = ColumnIndex * tileSize.x;
        float posY = RowIndex * tileSize.y;

#if ODD_Q_VERTICAL_LAYOUT
        posY -= (ColumnIndex % 2) * (tileSize.y * 0.5f);
#else
        posY += (ColumnIndex % 2) * (tileSize.y * 0.5f);
#endif

        transform.localPosition = new Vector3(posX, posY);
    }

    public void SetClickDelegate(Action<HexagonalTile> clickDelegate)
    {
        _clickDelegate = clickDelegate;
    }

    public void SetSelectImageActive(bool active)
    {
        _selectImage.gameObject.SetActive(active);
    }

    public int Distance(HexagonalTile target)
    {
        if(target == null)
        {
            return -1;
        }

        int distanceX = Mathf.Abs(PosX - target.PosX);
        int distanceY = Mathf.Abs(PosY - target.PosY);
        int distanceZ = Mathf.Abs(PosZ - target.PosZ);
        int resultDistance = Mathf.Max(Mathf.Max(distanceX, distanceY), distanceZ);
        //int resultDistance = (distanceX + distanceY + distanceZ) / 2;

        return resultDistance;
    }

    public void OnTileClick()
    {
        Debug.LogFormat("HexagonalTile.OnClickTile() Offset : {0}, {1} Position : {2}, {3}, {4}"
        , ColumnIndex, RowIndex, PosX, PosY, PosZ);

        if (_clickDelegate != null)
        {
            _clickDelegate(this);
        }
    }
}
