using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void TakeDamage(int damage) {
        Debug.Log($"적이 {damage}의 대미지를 입었습니다.");
    }
}
