using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public static PuzzleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("PuzzleManager").AddComponent<PuzzleManager>();
            }
            return instance;
        }
    }

    public PuzzleLight[] getPuzzles; // PuzzleLight�� ������ �ִ� �ڽ� ������Ʈ�� ��� ���� �迭

    public PuzzleLight[,] puzzles = new PuzzleLight[5,5]; // �� �迭 ���� ������Ʈ�� ������ ���·� ��� ���� �迭

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        getPuzzles = GetComponentsInChildren<PuzzleLight>(); // ó������ 2���� �迭�� ���� �� ���� �켱 1���� �迭�� �Ҵ�
    }

    private void Start()
    {
        int k = 0;
        // 1���� �迭�� ��� �ڽĵ�(����Ʈ)�� 2���� �迭�� �ű�
        for (int i = 0; i< 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                puzzles[i, j] = getPuzzles[k++];
            }
        }
    }

    private void Update()
    {
        if(CheckPuzzleClear())
        {
            ClearedPuzzle();
        }
    }


    public void TurnSideLightS(PuzzleLight puzzle) // Ŭ���� ����Ʈ�� �翷 �� ���Ʒ��� Ű�ų� ���� �޼���
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (puzzles[i, j] == puzzle)
                {
                    puzzles[i + 1, j].isLightON = !puzzles[i + 1, j].isLightON;
                    puzzles[i - 1, j].isLightON = !puzzles[i - 1, j].isLightON;
                    puzzles[i , j + 1].isLightON = !puzzles[i, j + 1].isLightON;
                    puzzles[i , j - 1].isLightON = !puzzles[i, j - 1].isLightON;
                }
            }
        }
    }


    public bool CheckPuzzleClear() // ���� Ŭ���� �޼���
    {
        bool isAllOn = true;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                // ����Ʈ�� �ϳ��� ���������� Ŭ���� �ȵ� ����
                if (puzzles[i, j].isLightON == false)
                {
                    isAllOn = false;
                    break;
                }
            }
        }

        if (isAllOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClearedPuzzle() // ������ Ŭ�����ϸ� ���� ������ �޼���
    {
        Debug.Log("���� Ŭ����! ���� �����ϴ�.");
    }


}
