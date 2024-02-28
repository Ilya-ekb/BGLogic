using Core.Locations.Model;
using Core.ObjectsSystem;
using GameData;
using UnityEngine;

namespace Core.Locations.View
{
    public abstract class LocationView : BaseDroppable
    {
        public GameObject Root { get; private set; }
        
        protected readonly IContext context;
        private readonly GameObject rootResource;

        protected LocationView(Location location, IContext ctx)
        {
            rootResource = Resources.Load<GameObject>(location.RootObjectResourcesPath);
            context = ctx;
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            if(!rootResource)
                return;
            Root = Object.Instantiate(rootResource);
            Root.name = $"[{GetType().Name}]"+ rootResource.name;
        }
        
        protected override void OnDrop()
        {
            base.OnDrop();
            Object.Destroy(Root);
            Root = null;
        }
    }
}