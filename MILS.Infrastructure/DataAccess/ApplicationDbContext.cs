using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MILS.Domain;

namespace MILS.Infrastructure.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tracking> Trackings { get; set; }
    public DbSet<Incident> Incidents { get; set; }
    public DbSet<IncidentLine> IncidentLines { get; set; }

    public ApplicationDbContext() { }
    public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        

        modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                Id = 1, ShortLabel = "Management", LongLabel = "CHECK LIST Evaluation des impacts CC",
                ImprovementProcess = "Q-4-06-XX-00"
            },
            new Category()
            {
                Id = 2, ShortLabel = "Conception", LongLabel = "CHECK LIST Evaluation des impacts CC CONCEPTION",
                ImprovementProcess = "Q-4-06-XX-00"
            }
        );
        
        modelBuilder.Entity<Line>().HasData(
            new Line()
            {
                Id = 1, Label = "Modification administrative (nom, entité, adresse)", CategoryId = 1
            },
            new Line()
            {
                Id = 2, Label = "Modification des certificats (ajout/retrait d'une activité, modification, libellé)", CategoryId = 1
            },
            new Line()
            {
                Id = 3, Label = "Déménagement", CategoryId = 1
            },
            new Line()
            {
                Id = 4, Label = "Ajout de nouveaux sites", CategoryId = 1
            },
            new Line()
            {
                Id = 5, Label = "Transfert/internalisation d'activités", CategoryId = 1
            },
            new Line()
            {
                Id = 6, Label = "Internalisation d'une activité", CategoryId = 1
            },
            new Line()
            {
                Id = 7, Label = "Changement de responsabilités au niveau des rôles règlementaires ou de la direction générale", CategoryId = 1
            },
            new Line()
            {
                Id = 8, Label = "Modification des infrastructures", CategoryId = 1
            },
            new Line()
            {
                Id = 9, Label = "Formation", CategoryId = 1
            },
            new Line()
            {
                Id = 10, Label = "Destination / Utilisation du produit", CategoryId = 2
            },
            new Line()
            {
                Id = 11, Label = "Caractéristiques techniques du dispositif", CategoryId = 2
            },
            new Line()
            {
                Id = 12, Label = "Performance du produit", CategoryId = 2
            },
            new Line()
            {
                Id = 13, Label = "Plan de contrôle", CategoryId = 2
            },
            new Line()
            {
                Id = 14, Label = "Plaque de marque", CategoryId = 2
            },
            new Line()
            {
                Id = 15, Label = "Etiquetage", CategoryId = 2
            },
            new Line()
            {
                Id = 16, Label = "Gestion des alarmes", CategoryId = 2
            },

            new Line()
            {
                Id = 17, Label = "Modification de composant", CategoryId = 2
            }
        );
    }

}