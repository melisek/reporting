﻿namespace szakdoga.Models
{
    public interface IDashboardRepository : IBaseRepositoryInterface<Dashboard>
    {
        Dashboard Get(string GUID);
    }
}