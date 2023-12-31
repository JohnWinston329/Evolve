﻿using Npgsql;
using System.Data.Common;
using System.Threading.Tasks;

namespace EvolveDb.Tests.Infrastructure
{
    public class PostgreSqlContainer : IDbContainer
    {
        public const string ExposedPort = "5432";
        public const string HostPort = "5432";
        public const string DbName = "my_database";
        public const string DbPwd = "Password12!";
        public const string DbUser = "postgres";

        private DockerContainer _container;

        public string Id => _container?.Id;
        public string CnxStr => $"Server=127.0.0.1;Port={HostPort};Database={DbName};User Id={DbUser};Password={DbPwd};Pooling=false";
        public int TimeOutInSec => 5;

        public async Task<bool> Start(bool fromScratch = false)
        {
            _container = await new DockerContainerBuilder(new DockerContainerBuilderOptions
            {
                FromImage = "postgres",
                Tag = "alpine",
                Name = "postgres-evolve",
                Env = new[] { $"POSTGRES_PASSWORD={DbPwd}", $"POSTGRES_DB={DbName}" },
                ExposedPort = $"{ExposedPort}/tcp",
                HostPort = HostPort,
                RemovePreviousContainer = fromScratch
            }).Build();

            return await _container.Start();
        }

        public DbConnection CreateDbConnection() => new NpgsqlConnection(CnxStr);
    }
}
