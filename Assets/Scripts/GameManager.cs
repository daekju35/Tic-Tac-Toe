using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CellStatus lastChoice;
    [SerializeField]
    private Cell[] _cells;
    private CellStatus[,] _gridStatus = new CellStatus[3,3];
    private int _numberOfWinsCircle;
    private int _numberOfWinsCross;
    [SerializeField]
    private TextMeshProUGUI _numberOfWinsText;
    [SerializeField]
    private GameObject _restartButton;
    [SerializeField]
    private GameObject _winnerText;
    [SerializeField]
    private TextMeshProUGUI _winnerTMP;


    [SerializeField]
    private GameObject _grid;
    [SerializeField]
    private GameObject _gameArea;
    private bool _isGameOver;
    private int _numberOfMoves;

    private void Awake()
    {
        _winnerTMP.text = "";
        _winnerText.SetActive(false);   
        _restartButton.SetActive(false);
        _gameArea.SetActive(true);
        _grid.SetActive(true);
    }

    void Start()
    {
        _numberOfWinsCircle = PlayerPrefs.GetInt("WINS_CIRCLE");
        _numberOfWinsCross = PlayerPrefs.GetInt("WINS_CROSS");
        foreach (Cell cell in _cells)
        {
            cell.onCellStatusPressed += SelectedCell;
            cell.onCellStatusChanged += CellLastChoice;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CellLastChoice(CellStatus status)
    {
        lastChoice = status;
    }
    private void SelectedCell(Cell cell)
    {
        var newStatus = lastChoice != CellStatus.Circle ? CellStatus.Circle : CellStatus.Cross;
        cell.ChangeState(newStatus);
        _gridStatus[cell.x, cell.y] = newStatus;
        if (CheckWin(newStatus) == CellStatus.None)
        {
            _numberOfMoves++;
            if (_numberOfMoves >= 9)
            {
                GameOver("Draw");
            }
        }
    }
    public CellStatus CheckWin(CellStatus status)
    {
        
        for (int i = 0; i < 3; i++)
        {
            if (_gridStatus[i, 0] == status && _gridStatus[i, 1] == status && _gridStatus[i, 2] == status)
            {
                SetWin(status);
                return status;
            }
        }
        for (int j = 0; j < 3; j++)
        {
            if (_gridStatus[0, j] == status && _gridStatus[1, j] == status && _gridStatus[2, j] == status)
            {
                SetWin(status);
                return status;
            }
        }
        if (_gridStatus[0, 0] == status && _gridStatus[1, 1] == status && _gridStatus[2, 2] == status)
        {
            SetWin(status);
            return status;
        }
        if (_gridStatus[2, 0] == status && _gridStatus[1, 1] == status && _gridStatus[0, 2] == status)
        {

            SetWin(status);
            return status;
        }


        return CellStatus.None;

    }

    private void SetWin(CellStatus newStatus)
    {
        if (_isGameOver == true)
        {
            return;
        }
        if (newStatus != CellStatus.None)
        {
            if (newStatus == CellStatus.Cross)
            {
                _numberOfWinsCross++;
                GameOver("Cross");
                PlayerPrefs.SetInt("WINS_CROSS", _numberOfWinsCross);
                PlayerPrefs.Save();
            }
            else
            {
                _numberOfWinsCircle++;
                GameOver("Circle");
                PlayerPrefs.SetInt("WINS_CIRCLE", _numberOfWinsCircle);
                PlayerPrefs.Save(); 
            }
        }
        else
        {
            GameOver("Draw");
        }
    }

    private void GameOver(string winner)
    {
        _isGameOver = true;
        _numberOfWinsText.text = $"Circle: {_numberOfWinsCircle} Cross: {_numberOfWinsCross}";
        _restartButton.SetActive(true);
        _grid.SetActive(false);
        _gameArea.SetActive(false);
        _winnerText.SetActive(true);
        _winnerTMP.text = winner;
        
    }

 


}
