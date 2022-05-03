namespace GreenLineSystems.Core.ViewModels;

public class UserViewModel
{
    
}

public class UserCreationRequest
{
    public string firstname { get; set; }

    public string lastname { get; set; }
    public string email { get; set; }
    public string phonenumber { get; set; }
    public string password { get; set; }
     public List<string> roles { get; set; }
    
}


public class LoginModel
{
    public string email { get; set; }
    public string password { get; set; }
}


public class UserCredentialsUpdate
{
    public string id { get; set; }
    public string password { get; set; }
}

public class LoginResponse
{
    public string Email { get; set; }

    public List<string> Roles { get; set; }
    
    public string Token { get; set; }
}