﻿using AutoMapper.Configuration.Conventions;
using System.ComponentModel.DataAnnotations;

namespace NZWalksApi.Models.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
