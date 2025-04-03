using Business.Models.Members;
using Data.Entities;


namespace Business.Factories;

public class MemberFactory
{

    public static MemberEntity Create(MemberSignUpModel signupForm)
    {
        return new MemberEntity
        {
            UserName = signupForm.Email,
            FirstName = signupForm.FirstName,
            LastName = signupForm.LastName,
            Email = signupForm.Email,

        };
    }

    public static Member Create(MemberEntity entity)
    {
        return new Member
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.PhoneNumber,
            JobTitle = entity.JobTitle,
            Adress = entity.Adress is not null ? new MemberAdress
            {
                StreetName = entity.Adress.StreetName,
                PostalCode = entity.Adress.PostalCode,
                City = entity.Adress.City
            } : null
        };
    }

}
