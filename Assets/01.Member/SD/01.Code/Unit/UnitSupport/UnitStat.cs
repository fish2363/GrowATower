using _01.Member.SD._01.Code.Unit.Interface.UnitSupport;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStat", menuName = "SO/Unit/UnitStat")]
public class UnitStat : ScriptableObject
{
   public string unitName;
   public UnitType type;
   public GameObject unitPrefab;
   public float actionSpeed;
   public float rotationSpeed;
}
