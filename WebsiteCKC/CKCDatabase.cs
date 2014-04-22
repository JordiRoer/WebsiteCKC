namespace WebsiteCKC
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class CKCDatabase : DbContext
    {
        // Your context has been configured to use a 'CKCDatabase' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebsiteCKC.CKCDatabase' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CKCDatabase' 
        // connection string in the application configuration file.
        public CKCDatabase()
            : base("name=CKCDatabase")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Competition
    {
        [Key]
        public int ID { get; set; }
        public int Class { get; set; }
        public int Group { get; set; }
    }

    public class Team
    {
        [Key]
        public int ID { get; set; }
        public string ClubName { get; set; }
        public int TeamNumber { get; set; }
        public int CompID { get; set; }
    }
}