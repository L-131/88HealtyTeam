using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleLight : MonoBehaviour
{
    public bool isLightON = false;
    public void LightsUpAndDown()
    {
        isLightON = !isLightON; // ����Ʈ�� Ű�ų� ���� �Ұ�
        PuzzleManager.Instance.TurnSideLightS(this);
    }
}
