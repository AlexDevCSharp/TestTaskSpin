using TestTaskSpin.Models;

namespace TestTaskSpin.Services
{
    public interface IMachineService
    {
        Task<SpinResponse> SpinAsync(Bet bet, string machineId);

        Task UpdateMachineAsync(Machine machine);

        Task UpdatePlayerBalanceAsync(string playerId, int amount);

        Task CreateMachineAsync(Machine machine);

        Task CreateBalanceAsync(PlayerBalance balance);
    }
}
