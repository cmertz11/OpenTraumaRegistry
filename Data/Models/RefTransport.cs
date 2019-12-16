using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpenTraumaRegistry.Data.Models
{
    public class RefTransport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
