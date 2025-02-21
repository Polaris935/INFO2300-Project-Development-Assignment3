using Healthcare_And_Wellness.Models;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_And_Wellness.Data
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> jobs { get; set; }
        public DbSet<Applicant> applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "admin",
                Password = "Admin@1234",
                ConfirmPassword = "Admin@1234",
                Name = "Administrator",
                Age = 30,
                DateOfBirth = "1994-01-01",
                Role = "Admin"
            },
            new User
            {
                Id = 2,
                Username = "member",
                Password = "Lovepreet@1234",
                ConfirmPassword = "Lovepreet@1234",
                Name = "Lovepreet Singh",
                Age = 21,
                DateOfBirth = "2003-10-26",
                Role = "Member"
            });

            modelBuilder.Entity<Applicant>().HasOne(c => c.Job).WithMany(m => m.applicants).HasForeignKey(c => c.jobID);
            modelBuilder.Entity<Job>().HasData(
               new Job()
               {
                   jobID = 1,
                   jobName = "Instructor Therapist",
                   statusJob = "Apply",
                   description = "The responsibility of the Instructor " +
               "Therapist is to deliver direct ABA (Applied Behaviour Analysis) interventions. This includes providing input into the development " +
               "and implementation of Behaviour Support Plans (BSPs), collecting baseline data, maintaining progress notes (i.e., case notes), " +
               "the recording and graphing of relevant data, parent coaching, individual and group services, as well as the preparation of teaching materials " +
               "and also working with, and mentoring volunteers."
               }
           );
        }
    }
}
