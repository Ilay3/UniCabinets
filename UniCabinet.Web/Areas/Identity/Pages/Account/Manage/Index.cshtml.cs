﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UniCabinet.Application.UseCases;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public bool IsProfileComplete { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsVerified { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public required InputModel Input { get; set; }


        public class InputModel
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            public string Patronymic { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime? DateBirthday { get; set; }
        }

        private async Task LoadAsync(UserEntity user)
        {
            Input = new InputModel
            {
                FirstName = user?.FirstName ?? string.Empty,
                LastName = user?.LastName ?? string.Empty,
                Patronymic = user?.Patronymic ?? string.Empty,
                DateBirthday = user?.DateBirthday ?? DateTime.MinValue
            };

            await Task.CompletedTask;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);

            IsProfileComplete = !string.IsNullOrEmpty(user.FirstName) &&
                                !string.IsNullOrEmpty(user.LastName) &&
                                !string.IsNullOrEmpty(user.Patronymic) &&
                                user.DateBirthday.HasValue;

            IsEmailConfirmed = user.EmailConfirmed;
            IsVerified = await _userManager.IsInRoleAsync(user, "Верефицирован");

            return Page();
        }


        public async Task<IActionResult> OnPostAsync([FromServices] UserVerificationUseCase userVerificationUseCase)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Patronymic = Input.Patronymic;
            user.DateBirthday = Input.DateBirthday;

            var updateResult = await _userManager.UpdateAsync(user);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("Профиль пользователя обновлен.");

                // Вызываем VerifyUserAsync после обновления профиля
                await userVerificationUseCase.ExecuteAsync(user.Id);

                await _signInManager.RefreshSignInAsync(user);
                TempData["StatusMessage"] = "Ваш профиль обновлен";
                return RedirectToPage();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await LoadAsync(user);

            return Page();
        }
    }
}
