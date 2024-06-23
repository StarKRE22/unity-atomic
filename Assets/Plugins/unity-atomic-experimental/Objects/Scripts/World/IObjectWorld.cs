using System;
using System.Collections.Generic;

namespace Atomic.Objects
{
    public interface IObjectWorld : IDisposable
    {
        event Action<IObject> OnObjectSpawned;
        event Action<IObject> OnObjectUnspawned;
       
        bool Constructed { get; }
        bool Enabled { get; }
        
        IReadOnlyList<IObject> Objects { get; }
        IObject GetObject(int objectId);
        IObject SpawnObject(string name = null, bool enabled = true, bool autoRun = true);
        bool UnspawnObject(int objectId);
        bool UnspawnObject(IObject obj);
        
        IObject GetObjectWithTag(int tag);
        IObject[] GetObjectsWithTag(int tag);
        //TODO: GetObjectsWithTagNonAlloc(int tag)
        
        void Construct();
        void ConstructObject(IObject obj);
        
        void Enable();
        void OnEnableObject(IObject obj);
        
        void Disable();
        void OnDisableObject(IObject obj);
        
        void Tick(float deltaTime);
        void FixedTick(float deltaTime);
        void LateTick(float deltaTime);
    }
}