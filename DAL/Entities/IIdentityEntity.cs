using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public interface IIdentityEntity<T> : IEntity<T>
    {        
        [EmailAddress]
        [Required, StringLength(100)]
        string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        string Password { get; set; }

    }
}