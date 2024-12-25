using Microsoft.AspNetCore.Mvc;
using lab1_api.Models.DTOs;
using lab1_api.Facade;
using lab1_api.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;
namespace lab1_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserFacade _userFacade;
        public UserController(UserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng
        /// </summary>
        /// <returns>Danh sách người dùng đã đăng ký trong hệ thống</returns>
        [HttpGet("get-all-users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Lấy danh sách tất cả người dùng",
            Description = "API này trả về danh sách tất cả người dùng đã đăng ký trong hệ thống",
            OperationId = "GetAllUsers"
        )]
        public async Task<ActionResult<GenericApiResponse<List<UserDto>>>> GetAllUsers()
        {
            try
            {
                var users = (await _userFacade.GetAllUsersAsync()).ToList();
                var response = GenericApiResponse<List<UserDto>>.Success(users);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = GenericApiResponse<List<UserDto>>.Error($"An error occurred: {ex.Message}");
                return BadRequest(errorResponse);
            }
        }
    }
}
