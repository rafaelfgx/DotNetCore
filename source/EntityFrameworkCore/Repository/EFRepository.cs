using DotNetCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.EntityFrameworkCore;

public class EFRepository<T>(DbContext context) : Repository<T>(new EFCommandRepository<T>(context), new EFQueryRepository<T>(context)) where T : class;
