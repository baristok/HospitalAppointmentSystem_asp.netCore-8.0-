using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models;

public class AdminLoginModel
{

    public string Username { get; set; }
    
    public string Password { get; set; }
}