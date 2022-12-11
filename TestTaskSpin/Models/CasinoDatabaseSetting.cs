namespace TestTaskSpin.Models
{
    public class CasinoDatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string MachineCollectionName { get; set; } = null!;

        public string PlayerBalanceCollectionName { get; set; } = null!;
    }
}
