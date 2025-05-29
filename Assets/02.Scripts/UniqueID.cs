// UniqueID.cs (�� C# ��ũ��Ʈ, PickupableItem ������Ʈ�� �ٿ���)
using UnityEngine;

public class UniqueID : MonoBehaviour
{
    public string id;

#if UNITY_EDITOR
    // �����Ϳ��� ���� �ٲ� �� �Ǵ� ������Ʈ�� ������ �� ID �ڵ� ����/����
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id) || UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this))
        {
            // ������ ���¿����� ID�� �������� �ʰų�, ������ �ν��Ͻ�ȭ ������ �����ϵ��� ������ �� ����
            // �����ϰԴ�, ���� ��ġ�� �� ID�� ��������� �׶� ����
            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && gameObject.scene.name != null)
            { // ���� �ִ� ������Ʈ�� ���
                id = System.Guid.NewGuid().ToString();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
    }
    // ������ �ν��Ͻ��� ���� �߰��� �� ID�� ���� �߱޹޵��� �ϴ� ���� (�� ������ �� ����)
    // ���� OnValidate�� ������ ���� ��ü�� ������ �� �����Ƿ� �����ؼ� ����ϰų�,
    // ��Ÿ�ӿ� ID�� �����ϴ� ���� �ý����� �δ� ���� �� ������ �� �ֽ��ϴ�.
    // ���� ������ ����� ���� ��ġ�� �� �������� Inspector���� uniqueID�� �������� �Է��ϰų�,
    // �� OnValidate�� ����ϵ�, �������� �������� �ʵ��� �����ϴ� ���Դϴ�.
#endif
}