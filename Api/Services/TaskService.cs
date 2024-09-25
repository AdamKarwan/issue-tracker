using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Api.DTO;
using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TaskDto>> GetTasksAsync()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            var taskModel = _mapper.Map<TaskModel>(createTaskDto);
            await _context.Tasks.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDto>(taskModel);
        }

        public async Task<TaskDto?> UpdateTaskAsync(int id, CreateTaskDto createTaskDto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return null;
            }
            _mapper.Map(createTaskDto, task);
            await _context.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
