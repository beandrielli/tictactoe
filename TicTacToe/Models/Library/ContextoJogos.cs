using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Models.Library
{
    public class ContextoJogos : DbContext
    {

        public ContextoJogos(DbContextOptions<ContextoJogos> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sessao>(sessaoEntity =>
            {
                sessaoEntity.HasKey(sessao => sessao.Identificador);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sessao> Sessoes { get; set; }

    }
}
