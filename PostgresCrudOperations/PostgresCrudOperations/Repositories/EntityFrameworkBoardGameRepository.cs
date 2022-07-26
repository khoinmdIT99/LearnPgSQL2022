﻿using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PostgresCrudOperations.Repositories
{
    public class BoardGamesContext : DbContext
    {
        private const string CONNECTION_STRING = "Server=localhost;Port=5432;" +
                   "Username=postgres;" +
                   "Password=123456;" +
                   "Database=postgresDB";

        public DbSet<BoardGame> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(CONNECTION_STRING);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>(e => e.ToTable("games"));
            base.OnModelCreating(modelBuilder);
        }
    }

    public class EntityFrameworkBoardGameRepository : IBoardGameRepository
    {
        public async Task Add(BoardGame game)
        {
            using var db = new BoardGamesContext();
            await db.Games.AddAsync(game);
            await db.SaveChangesAsync();
        }

        public Task CreateTableIfNotExists() => Task.CompletedTask;

        public async Task Delete(int id)
        {
            using var db = new BoardGamesContext();
            var game = await db.Games.FindAsync(id);
            if (game == null)
                return;

            db.Games.Remove(game);
            await db.SaveChangesAsync();
        }

        public async Task<BoardGame> Get(int id)
        {
            using var db = new BoardGamesContext();
            return await db.Games.FindAsync(id);
        }

        public async Task<IEnumerable<BoardGame>> GetAll()
        {
            using var db = new BoardGamesContext();
            return await db.Games.ToListAsync();
        }

        public Task<string> GetVersion() => Task.FromResult("");

        public async Task Update(int id, BoardGame game)
        {
            using var db = new BoardGamesContext();
            db.Games.Update(game);
            await db.SaveChangesAsync();
        }
    }
}