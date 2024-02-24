using System;
using CRUD.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.WebAPI.Data
{
	public class ContactsDbContext : DbContext
	{

		public ContactsDbContext(DbContextOptions options) :base(options)
		{


		}


		public DbSet<Contact> Contacts { get; set; }
	}
}

