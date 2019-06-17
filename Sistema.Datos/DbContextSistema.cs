using Microsoft.EntityFrameworkCore;
using Sistema.Datos.mapping.almacen;
using Sistema.Entidades.almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Categoria> Categorias { get;  set; }
        public DbContextSistema( DbContextOptions<DbContextSistema> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }
    }
}
