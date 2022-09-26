using CRUDWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDWebApi.Data
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var usr = modelBuilder.Entity<Usuario>();
            usr.ToTable("tb_usuario");
            usr.HasKey(x => x.Id);
            usr.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            usr.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            usr.Property(x => x.DataNascimento).HasColumnName("data_nascimento");


        }
    }
}
