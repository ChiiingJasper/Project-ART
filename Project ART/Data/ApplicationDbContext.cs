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
        public DbSet<TableResume> Resume { get; set; }
        public DbSet<TableStatus> Status { get; set; }


        public DbSet<TableUser> Users { get; set; }
        public DbSet<TableExam> Exams { get; set; }
        public DbSet<TableInterview> Interviews { get; set; }
        public DbSet<TableIntroduction> Introductions { get; set; }
        public DbSet<TableJobApplication> JobApplications { get; set; }
        public DbSet<TableKeyword> KeyWords { get; set; }

        public DbSet<TableSkill> Skills { get; set; }
    }

   
}
