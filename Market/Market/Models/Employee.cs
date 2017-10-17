using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Market.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Column("FirstName")]
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="You must enter {0}")]
        [StringLength(30, ErrorMessage ="The field {0} must be between {2} and {1} characters",MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You must enter {0}")]
        [StringLength(30, ErrorMessage ="The field {0} must be between {2} and {1} characters",MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must enter {0}")]
        [DisplayFormat(DataFormatString ="{0:C2}",ApplyFormatInEditMode =false)]
        public decimal Salary { get; set; }

        [Display(Name = "Bonus Percent")]
        [DisplayFormat(DataFormatString = "{0:P2}", ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DateOfBirth { get; set; }
        
        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "You must enter {0}")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime StartTime { get; set; }

        [Display(Name = "EMail")]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        [Display(Name = "Url")]
        [DataType(DataType.Url)]
        public string Url { get; set; }

        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "You must enter {0}")]
        public int DocumentTypeID { get; set; }

        [NotMapped]
        public int Age { get { return DateTime.Now.Year - DateOfBirth.Year; } }

        public virtual DocumentType DocumentType { get; set; }
    }
}