using Thesis.Data;
using Thesis.Models;
using Microsoft.EntityFrameworkCore;

namespace Thesis.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterAsync(User user)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Name == user.Name))
            {
                return false;
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<User?> LoginAsync(string name, string password)
    {
        try
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Name == name && u.Password == password);
            return user;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> UserExistsAsync(string name)
    {
        try
        {
            return await _context.Users.AnyAsync(u => u.Name == name);
        }
        catch (Exception)
        {
            return false;
        }
    }
}