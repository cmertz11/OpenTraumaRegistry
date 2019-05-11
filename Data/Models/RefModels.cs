using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TraumaData.Models
{
    public class RefModels
    {
        public class RefGender
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
        public class RefRace
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
        public class RefInjuryType
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
        public class RefSafetyDevices
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
        public class RefRiskData
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
        public class RefLocation
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
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
        public class RefArrivedFrom
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

            public int Id { get; set; }

            [MaxLength(20)]
            public string Code { get; set; }
            [MaxLength(200)]
            public string Description { get; set; }

        }
        public class RefTraumaLevel
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
}
