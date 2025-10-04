using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitSO", menuName = "Scriptable Objects/UnitSO")]
public class UnitStatSO : ScriptableObject
{
   public string unitName;
   public UnitType type;
   public GameObject unitPrefab;
   public float actionSpeed;
   public float rotationSpeed;
}
