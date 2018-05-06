using System;
using System.Collections.Generic;
using RetroClash.Protocol.Messages.Client;

namespace RetroClash.Protocol
{
    public class MessageFactory
    {
        public static Dictionary<int, Type> Messages;

        public MessageFactory()
        {
            Messages = new Dictionary<int, Type>
            {
                {10101, typeof(LoginMessage)},
                {10108, typeof(KeepAlive)},
                {10113, typeof(SetDeviceTokenMessage)},
                {10212, typeof(ChangeAvatarName)},
                {14101, typeof(GoHome)},
                {14102, typeof(EndClientTurn)},
                {14113, typeof(VisitHome)},
                {14134, typeof(AttackNpc)},
                {14262, typeof(BindGoogleServiceAccount)},
                {14301, typeof(CreateAlliance)},
                {14302, typeof(AskForAllianceData)},
                {14303, typeof(AskForJoinableAlliancesList)},
                {14325, typeof(AskForAvatarProfileMessage)},
                {14401, typeof(AskForAllianceRankingList)},
                {14403, typeof(AskForAvatarRankingList)},
                {14404, typeof(AskForAvatarLocalRankingList)},
                {14715, typeof(SendGlobalChatLine)}
            };
        }
    }
}