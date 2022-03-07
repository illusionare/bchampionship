using System.ComponentModel.DataAnnotations;

namespace BeardChamp.Data
{
    /// <summary>
    /// Base class that contains all information about event
    /// </summary>
    public class Competition
    { 
        [Required]
        public uint Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset AnnonceDate { get; set; }

        [Required]
        public DateTimeOffset EventStartDate { get; set; }

        [Required, StringLength(100)]
        public string Address { get; set; } = string.Empty;

        public string LogoFileName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string OfficialPageURI { get; set; } = string.Empty;
        public DateTimeOffset Updated { get; internal set; }
    }
}
