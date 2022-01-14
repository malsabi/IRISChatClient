using IRISChatClient.Views.Scenarios;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IRISChatClient.Models
{
    /// <summary>
    /// Represents an observable collection for the list box.
    /// NOTE: Using Clear will clear the buffer and will cause an handled exception,
    /// so it's better to use "ClearItems" that will only remove the items that are
    /// inserted.
    /// </summary>
    public class ScenariosModel : ObservableCollection<ScenarioModel>
    {
        #region "Fields"
        private List<ScenarioModel> TempScenarios;
        #endregion

        #region "Constructors"
        public ScenariosModel()
        {
            Initialize();
        }
        #endregion

        #region "Events / Handlers"
        public delegate void OnAddSignedOutItemsEvent();
        public event OnAddSignedOutItemsEvent OnAddSignedOutItems;
        private void SetOnAddSignedOutItems()
        {
            OnAddSignedOutItems?.Invoke();
        }

        public delegate void OnAddSignedInItemsEvent();
        public event OnAddSignedOutItemsEvent OnAddSignedInItems;
        private void SetOnAddSignedInItems()
        {
            OnAddSignedInItems?.Invoke();
        }
        #endregion

        #region "Private Methods"
        private void Initialize()
        {
            TempScenarios = new List<ScenarioModel>()
            {
                new ScenarioModel() { Title = "General", ClassType = typeof(SignInPage) },
                new ScenarioModel() { Title = "Friends List", ClassType = typeof(FriendsListPage) },
                new ScenarioModel() { Title = "Global Chat", ClassType = typeof(FriendsListPage) }
            };
            foreach (ScenarioModel Scene in TempScenarios)
            {
                Add(Scene);
            }
        }
        #endregion
    }
}