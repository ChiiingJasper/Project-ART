using Microsoft.EntityFrameworkCore;
using Project_ART.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_ART.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<TableAssessment> Assessments { get; set; }
        public DbSet<TableCandidate> Candidates { get; set; }
        public DbSet<TableDatasheet> Datasheets { get; set; }
        public DbSet<TableEmployee> Employees { get; set; }
        public DbSet<TableExam> Exams { get; set; }
        public DbSet<TableInterview> Interviews { get; set; }
        public DbSet<TableIntroduction> Introductions { get; set; }
        public DbSet<TableJobApplication> JobApplication { get; set; }
        public DbSet<TableKeyword> KeyWords { get; set; }
        public DbSet<TableRecruiter> Recruiters { get; set; }
        public DbSet<TableSkill> Skills { get; set; }
    }

   
}
