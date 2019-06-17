using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.almacen;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.mapping.almacen
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria")
                .HasKey(c => c.IdCategoria);
            builder.Property(c=>c.Nombre)
                .HasMaxLength(50);
            builder.Property(c=>c.Descripcion)
                .HasMaxLength(256);
        }
    }
}
