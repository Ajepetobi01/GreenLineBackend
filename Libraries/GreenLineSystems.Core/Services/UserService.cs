using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Context;
using GreenLineSystems.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace GreenLineSystems.Core.Services;

public class UserService:IUserService
{
    private readonly ILogger<UserService> _logger;
    private GreenLineContext _db;
    private UserManager<ApplicationUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    
    public UserService(
        ILogger<UserService> logger,
        GreenLineContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _logger = logger;
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    
      public async Task<MessageResult<bool>> AddUser(UserCreationRequest model)
    {
        MessageResult<bool> response = new MessageResult<bool>();
        try
        {
                var userExists = await _userManager.FindByEmailAsync(model.email);
                                
                if (userExists != null)
                {
                    response.Code = 404;
                    response.Message = "Email Exists";

                    return response;

                }
                
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = model.firstname,
                    LastName = model.lastname,
                    UserName = model.email,
                    Email = model.email,
                    PhoneNumber = model.phonenumber,
                    SecurityStamp = Guid.NewGuid().ToString()
                    
                };


                var result = await _userManager.CreateAsync(user,model.password);
                
                if (!result.Succeeded)
                {
                    var identityMessage = result.Errors.FirstOrDefault().Description;
                    
                    response.Code = 500;
                    response.Message = "error Creating User: "+identityMessage;
                    
                    _logger.LogError("error Creating User :" + identityMessage);

                    return response;
                }
                else
                {

                    foreach (var role in model.roles)
                    {
                        
                        IdentityRole identityRole = new IdentityRole
                        {
                            Name = role
                        };
                        
                        await _roleManager.CreateAsync(identityRole);
                        await _userManager.AddToRoleAsync(user, role);
                    } 
                    
                    response.Code = 200;
                    response.Data = true;
                  response.Message = "user added successfully";

                }
                
            
        }
        catch (Exception e)
        {
           _logger.LogError("exception Adding Users:"+ e.StackTrace);

           response.Code = 500;
           response.Data = false;
           response.Message = "error adding user: " + e.Message;
        }

        return response;

    }

      public async Task<MessageResult<LoginResponse>> LoginUser(LoginModel model)
    {
        MessageResult<LoginResponse> response = new MessageResult<LoginResponse>();
        
        try
        {

            var user = await _userManager.FindByEmailAsync(model.email);
               var result = await _signInManager.PasswordSignInAsync(model.email, model.password, false, true);

          
                //check the user role
                //pass data into claims

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim("Id",user.Id)
                };

                //Add new claim
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                //create signing key
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddYears(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                LoginResponse loginResponse = new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                     Email = user.Email
                };

                response.Data = loginResponse;
                response.Code = 200;
                response.Message = "User logged in successfully";


        }
        catch (Exception e)
        {
            _logger.LogError("Exception logging in:"+ e.StackTrace);

            response.Code = 500;
            response.Message = "Error logging in: " + e.Message;
        }

        return response;
    }

      public async Task<MessageResult<bool>> ResetCredentials(UserCredentialsUpdate model)
      {
          MessageResult<bool> response = new MessageResult<bool>();
          try
          {
              using (_db)
              {
                  var user = await _db.Users.Where(x => x.Id == model.id).FirstOrDefaultAsync();

                  if (user == null)
                  {

                      response.Code = 404;
                      response.Message = "Error finding user: ";

                      return response;
                  }

                  await _userManager.RemovePasswordAsync(user);

                  await _userManager.AddPasswordAsync(user, model.password);

                  response.Code = 200;
                  response.Message = "Password Changed Successfully";
                  response.Data = true;

                  return response;

              }
          }
          catch (Exception e)
          {
              _logger.LogError("Exception Adding Users: "+ e.StackTrace);

              response.Code = 500;
              response.Message = "Error adding user: " + e.Message;
          }

          return response;
      }

}