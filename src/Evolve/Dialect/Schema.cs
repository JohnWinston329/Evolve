﻿using Evolve.Connection;
using Evolve.Utilities;

namespace Evolve.Dialect
{
    public abstract class Schema
    {
        protected readonly IWrappedConnection _wrappedConnection;

        public Schema(string schemaName, IWrappedConnection wrappedConnection)
        {
            Name = Check.NotNullOrEmpty(schemaName, nameof(schemaName));
            _wrappedConnection = Check.NotNull(wrappedConnection, nameof(wrappedConnection));
        }

        public string Name { get; }

        public abstract bool IsExists();

        public abstract bool IsEmpty();

        public abstract bool Create();

        public abstract bool Clean();

        public abstract bool Drop();
    }
}
