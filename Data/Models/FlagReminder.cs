using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraumaRegistry.Data.Models
{
    public class FlagReminder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string NotifyEmailList { get; set; }
        public string CronTab { get; set; }

        public int FlagId { get; set; }
        public Flag Flag { get; set; }

    }
}
