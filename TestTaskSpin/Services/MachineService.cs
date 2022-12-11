using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestTaskSpin.Models;

namespace TestTaskSpin.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMongoCollection<Machine> _machineCollection;

        private readonly IMongoCollection<PlayerBalance> _playerBalanceCollection;

        public MachineService(IOptions<CasinoDatabaseSetting> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);

            _machineCollection = mongoDatabase.GetCollection<Machine>(
                options.Value.MachineCollectionName);
            _playerBalanceCollection = mongoDatabase.GetCollection<PlayerBalance>(
                options.Value.PlayerBalanceCollectionName);
        }

        public async Task<SpinResponse> SpinAsync(Bet bet, string machineId)
        {
            //Get machine
            var machine = await _machineCollection.Find(x => x.Id == machineId).FirstOrDefaultAsync();

            //Get player balance
            var balance = await _playerBalanceCollection.Find(x => x.PlayerId == bet.PlayerId).FirstOrDefaultAsync();

            //new array with length from db
            var reel = new int[machine.ReelLength];
            int sumOfSymbols = 0;
            Random rnd = new Random();
            for (int i = 0; i < machine.ReelLength; i++)
            {
                var random = rnd.Next(0, 9);
                sumOfSymbols += random;
                //Add to array
                reel[i] = random;
            }

            //Calculate win scores
            var winScores = sumOfSymbols * bet.BetAmount;

            //update player balance in db
            balance.CurrentBalance += winScores - bet.BetAmount;
            await _playerBalanceCollection.ReplaceOneAsync(x => x.PlayerId == bet.PlayerId, balance);

            return new SpinResponse(reel, winScores, balance.CurrentBalance);
        }

        public async Task UpdateMachineAsync(Machine machine) =>
            await _machineCollection.ReplaceOneAsync(x => x.Id == machine.Id, machine);

        public async Task UpdatePlayerBalanceAsync(string playerId, int amount)
        {
            //Get player balance
            var balance = await _playerBalanceCollection.Find(x => x.PlayerId == playerId).FirstOrDefaultAsync();

            balance.CurrentBalance += amount;

            await _playerBalanceCollection.ReplaceOneAsync(x => x.Id == balance.Id, balance);

        }

        public async Task CreateMachineAsync(Machine machine) =>
            await _machineCollection.InsertOneAsync(machine);

        public async Task CreateBalanceAsync(PlayerBalance balance) =>
            await _playerBalanceCollection.InsertOneAsync(balance);
    }
}
