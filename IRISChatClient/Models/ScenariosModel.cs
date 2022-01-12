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
                new ScenarioModel() { Title = "Login", ClassType = typeof(LoginPage) },
                new ScenarioModel() { Title = "Profile", ClassType = typeof(ProfilePage) },
                new ScenarioModel() { Title = "Friends List", ClassType = typeof(FriendsListPage) }
            };
        }
        #endregion

        #region "Public Methods"
        public void AddSignedOutItems()
        {
            ClearItems();
            Add(new ScenarioModel() { Title = string.Format("1) {0}", TempScenarios[0]), ClassType = TempScenarios[0].ClassType });
            SetOnAddSignedInItems();
        }
        public void AddSignedInItems()
        {
            ClearItems();
            for (int i = 1; i < TempScenarios.Count; i++)
            {
                Add(new ScenarioModel() { Title = string.Format("{0}) {1}", i, TempScenarios[i]), ClassType = TempScenarios[i].ClassType });
            }
            SetOnAddSignedOutItems();
        }
        #endregion
    }
}