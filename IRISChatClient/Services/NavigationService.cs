using System;
using System.Collections.Generic;
using IRISChatClient.Interfaces;
using Windows.UI.Xaml.Controls;

namespace IRISChatClient.Services
{
    public class NavigationService : INavigationService
    {
        #region "Fields"
        private Frame currentFrame;
        private Dictionary<Type, Type> viewMapping;
        #endregion

        #region "Properties"
        public Frame CurrentFrame
        {
            get
            {
                return currentFrame;
            }
        }

        public bool CanGoBack
        {
            get
            {
                return CurrentFrame != null && CurrentFrame.CanGoBack;
            }
        }
        
        public IReadOnlyDictionary<Type, Type> ViewMapping
        {
            get
            {
                return viewMapping ?? null;
            }
        }
        #endregion

        #region "Constructors"
        public NavigationService()
        {
            Initialize();
        }
        public NavigationService(Frame currentFrame)
        {
            this.currentFrame = currentFrame;
            Initialize();
        }
        #endregion

        #region "Private Methods"
        /// <summary>
        /// Used to initialize the view mapping dictionary when the <see cref="NavigationService"/> is initialized.
        /// </summary>
        private void Initialize()
        {
            viewMapping = new Dictionary<Type, Type>();
        }
        #endregion

        #region "Public Methods"
        public void RegisterView(Type ViewModelType, Type ViewType)
        {
            if (viewMapping != null && viewMapping.ContainsKey(ViewModelType) == false)
            {
                viewMapping.Add(ViewModelType, ViewType);
            }
        }

        public void Unregister(Type ViewModelType)
        {
            if (viewMapping != null && viewMapping.ContainsKey(ViewModelType))
            {
                viewMapping.Remove(ViewModelType);
            }
        }

        public void UnregisterAll()
        {
            if (viewMapping != null)
            {
                foreach (Type ViewModelType in viewMapping.Keys)
                {
                    viewMapping.Remove(ViewModelType);
                }
            }
        }

        public void SetCurrentFrame(Frame currentFrame)
        {
            this.currentFrame = currentFrame;
        }

        public void GoBack()
        {
            CurrentFrame.GoBack();
        }

        public void Navigate<ViewModelType>(object args = null)
        {
            CurrentFrame.Navigate(viewMapping[typeof(ViewModelType)], args);
        }
        #endregion
    }
}