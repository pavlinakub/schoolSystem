using CoreMVC.Models;
using CoreMVC.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;

namespace CoreMVC.Services
{
    public class GradeService
    {
        ApplicationDbContext dbContext;
        public GradeService(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public async Task<IEnumerable<Grade>> GetGradesAsync()
        {
            return await dbContext.Grades.Include(st => st.Student).Include(sb => sb.Subject).ToListAsync();
        }
        public async Task<GradesDropDownViewModel> GetGradesDropDownValues()
        {
            var gradesDropDownsData = new GradesDropDownViewModel()
            {
                Students = await dbContext.Students.ToListAsync(),
                Subjects = await dbContext.Subjects.ToListAsync()
            };
            return gradesDropDownsData;
        }

        internal async Task CreateAsync(GradesViewModel newGrade)
        {
            var gradeToInsert = new Grade()
            { Student = await dbContext.Students.FirstOrDefaultAsync(st => st.Id == newGrade.StudentId),
                Subject = await dbContext.Subjects.FirstOrDefaultAsync(sb => sb.Id == newGrade.SubjectId),
                Mark = newGrade.Mark,
                Date = DateTime.Today.Date,
                What=newGrade.What

            };
            if (gradeToInsert.Subject != null && gradeToInsert.Student != null)
            {
                await dbContext.Grades.AddAsync(gradeToInsert);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<Grade> GetByIdAsync(int id) {
            return await dbContext.Grades.Include(st=>st.Student).Include(sb=>sb.Subject).FirstOrDefaultAsync(g=>g.Id== id);
        }

        internal async Task UpdateAsync(int id, GradesViewModel updatedGrade)
        {
            var dbGrade=await dbContext.Grades.FirstOrDefaultAsync(gr=>gr.Id== id);
            if(dbGrade != null) {
                dbGrade.Student =await dbContext.Students.FirstOrDefaultAsync(st => st.Id == updatedGrade.StudentId);
                dbGrade.Subject =await dbContext.Subjects.FirstOrDefaultAsync(sub => sub.Id == updatedGrade.SubjectId);
                dbGrade.What=updatedGrade.What;
                dbGrade.Mark=updatedGrade.Mark;
                dbGrade.Date = updatedGrade.Date.Date;
            }
            dbContext.Update(dbGrade);
           await dbContext.SaveChangesAsync();
        }
        internal async Task DeleteAsync(int id)
        {
            var gradeToDelete = await dbContext.Grades.FirstOrDefaultAsync(gr => gr.Id == id);
            dbContext.Grades.Remove(gradeToDelete);
            await dbContext.SaveChangesAsync();
        }
    }
}
