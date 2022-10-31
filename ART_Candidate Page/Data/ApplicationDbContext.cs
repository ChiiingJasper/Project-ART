using Microsoft.EntityFrameworkCore;
using ART_Candidate_Page.Models;
using System.ComponentModel.DataAnnotations;

namespace ART_Candidate_Page.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        
        public DbSet<TableCandidate> Candidates { get; set; }

    }

   
}
