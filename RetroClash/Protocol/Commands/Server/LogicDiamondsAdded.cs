using System.Threading.Tasks;
using RetroClash.Extensions;
using RetroClash.Logic;

namespace RetroClash.Protocol.Commands.Server
{
    public class LogicDiamondsAdded : Command
    {
        public LogicDiamondsAdded(Device device) : base(device)
        {
            Id = 7;
        }

        public override async Task Encode()
        {
            await Stream.WriteIntAsync(0); // Free Diamonds
            await Stream.WriteIntAsync(0); // Ammount
            await Stream.WriteStringAsync("G:0"); // TransactionId
        }
    }
}