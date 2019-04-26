using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraumaRegistry.Data.Models
{
    public class FlagType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
      
        public string Name { get; set; }

        public int FlagId { get; set; }
        public Flag Flag { get; set; }
    }
}
