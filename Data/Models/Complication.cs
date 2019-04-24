using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraumaData.Models
{
    public class Complication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int EventId { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Note { get; set; }

        public int Unit { get; set; }

    }
}
