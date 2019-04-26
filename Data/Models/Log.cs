using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TraumaRegistry.Data.Models
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
 
        [MaxLength(200)]
        public int LogTypeId { get; set; }  //FK to ReferenceDetail Table Where Reference.Name = "LogType"

        [MaxLength(2000)]
        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
