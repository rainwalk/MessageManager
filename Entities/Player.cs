using UnityEngine;
public enum PlayerSkillState
{
    Ready,
    Cooldown,
}

public class Player : MonoBehaviour
{
    public PlayerSkillState skillState = PlayerSkillState.Ready;
    public int damage = 10;

    public void UseFireballSkill(Enemy target) {
        if (skillState == PlayerSkillState.Ready) {
            int enemyId = target.GetInstanceID();
            var fireballMessage = new MessageProcessFireball(enemyId, damage); // 10�� ���ظ� �ִ� ���̾

            MessageManager.Instance.AddMessage(fireballMessage, false, false);

            skillState = PlayerSkillState.Cooldown;

            var cooldownMessage = new MessageProcessExecuteDelay(() => {
                skillState = PlayerSkillState.Ready;
                Debug.Log("���̾�� ���� �� �� �ֽ��ϴ�");
            }, 2f);

            MessageManager.Instance.AddMessage(cooldownMessage, false, false);
        }
        else {
            Debug.Log("��ų ��Ÿ�����Դϴ�");
        }
    }
}
