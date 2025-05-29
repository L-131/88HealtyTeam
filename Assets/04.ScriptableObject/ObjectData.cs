using UnityEngine;
using System;

public enum ObjectColor
{
    RED,
    BLUE,
    GREEN,
    YELLOW,
}

[CreateAssetMenu(fileName = "Object", menuName = "Objects/Object")]
public class ObjectData : ScriptableObject
{
    public string objectID;
    public string objectName;
    public ItemColor objectColor;
    public string description;

#if UNITY_EDITOR
    // �����Ϳ��� ���� �ٲ� �� �ڵ� ȣ���
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(objectID))
        {
            objectID = Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this); // ������� ���� ǥ��
        }
    }
#endif
}
