using System.Security.Claims;
using System.Text;
using guacactings.Context;
using guacactings.Models;
using Microsoft.EntityFrameworkCore;

namespace guacactings.Services;

public class AdministratorService : IAdministratorService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion

    #region Constructor

    public AdministratorService(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion
    
    #region Methods
    
    // Get all administrators
    public async Task<IEnumerable<Administrator>> GetAdministrators(int page, int rows)
    {
        var administrators = await _context.Administrators.ToListAsync();
        var administratorsPaged =  administrators.Skip((page - 1) * rows).Take(rows);
        return administratorsPaged;
    }
    
    // Get an administrator by id
    public async Task<Administrator?> GetAdministratorById(int id)
    {
        var administrator = await _context.Administrators.FindAsync(id);
        return administrator ?? null;
    }
    
    // Create a new administrator
    public async Task<Administrator?> AddAdministrator(AdministratorRegistryDto administrator)
    {
        var employee = await _context.Employees.FindAsync(administrator.EmployeeId);
        if (employee == null)
        {
            return null;
        }
        
        administrator.Password = BCrypt.Net.BCrypt.HashPassword(administrator.Password);

        var newAdministrator = new Administrator
        {
            EmployeeId = administrator.EmployeeId,
            Email = administrator.Email,
            Password = administrator.Password,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        
        var saveAdministrator = _context.Administrators.Add(newAdministrator).Entity;
        await _context.SaveChangesAsync();
        return saveAdministrator;
    }
    
    // Update an administrator password
    public async Task<Administrator?> UpdatePasswordAdministrator(int id, AdministratorUpdatePasswordDto administrator)
    {
        var administratorToUpdate = await _context.Administrators.FindAsync(id);
        if (administratorToUpdate == null)
        {
            return null;
        }
        
        administratorToUpdate.Password = BCrypt.Net.BCrypt.HashPassword(administrator.Password);
        administratorToUpdate.UpdatedAt = DateTime.Now;
        
        _context.Administrators.Update(administratorToUpdate);
        await _context.SaveChangesAsync();
        return administratorToUpdate;
    }
    
    // Update an administrator email
    public async Task<Administrator?> UpdateEmailAdministrator(int id, AdministratorUpdateEmailDto administrator)
    {
        var administratorToUpdate = await _context.Administrators.FindAsync(id);
        if (administratorToUpdate == null)
        {
            return null;
        }
        
        administratorToUpdate.Email = administrator.Email;
        administratorToUpdate.UpdatedAt = DateTime.Now;
        
        _context.Administrators.Update(administratorToUpdate);
        await _context.SaveChangesAsync();
        return administratorToUpdate;
    }
    
    // Delete an administrator
    public async Task<Administrator?> DeleteAdministrator(int id)
    {
        var adminIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (adminIdString is null) return null;
        var adminId = int.Parse(adminIdString);
        if (adminId == id) return null;
        
        var administrators = await _context.Administrators.Take(5).ToListAsync();
        if (administrators.Count == 1) return null;
        
        var administratorToDelete = await _context.Administrators.FindAsync(id);
        if (administratorToDelete == null)
        {
            return null;
        }
        
        _context.Administrators.Remove(administratorToDelete);
        await _context.SaveChangesAsync();
        return administratorToDelete;
    }
    
    // Create a simple token
    private string CreateToken(int id)
    {
        var guid = Guid.NewGuid().ToString();
        var datePlusOneDay = DateTime.Now.AddDays(1);
        var datePlusOneDayTimestamp = new DateTimeOffset(datePlusOneDay).ToUnixTimeSeconds();
        var timestampToBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(datePlusOneDayTimestamp.ToString()));
        var employeeIdToBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(id.ToString()!));

        var token = employeeIdToBase64 + "-" + timestampToBase64 + "-" + guid;
        var tokenToBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

        return tokenToBase64;
    }
    
    // Check administrator authenticity
    public async Task<bool> CheckAdministratorAuthenticity(Administrator administrator)
    {
        var administratorToCheck = await _context.Administrators
            .Include(a => a.Employee)
            .FirstOrDefaultAsync(a => a.Id == administrator.Id);
        
        if (administratorToCheck == null) return false;
        
        return administratorToCheck.EmployeeId == administrator.EmployeeId;
    }

    // Login an administrator
    public async Task<Administrator?> LoginAdministrator(AdministratorLoginDto administrator)
    {
        var administratorToLogin = await _context.Administrators.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Email == administrator.Email);
        if (administratorToLogin == null)
        {
            return null;
        }

        var passwordMatch = BCrypt.Net.BCrypt.Verify(administrator.Password, administratorToLogin.Password);
        if (!passwordMatch)
        {
            return null;
        }
        
        var token = CreateToken(administratorToLogin.Id);

        administratorToLogin.Token = token;
        administratorToLogin.LastLogin = DateTime.Now;
        
        _context.Administrators.Update(administratorToLogin);
        await _context.SaveChangesAsync();
        return administratorToLogin;
    }

    public async Task<Administrator?> PersistConnection(AdministratorPersistDto administrator)
    {
        var administratorToPersist = await _context.Administrators.Include(a => a.Employee).FirstOrDefaultAsync(a => a.Token == administrator.Token);
        if (administratorToPersist is null)
        {
            return null;
        }
        
        var token = Convert.FromBase64String(administrator.Token!);
        var tokenDecoded = Encoding.UTF8.GetString(token);
        var tokenSplit = tokenDecoded.Split("-");

        var timestamp = Convert.FromBase64String(tokenSplit[1]);
        var timestampDecoded = Encoding.UTF8.GetString(timestamp);
        var timestampDecodedInt = Convert.ToInt64(timestampDecoded);
        var timestampDecodedDateTime = DateTimeOffset.FromUnixTimeSeconds(timestampDecodedInt).DateTime;
        
        if (DateTime.Now > timestampDecodedDateTime)
        {
            return null;
        }
        
        var newToken = CreateToken(administratorToPersist.Id);
        
        administratorToPersist.Token = newToken;
        administratorToPersist.LastLogin = DateTime.Now;
        
        _context.Administrators.Update(administratorToPersist);
        await _context.SaveChangesAsync();
        return administratorToPersist;
    }

    #endregion
}