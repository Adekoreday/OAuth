using System;
using OAuth.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

 namespace OAuth.Data
{
public class OAuthDbContext: DbContext {

        public DbSet<User> Users { get; set; }

    public OAuthDbContext(DbContextOptions options) :base(options) {

         }
    }

}