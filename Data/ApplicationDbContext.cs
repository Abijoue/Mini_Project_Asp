﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniProjet_Core.Model;



namespace MiniProjet_Core.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Filiere> Filieres { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Presence> Presences { get; set; }
        public DbSet<Absence> Absences { get; set; }


    }
}
