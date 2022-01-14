using System;
using System.Threading;
using IRISChatClient.Interfaces;

namespace IRISChatClient.Networking
{
    public class Response : IResponse, IDisposable
    {
        #region "Properties"
        public DateTime LastSeen { get; set; }
        public Type ExpectedMessageType { get; set; }
        public IMessage Result { get; set; }
        public bool IsTimedout { get; set; }
        public ManualResetEvent Handler { get; set; }
        #endregion

        #region "Constructors"
        public Response()
        {
            LastSeen = DateTime.Now;
            ExpectedMessageType = null;
            Result = null;
            IsTimedout = false;
            Handler = new ManualResetEvent(false);
        }
        public Response(DateTime lastSeen, Type expectedMessageType)
        {
            LastSeen = lastSeen;
            ExpectedMessageType = expectedMessageType;
            Result = null;
            IsTimedout = false;
            Handler = new ManualResetEvent(false);
        }
        #endregion

        #region "Disposable"
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}