using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.UseCases.FacultyUseCase;
using UniCabinet.Core.DTOs.FacultyManagement;
using UniCabinet.Core.Models.ViewModel.Faculty;

namespace UniCabinet.Web.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class FacultyController : Controller
    {
        private readonly IMapper _mapper;

        public FacultyController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> FacultyList(
            [FromServices] GetAllFacultiesUseCase getAllFacultiesUseCase)
        {
            var facultiesDTO = await getAllFacultiesUseCase.ExecuteAsync();
            var viewModel = _mapper.Map<List<FacultyListVM>>(facultiesDTO);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> FacultyDetails(
            int id,
            [FromServices] GetFacultyWithDepartmentsUseCase getFacultyWithDepartmentsUseCase)
        {
            var facultyDTO = await getFacultyWithDepartmentsUseCase.ExecuteAsync(id);
            if (facultyDTO == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<FacultyDetailsVM>(facultyDTO);
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult FacultyAddModal()
        {
            return PartialView("_FacultyAddModal", new FacultyCreateVM());
        }

        [HttpPost]
        public async Task<IActionResult> AddFaculty(
            FacultyCreateVM viewModel,
            [FromServices] AddFacultyUseCase addFacultyUseCase)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_FacultyAddModal", viewModel);
            }

            var facultyDTO = _mapper.Map<FacultyCreateDTO>(viewModel);
            await addFacultyUseCase.ExecuteAsync(facultyDTO);
            return RedirectToAction("FacultyList");
        }

        [HttpGet]
        public async Task<IActionResult> FacultyEditModal(
            int id,
            [FromServices] GetFacultyWithDepartmentsUseCase getFacultyWithDepartmentsUseCase)
        {
            var facultyDTO = await getFacultyWithDepartmentsUseCase.ExecuteAsync(id);
            if (facultyDTO == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<FacultyEditVM>(facultyDTO);
            return PartialView("_FacultyEditModal", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditFaculty(
            FacultyEditVM viewModel,
            [FromServices] UpdateFacultyUseCase updateFacultyUseCase)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_FacultyEditModal", viewModel);
            }

            var facultyDTO = _mapper.Map<FacultyEditDTO>(viewModel);
            await updateFacultyUseCase.ExecuteAsync(facultyDTO);
            return RedirectToAction("FacultyList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFaculty(
            int id,
            [FromServices] DeleteFacultyUseCase deleteFacultyUseCase)
        {
            await deleteFacultyUseCase.ExecuteAsync(id);
            return RedirectToAction("FacultyList");
        }

        [HttpGet]
        public async Task<IActionResult> AssignDepartmentsModal(
    int id,
    [FromServices] GetFacultyWithDepartmentsUseCase getFacultyWithDepartmentsUseCase,
    [FromServices] IDepartmentRepository departmentRepository)
        {
            var facultyDTO = await getFacultyWithDepartmentsUseCase.ExecuteAsync(id);
            if (facultyDTO == null)
            {
                return NotFound();
            }

            // Получаем все кафедры
            var allDepartments = await departmentRepository.GetAllDepartment();

            var assignmentVM = new FacultyDepartmentAssignmentVM
            {
                FacultyId = facultyDTO.Id,
                FacultyName = facultyDTO.Name,
                AvailableDepartments = allDepartments.Select(d => new DepartmentAssignmentItemVM
                {
                    Id = d.Id,
                    Name = d.DepartmentName,
                    IsAssigned = facultyDTO.Departments.Any(fd => fd.Id == d.Id),
                    IsAlreadyAssignedToOtherFaculty = d.FacultyId.HasValue && d.FacultyId != facultyDTO.Id,
                    OtherFacultyName = d.FacultyId.HasValue && d.FacultyId != facultyDTO.Id ? d.FacultyName : null
                }).ToList()
            };

            return PartialView("_AssignDepartmentsModal", assignmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> AssignDepartments(
    int facultyId,
    List<int> selectedDepartmentIds,
    [FromServices] AssignDepartmentsToFacultyUseCase assignDepartmentsToFacultyUseCase)
        {
            if (selectedDepartmentIds == null)
            {
                selectedDepartmentIds = new List<int>();
            }

            await assignDepartmentsToFacultyUseCase.ExecuteAsync(facultyId, selectedDepartmentIds);

            return RedirectToAction("FacultyDetails", new { id = facultyId });
        }

        [HttpGet]
        public async Task<IActionResult> FacultyHierarchy(
            [FromServices] GetAllFacultiesUseCase getAllFacultiesUseCase,
            [FromServices] GetFacultyWithDepartmentsUseCase getFacultyWithDepartmentsUseCase)
        {
            var facultiesDTO = await getAllFacultiesUseCase.ExecuteAsync();
            var faculties = new List<FacultyWithDepartmentsVM>();

            foreach (var facultyDTO in facultiesDTO)
            {
                // передаем сюда getFacultyWithDepartmentsUseCase
                var faculty = await GetFacultyWithDepartments(facultyDTO.Id, getFacultyWithDepartmentsUseCase);
                if (faculty != null)
                {
                    faculties.Add(faculty);
                }
            }

            var hierarchyVM = new FacultyHierarchyVM
            {
                Faculties = faculties
            };

            return View(hierarchyVM);
        }   

        private async Task<FacultyWithDepartmentsVM> GetFacultyWithDepartments(
            int facultyId,
            [FromServices] GetFacultyWithDepartmentsUseCase getFacultyWithDepartmentsUseCase)
        {
            var facultyDTO = await getFacultyWithDepartmentsUseCase.ExecuteAsync(facultyId);
            if (facultyDTO == null)
            {
                return null;
            }

            return _mapper.Map<FacultyWithDepartmentsVM>(facultyDTO);
        }
    }
}