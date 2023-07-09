﻿using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class LoginRequest
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get;set; }


    }
}
