using CoreMVC.Models;
using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace CoreMVC.Services
{
    public class StudentsService
    {
        private ApplicationDbContext _dbContext;

        public StudentsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(st => st.Id == id);
        }

        public async Task CreateAsync(Student newStudent)
        {
            await _dbContext.Students.AddAsync(newStudent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Student updatedStudent)
        {
            _dbContext.Students.Update(updatedStudent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var studentToDelete = await _dbContext.Students.FirstOrDefaultAsync(st => st.Id == id);
            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}