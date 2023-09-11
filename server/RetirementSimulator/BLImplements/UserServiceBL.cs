using AutoMapper;
using BL.DTO;
using DL.DalApi;
using DL.DataObjects;
using MongoDB.Driver;
using System.Reflection;
namespace BL.BLImplements;
public class UserServiceBL : IUserServiceBL
{
    readonly IUserService userService;
    readonly IMapper userMapper;

    public UserServiceBL(IUserService userService, IMapper userMapper)
    {
        this.userService = userService;
        this.userMapper = userMapper;
    }
    public async Task<List<UserDTO>> GetAllAsync()
    {
        List<User> users = await userService.GetAllAsync();
        List<UserDTO> result = new();
        users.ForEach(u => result.Add(userMapper.Map<UserDTO>(u)));
        return result;
    }
    public async Task<bool> CreateAsync(UserDTO userDTO)
    {
        User user = userMapper.Map<User>(userDTO);
        /*if (user.Role.Equals("Admin")|| user.Role.Equals("admin"))
        {
            throw new InvalidParameterException("the role is not valid");
        }*/
        var filter = Builders<User>.Filter.Eq("Email", user.Email);
        var result = await userService.GetAsync(filter);
        if (result is not null)
        {
            throw new Exception("email is not valid");
        }
        user.Password = Auth.HashPassword(userDTO.Password);
        return await userService.CreateAsync(user);
    }
    public Task<bool> DeleteAsync(UserDTO user)
    {
        var filter = UserServiceBL.GetCurrentFilter(user);
        return userService.DeleteAsync(filter); ;
    }
    public async Task<bool> UpdateAsync(UserDTO current, UserDTO updateDetail)
    {
        UpdateDefinition<User> update = null;
        foreach (PropertyInfo prop in updateDetail.GetType().GetProperties())
        {
            if (prop.PropertyType == typeof(string))
            {

                if (prop.GetValue(updateDetail).ToString().Length > 0)
                {
                    update = Builders<User>.Update.Set(prop.Name, prop.GetValue(updateDetail));
                }
            }
        }
        var filter = GetCurrentFilter(current);
        return await userService.UpdateAsync(filter, update);
    }

    public async Task<UserDTO> GetAsync(UserDTO user)
    {
        var filter = GetCurrentFilter(user);
        User userResult = await userService.GetAsync(filter);
        return userMapper.Map<UserDTO>(userResult);
    }
    public static FilterDefinition<User> GetCurrentFilter(UserDTO user)
    {
        var filter = Builders<User>.Filter.Eq("Name", user.Name);

        foreach (PropertyInfo prop in user.GetType().GetProperties())
        {
            if (prop.PropertyType == typeof(string))
            {
                if (prop.Name != "SubscriptionPeriodDate")
                {
                    if (prop.GetValue(user).ToString().Length > 0)
                    {
                        filter = Builders<User>.Filter.Eq(prop.Name, prop.GetValue(user));
                    }
                }
            }
        }
        return filter;
    }


    public UserDTO Login(string email, string pas)
    {
        try
        {
            UserDTO u = new UserDTO() { Password = pas };
            var filter = Builders<User>.Filter.Eq("Email", email);
            User theUser = userService.GetAsync(filter).Result;
            if (Auth.VerifyPassword(pas,theUser.Password))
            {
                if (theUser.SubscriptionPeriodDate <= DateTime.Now)
                {
                    return userMapper.Map<UserDTO>(theUser);
                }
                else
                {
                    throw new SubscriptionIsNotValid();

                }
            }
        }
        catch (Exception)
        {
            return null;
        }
        return null;
    }
}
