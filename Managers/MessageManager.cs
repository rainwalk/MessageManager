using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    private static MessageManager instance;

    public static MessageManager Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject("MessageManager");
                instance = go.AddComponent<MessageManager>();
            }
            return instance;
        }
    }

    private Queue<MessageData> messageQueue = new Queue<MessageData>();
    private Dictionary<MessageType, MemPool<MessageData>> messagePools = new Dictionary<MessageType, MemPool<MessageData>>();

    private void Awake() {
        InitializeMessagePools();
        DontDestroyOnLoad(gameObject);
    }

    private void InitializeMessagePools() {
        messagePools[MessageType.ProcessParam] = new MemPool<MessageData>();
        messagePools[MessageType.ExecuteDelayDelegate] = new MemPool<MessageData>();
        messagePools[MessageType.None] = new MemPool<MessageData>();
    }

    public void AddMessage(MessageData messageData, bool isNetworkMessage, bool shouldSave) {
        if (messageData == null) return;

        if (shouldSave) {
            // 저장 로직
        }
        else {
            messageQueue.Enqueue(messageData);
        }
    }

    public MessageData GetMessage() {
        if (messageQueue.Count > 0) {
            return messageQueue.Dequeue();
        }
        return null;
    }

    public void CompleteMessage(MessageData messageData, bool shouldDelete) {
        if (shouldDelete) {
            ReleaseMessageResources(messageData);
        }
        else {
            messageQueue.Enqueue(messageData);
        }
    }

    private void ReleaseMessageResources(MessageData messageData) {
        if (messagePools.TryGetValue(messageData.MessageType, out var pool)) {
            pool.MemFree(messageData);
        }
    }

    public void ProcessMessages() {
        while (true) {
            var message = GetMessage();
            if (message == null) break;

            if (message is MessageProcessFireball fireballMessage) {
                Enemy enemy = FindObjectsOfType<Enemy>().FirstOrDefault(e => e.GetInstanceID() == fireballMessage.EnemyId);
                if (enemy != null) {
                    enemy.TakeDamage(fireballMessage.Damage);
                }
            }
            else if (message is MessageProcessExecuteDelay delayMessage) {
                StartCoroutine(ExecuteDelayedAction(delayMessage.ActionToExecute, delayMessage.DelayTime));
            }

            message.SendInfo();
            CompleteMessage(message, true);
        }
    }

    private IEnumerator ExecuteDelayedAction(System.Action action, float delay) {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
    void Update() {
        ProcessMessages();
    }
}
