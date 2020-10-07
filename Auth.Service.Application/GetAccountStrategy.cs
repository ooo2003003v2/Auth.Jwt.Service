using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Auth.Service.Domain.DBModel;
using Auth.Service.Domain.Model.Request;
using Auth.Service.Persistence;
using General.Input.Validifier;
namespace Auth.Service.Application
{
    public interface IGetAccountStrategy

    {
        public Account RequestAccount<T>(T input) ;

    }

    public class LoginByRequestInput : IGetAccountStrategy
    {
        public Account RequestAccount<T>(T input) 
        {
            Account ao = null;
            try
            {

                var _input = (input as LoginInput);
                
                ao = AccountPersistence.GetAccounts(new List<int>(), !GeneralValidifier.IsValidEmail(_input.login)?  new List<string>() { _input.login }: new List<string>(),
                    GeneralValidifier.IsValidEmail(_input.login) ? new List<string>() { _input.login } : new List<string>()
                    ).FirstOrDefault();
                if (ao == null)
                    return null;
                if (! BCrypt.Net.BCrypt.Verify(PasswordManager.DecryptFrontEndPass(_input.password), ao.Password))
                    return null;
                ao.Password = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");

            }
            return ao;
        }
    }

    public class LoginById: IGetAccountStrategy
    {
        public Account RequestAccount<T>(T input)
        {
            Account ao = null;
            try
            {


                ao = AccountPersistence.GetAccounts(new List<int>() { int.Parse(input + "") }, new List<string>(), new List<string>()).FirstOrDefault();
                if (ao == null)
                    return null;                
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");

            }
            return ao;
        }

    }

}
