using System;
using System.Collections.Generic;
using System.Text;
using NLog.Targets;

namespace NlogMongoEx
{
    public class MongoTarget : Target
    {
        private struct MongoConnectionKey : IEquatable<MongoConnectionKey>
        {
            private readonly string ConnectionString;
            private readonly string CollectionName;
            private readonly string DatabaseName;

            public MongoConnectionKey(string connectionString, string collectionName, string databaseName)
            {
                ConnectionString = connectionString ?? string.Empty;
                CollectionName = collectionName ?? string.Empty;
                DatabaseName = databaseName ?? string.Empty;
            }

            public bool Equals(MongoConnectionKey other)
            {
                return ConnectionString == other.ConnectionString
                    && CollectionName == other.CollectionName
                    && DatabaseName == other.DatabaseName;
            }

            public override int GetHashCode()
            {
                return ConnectionString.GetHashCode() ^ CollectionName.GetHashCode() ^ DatabaseName.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is MongoConnectionKey && Equals((MongoConnectionKey)obj);
            }
        }
    }
}
