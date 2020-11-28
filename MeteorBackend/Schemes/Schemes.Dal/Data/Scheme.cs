using System;

namespace Schemes.Dal.Data
{
    public class Scheme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime DateTime { get; set; }
        public string Swarm1 { get; set; }
        public string Swarm2 { get; set; }
        public string Swarm3 { get; set; }
        public string Swarm4 { get; set; }
        public string Swarm5 { get; set; }
        public string Swarm6 { get; set; }
    }
}
