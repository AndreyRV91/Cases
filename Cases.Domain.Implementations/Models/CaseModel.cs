using System;
using System.ComponentModel.DataAnnotations;

namespace Cases.Domain.Implementations.Models
{
    public class CaseModel
    {   
        [Required]
        public int Id { get; set; }
    }
}
