using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TraumaData.Models
{
    public class Reference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(200)]
       
        public string Name { get; set; }

        public List<ReferenceDetail> ReferenceDetails { get; set; }
    }
}
