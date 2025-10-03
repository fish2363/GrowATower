using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TESTSOList", menuName = "SO/Unit/TESTSOList")]
public class TESTSOList : ScriptableObject
{
    public List<Test> tests = new();
}
