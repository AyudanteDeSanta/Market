using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage ="You must enter the field {0}")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "The field {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(30, ErrorMessage ="The field {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Phone { get; set; }

        [StringLength(30, ErrorMessage ="The field {0} must contain between {2} and {1} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "The field {0} must")]
        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name ="Document")]
        public string Document { get; set; }

        public int DocumentTypeID { get; set; }

        private string _FullName;
        [NotMapped]
        public string FullName { get { _FullName = $"{FirstName} {LastName}"; return _FullName; } set { _FullName = value; } }

        public virtual DocumentType DocumentType { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}