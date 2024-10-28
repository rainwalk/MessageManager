using UnityEngine;

public class MessageProcessFireball : MessageData
{
    public int EnemyId { get; }
    public int Damage { get; }

    public MessageProcessFireball() : base(MessageType.ProcessParam, 0) { }
    public MessageProcessFireball(int enemyId, int damage) : base(MessageType.ProcessParam, sizeof(int) * 2) {
        EnemyId = enemyId;
        Damage = damage;
    }

    public override void SendInfo() {
        Debug.Log($"���̾�� ��:ID{EnemyId}���� �����Ͽ� {Damage}�� ���ظ� �������ϴ�.");
    }
}
