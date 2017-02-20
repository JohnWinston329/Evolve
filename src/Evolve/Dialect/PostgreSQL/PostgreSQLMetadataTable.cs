﻿using Evolve.Connection;
using Evolve.Metadata;
using Evolve.Migration;
using System.Collections.Generic;

namespace Evolve.Dialect.PostgreSQL
{
    public class PostgreSQLMetadataTable : MetadataTable
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="schema"> Existing database schema name. </param>
        /// <param name="tableName"> Metadata table name. </param>
        /// <param name="wrappedConnection"> A connection to the database. </param>
        public PostgreSQLMetadataTable(string schema, string tableName, WrappedConnection wrappedConnection) 
            : base(schema, tableName, wrappedConnection)
        {
        }

        public override void Lock()
        {
            _wrappedConnection.ExecuteNonQuery($"SELECT * FROM \"{Schema}\".\"{TableName}\" FOR UPDATE");
        }

        protected override bool IsExists()
        {
            return _wrappedConnection.QueryForLong($"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{Schema}' AND table_name = '{TableName}'") == 1;
        }

        protected override void Create()
        {
            string sql = $"CREATE TABLE \"{Schema}\".\"{TableName}\" " +
             "( " +
                 "id SERIAL PRIMARY KEY NOT NULL, " +
                 "type SMALLINT, " +
                 "version VARCHAR(50), " +
                 "description VARCHAR(200) NOT NULL, " +
                 "name VARCHAR(300) NOT NULL, " +
                 "checksum VARCHAR(32), " +
                 "installed_by VARCHAR(100) NOT NULL, " +
                 "installed_on TIMESTAMP NOT NULL DEFAULT now(), " +
                 "success BOOLEAN NOT NULL " +
             ")";

            _wrappedConnection.ExecuteNonQuery(sql);
        }

        protected override void InternalSave(MigrationMetadata metadata)
        {
            string sql = $"INSERT INTO \"{Schema}\".\"{TableName}\" (type, version, description, name, checksum, installed_by, success) VALUES" +
             "( " +
                $"'{(int)metadata.Type}', " +
                $"'{metadata.Version.Label}', " +
                $"'{metadata.Description.TruncateWithEllipsis(200)}', " +
                $"'{metadata.Name.TruncateWithEllipsis(1000)}', " +
                $"'{metadata.Checksum}', " +
                $"'', " +
                $"{(metadata.Success ? "true" : "false")}" +
             ")";

            _wrappedConnection.ExecuteNonQuery(sql);
        }

        protected override IEnumerable<MigrationMetadata> InternalGetAllMetadata()
        {
            string sql = $"SELECT id, type, version, description, name, checksum, installed_by, installed_on, success FROM \"{Schema}\".\"{TableName}\"";
            return _wrappedConnection.QueryForList(sql, r =>
            {
                return new MigrationMetadata(r.GetString(2), r.GetString(3), r.GetString(4), (MetadataType)r.GetInt16(1))
                {
                    Id = r.GetInt32(0),
                    Checksum = r.GetString(5),
                    InstalledBy = r.GetString(6),
                    InstalledOn = r.GetDateTime(7),
                    Success = r.GetBoolean(8)
                };
            });
        }
    }
}
