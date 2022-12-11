using Microsoft.AspNetCore.Mvc;
using TestTaskSpin.Models;
using TestTaskSpin.Services;

namespace TestTaskSpin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly ILogger<MachineController> _logger;
        private readonly IMachineService _machineService;

        public MachineController(ILogger<MachineController> logger, IMachineService machineService)
        {
            _logger = logger;
            _machineService = machineService;
        }

        [HttpPost]
        [Route("spin/{machineId}")]
        [ProducesResponseType(typeof(SpinResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<SpinResponse>> SpinAsync(Bet bet, string machineId)
        {
            var result = await _machineService.SpinAsync(bet, machineId);

            return result;
        }

        [HttpPut]
        [Route("update/balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePlayerBalanceAsync(string playerId, int amount)
        {
            await _machineService.UpdatePlayerBalanceAsync(playerId, amount);

            return Ok();
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMachineAsync(Machine machine)
        {
            await _machineService.UpdateMachineAsync(machine);

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateMachineAsync(int length)
        {
            var machine = new Machine(length);
            await _machineService.CreateMachineAsync(machine);

            return Ok();
        }

        [HttpPost]
        [Route("create/balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateBalanceAsync(string playerId)
        {
            var balance = new PlayerBalance(playerId, 0);

            await _machineService.CreateBalanceAsync(balance);

            return Ok();
        }
    }
}
