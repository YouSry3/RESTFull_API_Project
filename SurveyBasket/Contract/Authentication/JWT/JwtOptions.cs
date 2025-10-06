﻿using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Contract.Authentication.JWT
{
    public class JwtOptions
    {
        public static string SectionName = "JwtOptions";
        [Required]
        public string Key { get; init; } = string.Empty;
        [Required]
        public string Issuer { get; init; } = string.Empty;
        [Required]
        public string Audience { get; init; } = string.Empty;
        [Required]
        [Range(1, int.MaxValue)]
        public int ExpiryMinutes { get; init; }


    }
}
