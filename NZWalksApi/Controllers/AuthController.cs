using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksApi.Models.DTO;
using NZWalksApi.Repositories;

namespace NZWalksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepository tokenRepository)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            
        }
        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Register([FromBody] RegistrerRequestDTO requestDTO)
        {
            var identity = new IdentityUser
            {
                UserName = requestDTO.UserName,
                Email=requestDTO.UserName
            };
            var identityResult =await _userManager.CreateAsync(identity, requestDTO.Password);

            if (identityResult.Succeeded) {
                //Agregar roles para el usuario.
                if (requestDTO.Roles != null && requestDTO.Roles.Any()) {
                    identityResult = await _userManager.AddToRolesAsync(identity, requestDTO.Roles);
                    if (identityResult.Succeeded) {
                        return Ok("Usuario registrado!!");
                    }
                }
            }
            return BadRequest("Algun error ocurrio");

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDTO loginRequestDTO)
        {
           var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null)
            {
               var userRequest = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

                if (userRequest)
                {
                    //Get Roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null) {
                        //Create Token
                        var JWToken = _tokenRepository.CreateJWToken(user, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            Jwtoken = JWToken,
                        };
                        return Ok(response);
                    }

                   
                    
                }
            }
            return BadRequest("Email o Password incorrecto");
        }
    }
}
