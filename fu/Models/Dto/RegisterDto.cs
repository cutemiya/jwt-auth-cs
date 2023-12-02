using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace fu.Models.Dto;

public class RegisterDto
{
    [JsonRequired] public string? Email { get; set; }
    [JsonRequired] public string? Password { get; set; }
    [JsonRequired] public string? Username { get; set; }
    [JsonRequired] public string? Role { get; set; }
}