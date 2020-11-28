using System.Text.Json;
using System.Text.Json.Serialization;

namespace Schemes.Dal.Data
{
    public class Scheme
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }

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

        public Scheme Normalize() {
            if (Swarm1 == "") Swarm1 = null;
            if (Swarm2 == "") Swarm2 = null;
            if (Swarm3 == "") Swarm3 = null;
            if (Swarm4 == "") Swarm4 = null;
            if (Swarm5 == "") Swarm5 = null;
            if (Swarm6 == "") Swarm6 = null;
            return this;
        }

    }
}
