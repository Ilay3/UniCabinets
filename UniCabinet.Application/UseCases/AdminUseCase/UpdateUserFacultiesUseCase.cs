using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class UpdateUserFacultiesUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UpdateUserFacultiesUseCase> _logger;

        public UpdateUserFacultiesUseCase(IUserService userService, ILogger<UpdateUserFacultiesUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> Execute(string userId, List<int> selectedFacultyIds, int? primaryFacultyId)
        {
            try
            {
                _logger.LogInformation("Обновление факультетов пользователя {UserId}.", userId);

                await _userService.UpdateUserFacultiesAsync(userId, selectedFacultyIds, primaryFacultyId);

                _logger.LogInformation("Факультеты пользователя {UserId} успешно обновлены.", userId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении факультетов пользователя {UserId}.", userId);
                return false;
            }
        }
    }
}