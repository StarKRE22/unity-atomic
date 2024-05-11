namespace Atomic.Elements
{
    public delegate void ChangeItemHandler<in T>(int index, T value);
    public delegate void AddItemHandler<in T>(int index, T value);
    public delegate void RemoveItemHandler<in T>(int index, T value);
    
    public delegate void ChangeItemHandler<in K, in V>(K key, V value);
    public delegate void AddItemHandler<in K, in V>(K key, V value);
    public delegate void RemoveItemHandler<in K, in V>(K key, V value);
    
    public delegate void ClearHandler();
}