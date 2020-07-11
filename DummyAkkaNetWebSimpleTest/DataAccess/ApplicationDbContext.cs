using DataModel.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        private static readonly bool[] s_migrated = { false };
        public DbSet<CoilSchedule> CoilSchedules { get; set; }
        public DbSet<CoilPDI> CoilPDI { get; set; }


        public ApplicationDbContext()
        {

        }


        public ApplicationDbContext(DbContextOptions options)
          : base(options)
        {
            if (options == null)
            {
                options = DbOptionsFactory.DbContextOptions;
            }

            if (!s_migrated[0])
            {
                lock (s_migrated)
                {
                    if (!s_migrated[0])
                    {
                        this.Database.Migrate();
                        s_migrated[0] = true;
                    }
                }
            }
        }

        #region 捨棄
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<CoilPDI>().HasKey(c => new { c.Id, c.CoilScheduleID });
        //    modelBuilder.Entity<CoilSchedule>().HasKey(c => new { c.Id });

        //    // 創建，記得.net core版本需要將Id設值初始化
        //    var coilSchedules = CreateCoilSchedule();
        //    var coilPDI = CreateCoilPDI();
        //    modelBuilder.Entity<CoilSchedule>().HasData(coilSchedules);
        //    modelBuilder.Entity<CoilPDI>().HasData(coilPDI);
        //}

        //private IEnumerable<CoilSchedule> CreateCoilSchedule()
        //{
        //    return new List<CoilSchedule>
        //    {
        //        new CoilSchedule{
        //           Id ="HE00010000",
        //           SeqNo = 0,
        //           UpdateSource = "0",
        //           CreateTime = DateTime.Now,                  
        //        },
        //       new CoilSchedule {
        //            Id ="HE00020000",
        //           SeqNo = 1,
        //           UpdateSource = "0",
        //           CreateTime = DateTime.Now,
        //        },
        //        new CoilSchedule {
        //           Id ="HE00030000",
        //           SeqNo = 2,
        //           UpdateSource = "0",
        //           CreateTime = DateTime.Now,
        //        },
        //        new CoilSchedule {
        //           Id ="HE00040000",
        //           SeqNo = 3,
        //           UpdateSource = "0",
        //           CreateTime = DateTime.Now,
        //        }

        //    };
        //}
        //private IEnumerable<CoilPDI> CreateCoilPDI()
        //{
        //    return new List<CoilPDI>
        //    {
        //        new CoilPDI{
        //           Id = 0,
        //           CoilScheduleID ="HE00010000",
        //           EntryCoilWidth = 12540,
        //           EntryCoilLength = 1200,
        //           EntryCoilWeight = 16000,
        //           CreateTime = DateTime.Now
        //        },
        //       new CoilPDI {
        //           Id = 1,
        //           CoilScheduleID ="HE00020000",
        //           EntryCoilWidth = 12543,
        //           EntryCoilLength = 1210,
        //           EntryCoilWeight = 16200,
        //           CreateTime = DateTime.Now
        //        },
        //        new CoilPDI {
        //            Id = 2,
        //           CoilScheduleID ="HE00030000",
        //           EntryCoilWidth = 13543,
        //           EntryCoilLength = 1310,
        //           EntryCoilWeight = 16200,
        //           CreateTime = DateTime.Now
        //        },
        //        new CoilPDI {
        //           Id = 3,
        //           CoilScheduleID ="HE00040000",
        //           EntryCoilWidth = 11543,
        //           EntryCoilLength = 1210,
        //           EntryCoilWeight = 15200,
        //           CreateTime = DateTime.Now
        //        }

        //    };
        //}
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = DbOptionsFactory.ConnectionString;
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
