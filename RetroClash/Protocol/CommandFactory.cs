using System;
using System.Collections.Generic;
using RetroClash.Protocol.Commands.Client;

namespace RetroClash.Protocol
{
    public class CommandFactory
    {
        public static Dictionary<int, Type> Commands;

        public CommandFactory()
        {
            Commands = new Dictionary<int, Type>
            {
                {500, typeof(LogicBuyBuilding)},
                {501, typeof(LogicMoveBuilding)},
                {502, typeof(LogicUpgradeBuilding)},
                {503, typeof(LogicSellBuilding)},
                {504, typeof(LogicSpeedUpConstruction)},
                {507, typeof(LogicClearObstacle)},
                {508, typeof(LogicTrainUnit)},
                {510, typeof(LogicBuyTrap)},
                {512, typeof(LogicBuyDeco)},
                //{513, typeof()},
                {516, typeof(LogicUnitUpgrade)},
                {517, typeof(LogicSpeedUpUnitUpgrade)},
                {522, typeof(LogicBuyShield)},
                //{523, typeof(LogicClaimAchievementReward)},
                //{524, typeof()},
                {532, typeof(LogicNewShopItemsSeen)},
                {533, typeof(LogicMoveMultipleBuildings)},
                {534, typeof(LogicDisbandLeague)},
                {538, typeof(LogicLeagueNotificationsSeen)},
                {539, typeof(LogicNewsSeen)},
                {544, typeof(LogicEditModeShown)},
                {600, typeof(LogicPlaceAttacker)},
                {603, typeof(LogicEndCombat)},
                {604, typeof(LogicCastSpell)},
                {700, typeof(LogicMatchmaking)},
                {701, typeof(LogicCommandFailed)}
            };
        }
    }
}