﻿using CleanArchitecture.Domain;
namespace CleanArchitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository  : IAsyncRepository<Video>
    {
        Task<Video> GetVideoByName(string nameVideo);
        Task<IEnumerable<Video>> GetVideoByUserName(string userName);
    }
}
