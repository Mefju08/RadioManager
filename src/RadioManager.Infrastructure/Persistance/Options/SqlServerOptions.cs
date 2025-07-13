namespace RadioManager.Infrastructure.Persistance.Options
{
    internal sealed class SqlServerOptions
    {
        public const string SectionName = "sqlserver";
        public string ConnectionString { get; set; }
    }
}
