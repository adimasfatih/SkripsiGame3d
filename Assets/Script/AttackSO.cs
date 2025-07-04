using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Attack/Normal Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    public float damage;
}
