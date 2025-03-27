using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class GetUserFacultiesModalUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<GetUserFacultiesModalUseCase> _logger;

        public GetUserFacultiesModalUseCase(IUserService userService, ILogger<GetUserFacultiesModalUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<UserMultiFacultyDTO> Execute(string userId)
        {
            _logger.LogInformation("Получение данных о факультетах пользователя {UserId}.", userId);

            var userFacultiesDTO = await _userService.GetUserFacultiesAsync(userId);
            if (userFacultiesDTO == null)
            {
                _logger.LogWarning("Пользователь с ID {UserId} не найден.", userId);
                return null;
            }

            _logger.LogInformation("Данные о факультетах пользователя {UserId} получены.", userId);

            return userFacultiesDTO;
        }
    }
}