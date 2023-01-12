﻿using BGCore.Game.Factories;
using Core.ObjectsSystem;
using UI.View;
using GameData;
using Game.Settings.UISettings;
using GameLogic.GameData.Contexts;
using UnityEngine;

namespace Game.UI
{
    public abstract class UiElement : BaseDroppable, IUiElement
    {
        public abstract Transform ContentHolder { get; protected set; }

        public bool IsShown { get; private set; }

        public string RootObjectResourcesPath { get; }

        private IUiElement[] ChildUiElements { get; set; }


        protected Transform parent;
        protected readonly UiContext uiContext;
        protected UiElementView view;
        private readonly UISetting setting;


        protected UiElement(UISetting setting, UiContext context)
        {
            uiContext = context;
            uiContext?.SetSelf(this);
            this.setting = setting;
            RootObjectResourcesPath = setting.RootObjectPath;
            view = (UiElementView) GeneralFactory.CreateItem(this, context);
        }

        protected override void OnAlive()
        {
            base.OnAlive();
            Initialize();
            SetAliveChilds();
        }

        public virtual void Show()
        {
            IsShown = true;
            view.Show();
        }

        public virtual void Hide()
        {
            IsShown = false;
            view.Hide();
        }

        public void Update<TUiAgs>(object sender, TUiAgs ags)
        {
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            view?.Drop();

            if (ChildUiElements is { })
            {
                foreach (var childUiElement in ChildUiElements)
                {
                    childUiElement?.Drop();
                }
            }

            ChildUiElements = null;
            view = null;
        }

        protected abstract void Initialize();

        protected virtual void AssignChilds()
        {
            ChildUiElements = new IUiElement[setting.childUiElementSettings.Length];
            for (var i = 0; i < ChildUiElements.Length; i++)
            {
                var childContext = new UiContext().SendParent(this);
                childContext.AddContext(uiContext.GetContext<GeneralContext>());
                ChildUiElements[i] = (IUiElement) GeneralFactory.CreateItem(setting.childUiElementSettings[i], childContext);
            }
        }

        private void SetAliveChilds()
        {
            if (ChildUiElements is null)
            {
                return;
            }

            foreach (var childUiElement in ChildUiElements)
            {
                childUiElement.SetAlive(location);
            }
        }
    }
}