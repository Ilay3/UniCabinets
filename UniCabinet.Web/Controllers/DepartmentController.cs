using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.UseCases.DepartmentUseCase;
using UniCabinet.Application.UseCases.DisciplineUseCase;
using UniCabinet.Application.UseCases.FacultyUseCase;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.DTOs.FacultyManagement;
using UniCabinet.Core.Models.ViewModel.Department;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Core.Models.ViewModel.Discipline;
using UniCabinet.Core.UseCases;

namespace UniCabinet.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFacultyRepository _facultyRepository;

        public DepartmentController(IMapper mapper, IDepartmentRepository departmentRepository, IFacultyRepository facultyRepository)
        {
            _mapper = mapper;
            _departmentRepository = departmentRepository;
            _facultyRepository = facultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentListAsync([FromServices] GetDepartmentDisciplinesUseCase getDisciplinesByHeadUseCase)
        {
            var viewModel = await GetDepartmentDisciplinesAsync(getDisciplinesByHeadUseCase);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard([FromServices] GetDepartmentDisciplinesUseCase getDisciplinesByHeadUseCase)
        {
            var viewModel = await GetDepartmentDisciplinesAsync(getDisciplinesByHeadUseCase);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentsAdminList(
        [FromServices] GetDepartmentsWithUsersUseCase getDepartmentsWithUsersUseCase)
        {
            var departmentsDTO = await getDepartmentsWithUsersUseCase.ExecuteAsync();
            var departmentsVM = _mapper.Map<List<DepartmentsWithUsersVM>>(departmentsDTO);
            return View(departmentsVM);
        }
        private async Task<DepartmentWithDisciplinesVM> GetDepartmentDisciplinesAsync(GetDepartmentDisciplinesUseCase getDisciplinesByHeadUseCase)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var result = await getDisciplinesByHeadUseCase.ExecuteAsync(userId);
            return _mapper.Map<DepartmentWithDisciplinesVM>(result);
        }

        [HttpGet]
        public IActionResult AddDepartmentModal()
        {
            var model = new DepartmentAddEditVM();
            return PartialView("_AddDepartmentModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentAddEditVM model,
            [FromServices] AddDepartmentUseCase addDepartmentUseCase)
        {
            if (ModelState.IsValid)
            {
                var departmentDTO = _mapper.Map<DepartmentDTO>(model);
                await addDepartmentUseCase.ExecuteAsync(departmentDTO);

                return RedirectToAction("DepartmentsAdminList");
            }
            else
            {
                return PartialView("_AddDepartmentModal", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartmentModal(int departmentId)
        {
            var getDepartmentUseCase = new GetDepartmentByIdUseCase(_departmentRepository, _mapper);
            var departmentDTO = await getDepartmentUseCase.ExecuteAsync(departmentId);

            if (departmentDTO == null)
            {
                return NotFound();
            }

            var model = new DepartmentAddEditVM
            {
                Id = departmentDTO.Id,
                DepartmentName = departmentDTO.DepartmentName,
                FacultyId = departmentDTO.FacultyId,
                FacultyName = departmentDTO.FacultyName
            };

            ViewBag.Faculties = await GetFacultiesForDropdown();

            return PartialView("_EditDepartmentModal", model);
        }


        [HttpPost]
        public async Task<IActionResult> EditDepartment(DepartmentAddEditVM model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var departmentDTO = new DepartmentDTO
                {
                    Id = model.Id.Value,
                    DepartmentName = model.DepartmentName,
                    FacultyId = model.FacultyId
                };

                var editDepartmentUseCase = new EditDepartmentUseCase(_departmentRepository, _mapper);
                await editDepartmentUseCase.ExecuteAsync(departmentDTO);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("DepartmentsAdminList");
            }

            ViewBag.Faculties = await GetFacultiesForDropdown();

            return View("_EditDepartmentModal", model);
        }

        private async Task<List<object>> GetFacultiesForDropdown()
        {
            var getAllFacultiesUseCase = new GetAllFacultiesUseCase(_facultyRepository);
            var faculties = await getAllFacultiesUseCase.ExecuteAsync();

            return faculties.Select(f => new { Id = f.Id, Name = f.Name }).ToList<object>();
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentDetails(int id,
        [FromServices] GetDepartmentByIdUseCase getDepartmentByIdUseCase)
        {
            var department = await getDepartmentByIdUseCase.ExecuteAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<DepartmantVM>(department);
            return View(viewModel);
        }
    }
}
