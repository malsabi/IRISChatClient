using System;
using System.ComponentModel;

namespace IRISChatClient.Models
{
    public class ScenarioModel : INotifyPropertyChanged
    {
        #region "Fields"
        private string title;
        private Type classType;
        #endregion

        #region "Properties"
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisPropertyChanged("Title");
            }
        }
        public Type ClassType
        {
            get
            {
                return classType;
            }
            set
            {
                classType = value;
                RaisPropertyChanged("ClassType");
            }
        }
        #endregion

        #region "Events / Handlers"
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion

        #region "Overrided Methods"
        public override string ToString()
        {
            return Title;
        }
        #endregion
    }
}