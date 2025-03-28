﻿using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Domain.Models;
using AutoMapper;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class EditDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public EditDepartmentUseCase(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(DepartmentDTO departmentDTO)
    {
        if (departmentDTO == null)
            throw new ArgumentNullException(nameof(departmentDTO));

        var departmentEntity = await _departmentRepository.GetDepartmentByIdAsync(departmentDTO.Id);
        if (departmentEntity != null)
        {
            departmentEntity.DepartmentName = departmentDTO.DepartmentName;
            departmentEntity.FacultyId = departmentDTO.FacultyId;

            await _departmentRepository.UpdateDepartmentAsync(departmentEntity);
        }
    }


}
