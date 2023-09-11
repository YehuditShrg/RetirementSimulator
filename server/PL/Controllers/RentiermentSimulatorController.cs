using BL;
using BL.BLImplements;
using BL.DTO;
using BL.Pension;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

[ApiController]
[Route("[controller]")]
public class RentiermentSimulatorController : ControllerBase
{
    IUserServiceBL userServiceBL;

    IPensionFactory pensionFactory;
    public RentiermentSimulatorController(IUserServiceBL us, IPensionFactory pf)
    {
        userServiceBL = us;
        pensionFactory = pf;
    }

    [HttpGet("GetAll")]
    public List<UserDTO> GetAll()
    {
        return userServiceBL.GetAllAsync().Result;
    }

    [HttpPost("GetDetails")]
    public UserDTO GetDetails(UserDTO user)
    {
        return userServiceBL.GetAsync(user).Result;
    }

    [HttpPost("CreateUser")]
    public bool CreateUser([FromBody] UserDTO user)
    {
        try { 
        return userServiceBL.CreateAsync(user).Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpDelete("DeleteUser")]
    public bool DeleteUser(UserDTO user)
    {
        return userServiceBL.DeleteAsync(user).Result;
    }

    [HttpPut("Update")]
    public bool UpdateUser(params UserDTO[] user)
    {
        return userServiceBL.UpdateAsync(user[0], user[1]).Result;
    }

    [HttpPost("Login")]
    public UserDTO Login(string email, [FromBody] string pass)
    {
        var r=userServiceBL.Login(email, pass);
        return r;
    }

    [HttpPost("IsAdmin")]
    public bool IsAdmin(UserDTO user)
    {
        if (user.Role.Equals("admin") || user.Role.Equals("ADMIN") || user.Role.Equals("Admin"))
        {
            return true;
        }
        return false;
    }
    [HttpPost("GetPensionCalculates")]
    public object CreatePensionService(string pensionType, [FromBody] object employee)
    {
        try
        {
           var o = employee;
           return pensionFactory.Create(pensionType, employee);
        }
        catch (InvalidParameterException ex)
        {
            throw ex;
        }
    }
    [HttpPost("GetBudgetEmp")]
    public Employee GetBudgetEmp()
    {
        return new BudgetPensionEmployee();
    }

    [HttpPost("GetEmp")]
    public Employee GetEmp()
    {
        return new Employee();
    }



}

