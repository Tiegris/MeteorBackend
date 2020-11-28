using System;
using System.Text.Json.Serialization;

namespace Schemes.Domain.Results
{
    public class SchemeResult
    {
        public SchemeResult(
            int id, string name, 
            int version, DateTime dateTime, 
            string swarm1, string swarm2, 
            string swarm3, string swarm4, 
            string swarm5, string swarm6) {
            Id = id;
            Name = name;
            Version = version;
            DateTime = dateTime;
            Swarm1 = swarm1?.Length == 0 ? null : swarm1;
            Swarm2 = swarm2?.Length == 0 ? null : swarm2;
            Swarm3 = swarm3?.Length == 0 ? null : swarm3;
            Swarm4 = swarm4?.Length == 0 ? null : swarm4;
            Swarm5 = swarm5?.Length == 0 ? null : swarm5;
            Swarm6 = swarm6?.Length == 0 ? null : swarm6;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }

        [JsonPropertyName("date_time")]
        public DateTime DateTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Swarm1 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Swarm2 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Swarm3 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Swarm4 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Swarm5 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Swarm6 { get; set; }

    }
}
