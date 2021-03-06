﻿namespace szakdoga.Models
{
    public interface IUserRepository : IBaseRepositoryInterface<User>
    {
        User Get(string GUID);

        User GetByEmailAdd(string emailAdd);
    }
}