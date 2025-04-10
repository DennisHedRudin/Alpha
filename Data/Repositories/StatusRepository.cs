﻿using Data.Context;
using Data.Entities;
using Data.Repositories.BaseRepository;
using Domain.Models;

namespace Data.Repositories;

public interface IStatusRepository : IBaseRepository<StatusEntity, Status>
{

}

public class StatusRepository(DataContext context) : BaseRepository<StatusEntity, Status>(context), IStatusRepository
{
}
