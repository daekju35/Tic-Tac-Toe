using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellStatus _cellStatus;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite[] _sprites = new Sprite[3];
    public Action<CellStatus> onCellStatusChanged;
    public Action<Cell> onCellStatusPressed;
    public int x;
    public int y;
    
    // Start is called before the first frame update
    private void Awake()
    {
        ChangeState(CellStatus.None);
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_cellStatus == CellStatus.None)
        {
            onCellStatusPressed?.Invoke(this);
        }
       
        
    }

    public void ChangeState(CellStatus newStatus)
    {
        _cellStatus = newStatus;
        switch (newStatus) { 
              case CellStatus.None:
                _spriteRenderer.sprite = _sprites[0];
                break;
              case CellStatus.Cross:
                _spriteRenderer.sprite = _sprites[1];
                break;
              case CellStatus.Circle:
                _spriteRenderer.sprite = _sprites[2];
                break;
        }
        onCellStatusChanged?.Invoke(newStatus);
    }


}

public enum CellStatus
{
    None,
    Cross,
    Circle,
}
