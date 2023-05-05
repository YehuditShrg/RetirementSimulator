﻿using BL;
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
    public bool CreateUser(UserDTO user)
    {
        return userServiceBL.CreateAsync(user).Result;
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
        return userServiceBL.Login(email, pass);
    }
    [HttpPost("GetPensionCalculates")]
    public string CreatePensionService(string pensionType, [FromBody] object employee)
    {
        try
        {
            return pensionFactory.Create(pensionType, employee);
        }
        catch (InvalidParameterException ex)
        {
            throw ex;
        }
    }

    [HttpPost("!!!")]
    public Employee GetEmp()
    {
        return new Employee();
    }

}

