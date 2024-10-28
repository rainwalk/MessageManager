using System;
using System.Collections.Generic;

public class MemPool<T> where T : MessageData, new()
{
    private readonly Stack<T> pool = new Stack<T>();

    public T MemAlloc() {
        return pool.Count > 0 ? pool.Pop() : new T();
    }

    public void MemFree(T item) {
        pool.Push(item);
    }


}
