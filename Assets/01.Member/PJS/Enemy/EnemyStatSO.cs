using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStat", menuName = "SO/Enemy/Stat")]
public class EnemyStatSO : ScriptableObject
{
    public float speed;
    public float health;
    public bool flying;
    public bool hasSkill;
}
