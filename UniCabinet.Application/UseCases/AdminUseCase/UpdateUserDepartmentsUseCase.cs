using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.AdminUseCase
{
    public class UpdateUserDepartmentsUseCase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UpdateUserDepartmentsUseCase> _logger;

        public UpdateUserDepartmentsUseCase(IUserService userService, ILogger<UpdateUserDepartmentsUseCase> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> Execute(string userId, List<int> selectedDepartmentIds, int? primaryDepartmentId)
        {
            try
            {
                _logger.LogInformation("Обновление кафедр пользователя {UserId}.", userId);

                await _userService.UpdateUserDepartmentsAsync(userId, selectedDepartmentIds, primaryDepartmentId);

                _logger.LogInformation("Кафедры пользователя {UserId} успешно обновлены.", userId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении кафедр пользователя {UserId}.", userId);
                return false;
            }
        }
    }
}