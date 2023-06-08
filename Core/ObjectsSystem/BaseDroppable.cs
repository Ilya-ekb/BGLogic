using System;
using Core.Locations.Model;
using Core.Timers;
using UnityEngine;

namespace Core.ObjectsSystem
{
    public abstract class BaseDroppable : IDroppable
    {
        public string Name { get; protected set; }
        public bool Alive { get; protected set; }
        public event Action<IDroppable> Dropped;

        protected Location location;

        protected BaseDroppable()
        {
            Name = GetType().Name;
        }
        
        public void SetAlive(Location location = null)
        {
            this.location = location;
            Alive = true;
            OnAlive();
        }
        
        public void Drop()
        {
            if (!Alive)
            {
                return;
            }

            OnDrop();
            Alive = false;
            Dropped?.Invoke(this);
            Dropped = null;
        }

        protected virtual void OnAlive()
        {
        }

        protected virtual void OnDrop()
        {
        }
    }
}