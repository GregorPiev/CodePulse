﻿using CodePulseAPI.Models.Domain;

namespace CodePulseAPI.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateAsync(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
        Task<BlogPost?> GetByURLHandleAsync(string urlHandle);
    }
}
