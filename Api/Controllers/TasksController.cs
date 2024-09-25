using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Context;
using Api.DTO;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public TasksController(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);
            return taskDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            var taskDto = _mapper.Map<TaskDto>(task);
            return taskDto;
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskDto createTaskDto)
        {
            var taskModel = _mapper.Map<TaskModel>(createTaskDto);
            await _context.Tasks.AddAsync(taskModel);
            await _context.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskModel);
            return CreatedAtAction(nameof(GetTask), new { id = taskDto.Id }, taskDto);
        }
    }
}
