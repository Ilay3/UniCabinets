using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetUserDepartmentsModalUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserDepartmentsModalUseCase> _logger;

        public GetUserDepartmentsModalUseCase(IUserService userService, ILogger<GetUserDepartmentsModalUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<UserMultiDepartmentDTO> Execute(string userId)
        {
            _logger.LogInformation("Получение данных о кафедрах пользователя {UserId}.", userId);

            var userDepartmentsDTO = await _userService.GetUserDepartmentsAsync(userId);
            if (userDepartmentsDTO == null)
            {
                _logger.LogWarning("Пользователь с ID {UserId} не найден.", userId);
                return null;
            }

            _logger.LogInformation("Данные о кафедрах пользователя {UserId} получены.", userId);

            return userDepartmentsDTO;
        }
    }
}