using System.ComponentModel.DataAnnotations;

namespace Schemes.Domain.Requests
{
    public class UpdateSchemeRequest
    {
        public UpdateSchemeRequest(
            string name, int version, 
            string swarm1, string swarm2, 
            string swarm3, string swarm4, 
            string swarm5, string swarm6) {
            Name = name;
            Version = version;
            Swarm1 = swarm1;
            Swarm2 = swarm2;
            Swarm3 = swarm3;
            Swarm4 = swarm4;
            Swarm5 = swarm5;
            Swarm6 = swarm6;
        }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public int Version { get; set; }
        public string Swarm1 { get; set; }
        public string Swarm2 { get; set; }
        public string Swarm3 { get; set; }
        public string Swarm4 { get; set; }
        public string Swarm5 { get; set; }
        public string Swarm6 { get; set; }

    }
}
