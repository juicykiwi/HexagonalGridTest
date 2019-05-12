using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonalGridControl : MonoBehaviour
{
    [SerializeField]
    int _gridColumn = 1;

    [SerializeField]
    int _gridRow = 1;

    [SerializeField]
    Vector2 _tileSize = new Vector2(32f, 32f);

    [SerializeField]
    HexagonalTile _tileTemplate = null;

    private void Awake()
    {
        _tileTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateGrid(_gridColumn, _gridRow);
        UpdateGridPositionToCenter();
    }

    private void CreateGrid( int column, int row )
    {
        HexagonalTileFactory tileFactory = new HexagonalTileFactory();
        tileFactory.TileSize = _tileSize;
        tileFactory.TileParent = gameObject;
        tileFactory.TemplateTile = _tileTemplate.gameObject;

        for (int columnIndex = 0; columnIndex < column; ++columnIndex)
        {
            for (int rowIndex = 0; rowIndex < row; ++rowIndex)
            {
                HexagonalTile tile = tileFactory.CreateTile(columnIndex, rowIndex);
                if(tile == null)
                {
                    continue;
                }

                tile.SetClickDelegate(HexagonalGridView.Instance.TileClickDelegate);
            }
        }
    }

    private void UpdateGridPositionToCenter()
    {
        float posX = (float)(_gridColumn) * -0.5f * _tileSize.x + (_tileSize.x * 0.5f);
        float posY = (float)(_gridRow) * -0.5f * _tileSize.y + (_tileSize.y * 0.5f);
        transform.localPosition = new Vector3(posX, posY);
    }
}
