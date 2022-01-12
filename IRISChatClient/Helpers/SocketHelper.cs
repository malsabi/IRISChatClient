using IRISChatClient.Configs;
using System;

namespace IRISChatClient.Helpers
{
    public class SocketHelper
    {
        public static byte[] AppendHeader(byte[] Packet)
        {
            byte[] Message = new byte[Packet.Length + Constants.HEADER_SIZE];
            Buffer.BlockCopy(BitConverter.GetBytes(Packet.Length), 0, Message, 0, (int)Constants.HEADER_SIZE);
            Buffer.BlockCopy(Packet, 0, Message, (int)Constants.HEADER_SIZE, Packet.Length);
            return Message;
        }
    }
}