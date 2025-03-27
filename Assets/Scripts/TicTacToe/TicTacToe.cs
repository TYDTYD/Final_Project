using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
public class TicTacToe : MonoBehaviour
{
    [SerializeField] List<Image> oSet;
    [SerializeField] List<Image> xSet;
    [SerializeField] List<Image> TileSet;

    Color selectColor = new Color(191f/ 255f, 191f/ 255f, 191f/ 255f);
    Color normalColor = new Color(1f, 1f, 1f);
    int pos = 0;
    bool turn = true;
    bool[] isVisited = { false, false, false, false, false, false, false, false, false };
    void Update()
    {
        if (isDraw())
        {
            gameObject.SetActive(false);
            return;
        }
        if (IsBingo())
        {
            gameObject.SetActive(false);
            return;
        }

        TileSet[pos].color = normalColor;
        IsMyTurn();
        if (IsBingo())
        {
            gameObject.SetActive(false);
            return;
        }
        MoveCursor();
        TileSet[pos].color = selectColor;
        if (isVisited[pos])
            return;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            isVisited[pos] = true;
            oSet[pos].enabled = true;
            turn = false;
        }
    }
    void IsMyTurn()
    {
        if (!turn)
        {
            IsYourTurn();
            turn = true;
        }
    }
    bool isDraw()
    {
        int sum = 0;
        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                if (isVisited[i * 3 + j])
                    sum++;
            }
        }
        return (sum == 9);
    }
    bool IsBingo()
    {
        int rowCountX = 0;
        int rowCountO = 0;
        int colCountX = 0;
        int colCountO = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (oSet[i * 3 + j].enabled)
                    rowCountO++;
                if (xSet[i * 3 + j].enabled)
                    rowCountX++;
                if (oSet[j * 3 + i].enabled)
                    colCountO++;
                if (xSet[j * 3 + i].enabled)
                    colCountX++;
            }
            if (rowCountO == 3 || rowCountX == 3)
                return true;
            if (colCountO == 3 || colCountX == 3)
                return true;
            rowCountO = 0;
            rowCountX = 0;
            colCountO = 0;
            colCountX = 0;
        }
        if (oSet[0].enabled && oSet[4].enabled && oSet[8].enabled)
            return true;
        if (oSet[2].enabled && oSet[4].enabled && oSet[6].enabled)
            return true;
        if (xSet[0].enabled && xSet[4].enabled && xSet[8].enabled)
            return true;
        if (xSet[2].enabled && xSet[4].enabled && xSet[6].enabled)
            return true;
        return false;
    }
    void IsYourTurn()
    {
        int rowCountO = 0;
        int colCountO = 0;
        int rowIndex = 0;
        int colIndex = 0;

        if (!isVisited[4])
        {
            xSet[4].enabled = true;
            isVisited[4] = true;
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (oSet[i * 3 + j].enabled)
                    rowCountO++;
                else
                    rowIndex = i * 3 + j;
                if (oSet[j * 3 + i].enabled)
                    colCountO++;
                else
                    colIndex = j * 3 + i;
            }
            if (rowCountO == 2 && !isVisited[rowIndex])
            {
                xSet[rowIndex].enabled = true;
                isVisited[rowIndex] = true;
                return;
            }
            if (colCountO == 2 && !isVisited[colIndex])
            {
                xSet[colIndex].enabled = true;
                isVisited[colIndex] = true;
                return;
            }   
            rowCountO = 0;
            colCountO = 0;
        }

        int rowCountX = 0;
        int colCountX = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (xSet[i * 3 + j].enabled)
                    rowCountX++;
                else
                    rowIndex = i * 3 + j;
                if (xSet[j * 3 + i].enabled)
                    colCountX++;
                else
                    colIndex = j * 3 + i;
            }
            if (rowCountX == 2 && !isVisited[rowIndex])
            {
                xSet[rowIndex].enabled = true;
                isVisited[rowIndex] = true;
                return;
            }
            if (colCountX == 2 && !isVisited[colIndex])
            {
                xSet[colIndex].enabled = true;
                isVisited[colIndex] = true;
                return;
            }
            rowCountX = 0;
            colCountX = 0;
        }

        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                if (isVisited[i * 3 + j])
                    continue;
                xSet[i * 3 + j].enabled = true;
                isVisited[i * 3 + j] = true;
                return;
            }
        }
        return;
    }
    void MoveCursor()
    {
        bool rightPress = Input.GetKeyDown(KeyCode.RightArrow);
        bool leftPress = Input.GetKeyDown(KeyCode.LeftArrow);
        bool upPress = Input.GetKeyDown(KeyCode.UpArrow);
        bool downPress = Input.GetKeyDown(KeyCode.DownArrow);

        if (rightPress)
        {
            pos++;
            if (pos >= 9)
            {
                pos = 0;
            }
        }
        else if (leftPress)
        {
            pos--;
            if (pos < 0)
            {
                pos = 8;
            }
        }
        else if (upPress)
        {
            if (pos - 3 < 0)
                pos += 6;
            else
                pos -= 3;
        }
        else if(downPress)
        {
            if (pos + 3 > 8)
                pos -= 6;
            else
                pos += 3;
        }
    }
}
