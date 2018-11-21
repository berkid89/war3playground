using Piranha.Data;
using Piranha.Extend.Fields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace war3playground.BusinessLogic.Models
{
    [Table("Players")]
    public class Player : BusinessEntity
    {
        [Required]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Account { get; set; }

        [MaxLength(500)]
        public string Aliases { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey("Pic")]
        public Guid? PicId { get; set; }

        public virtual Media Pic { get; set; }
    }
}
