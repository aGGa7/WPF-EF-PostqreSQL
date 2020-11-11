using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp3
{ 
    class ApplicationContext : DbContext //DbContext Является базовым классом Entity Framework
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated(); //создает БД при ее отсутствии
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //В этот метод передается объект DbContextOptionsBuilder, который позволяет создать параметры подключения. Для их создания вызывается метод UseSqlServer
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test_db;Username=postgres;Password=1234"); //Для установки подключения к базе данных вызывается метод UseNpgsql(), в который передается строка подключения.
        }
    }
}
