﻿using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data;

public class CatalogDbContext : DbContext
{
	public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
	{
	}

}