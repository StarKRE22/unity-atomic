#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace Atomic
{
    //TODO: CUSTOM EDITOR!
    [AddComponentMenu("Atomic/Atomic Entity")]
    [DisallowMultipleComponent]
    public class AtomicEntity : MonoBehaviour, IAtomicEntity
    {
        #region Unity

        protected virtual void Awake()
        {
            if (this.composeOnAwake) 
                this.Compose();
        }

        protected virtual void OnDestroy()
        {
            if (this.disposeOnDestroy) 
                this.Dispose();
        }
        
        protected virtual void Reset()
        {
            this.inflateSources = new Object[] {this};
        }

        protected virtual void OnValidate()
        {
            this.Compose();
        }

        #endregion

        #region Atomic

        public int Id
        {
            get { return this.id; }
        }

        private int id = -1;
        private bool composed;
        
        public bool Is(int index)
        {
            //TODO: IF NOT COMPOSED, COMPOSE!
            return AtomicEntities.IsType(index, this.id);
        }

        public T Get<T>(int index)
        {
            return AtomicEntities.GetValue<T>(index, this.id);
        }

        public void Get<T>(int index, ref T value)
        {
            AtomicEntities.GetValue(index, this.id, ref value);
        }

        public bool TryGet<T>(int index, out T result)
        {
            return AtomicEntities.TryGetValue(index, this.id, out result);
        }

        public bool Put<T>(int index, T value)
        {
            return AtomicEntities.PutValue(index, this.id, value);
        }

        public void Set<T>(int index, T value)
        {
            AtomicEntities.SetValue(index, this.id, value);
        }

        public bool Del(int index)
        {
            return AtomicEntities.DelValue(index, this.id);
        }
        
        public bool Mark(int index)
        {
            return AtomicEntities.AddMarker(index, this.id);
        }

        public bool Unmark(int index)
        {
            return AtomicEntities.RemoveMarker(index, this.id);
        }

        #endregion

        #region Internal
        
        [FoldoutGroup("Installers")]
        [PropertyOrder(80)]
        [SerializeReference]
        private IInstaller[] entityInstallers = default;

        [FoldoutGroup("Advanced")]
        [PropertyOrder(90)]
        [SerializeField]
        private Object[] inflateSources;
        
        [FoldoutGroup("Advanced")]
        [PropertyOrder(90)]
        [SerializeField]
        private bool composeOnAwake = true;

        [FoldoutGroup("Advanced")]
        [PropertyOrder(90)]
        [SerializeField]
        private bool disposeOnDestroy = true;
        
        
        [ContextMenu("Compose")]
        public virtual void Compose()
        {
            this.id = AtomicEntities.NewEntity();

            if (this.inflateSources is {Length: > 0})
            {
                for (int i = 0, count = this.inflateSources.Length; i < count; i++)
                {
                    Object source = this.inflateSources[i];
                    if (source != null)
                    {
                        EntityInflater.InflateFrom(this, source);
                    }
                }
            }

            if (this.entityInstallers is {Length: > 0})
            {
                for (int i = 0, count = this.entityInstallers.Length; i < count; i++)
                {
                    IInstaller installer = this.entityInstallers[i];
                    if (installer != null)
                    {
                        installer.Install(this);
                    }
                }
            }
        }

        [ContextMenu("Dispose")]
        public virtual void Dispose()
        {
            if (this.id != -1)
            {
                AtomicEntities.DelEntity(this.id);
                this.id = -1;
            }
        }

        public interface IInstaller
        {
            void Install(AtomicEntity entity);
        }
        
        #endregion
    }
}