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
        public DbSet<TableAssessment>? Assessment { get; set; }
        public DbSet<TableBenefit>? Benefit { get; set; }
        public DbSet<TableCandidate>? Candidate { get; set; }
        public DbSet<TableData>? Data { get; set; }
        public DbSet<TableExam>? Exam { get; set; }
        public DbSet<TableFAQ>? FAQ { get; set; }
        public DbSet<TableInterview>? Interview { get; set; }
        public DbSet<TableIntroduction>? Introduction { get; set; }
        public DbSet<TableJobApplication>? JobApplication { get; set; }
        public DbSet<TableKeyword>? KeyWord { get; set; }
        public DbSet<TableLog>? Log { get; set; }
        public DbSet<TableQualification>? Qualification { get; set; }
        public DbSet<TableResponsibility>? Responsibility { get; set; }
        public DbSet<TableResume>? Resume { get; set; }
        public DbSet<TableStatus>? Status { get; set; }
        public DbSet<TableUser>? User { get; set; }

    }

   
}
