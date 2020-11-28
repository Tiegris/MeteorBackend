using System;
using System.Data.Common;
using Schemes.Domain.Results;

namespace Schemes.Dal
{
    internal static class Converters
    {
        //public static Scheme ToDomain(this SchemeResult value)
        //    => new SchemeResult(value);


        public static SchemeResult ParseScheme(DbDataReader reader) {
            return new SchemeResult(
                Convert.ToInt32(reader["scheme_id"]),
                Convert.ToString(reader["name"]),
                Convert.ToInt32(reader["version"]),
                Convert.ToDateTime(reader["dt"]),
                Convert.ToString(reader["swarm_1"]),
                Convert.ToString(reader["swarm_2"]),
                Convert.ToString(reader["swarm_3"]),
                Convert.ToString(reader["swarm_4"]),
                Convert.ToString(reader["swarm_5"]),
                Convert.ToString(reader["swarm_6"])
            );
        }
    }
}
