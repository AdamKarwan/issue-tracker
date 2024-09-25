using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO;

namespace Api.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetTasksAsync();
        Task<TaskDto> GetTaskAsync(int id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto createTaskDto);
        Task<TaskDto?> UpdateTaskAsync(int id, CreateTaskDto createTaskDto);
        Task<bool> DeleteTaskAsync(int id);
    }
}
