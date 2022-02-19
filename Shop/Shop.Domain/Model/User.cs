using System;
using System.ComponentModel.DataAnnotations;
using Shop.Domain.DTO;

namespace Shop.Domain
{
    public class User
    {
        private readonly UserDto _userDto;

        public User(UserDto userDto)
        {
            this._userDto = userDto;
        }
        public Guid Id { get => _userDto.Id; }

        public string Name 
        { 
            get => _userDto.Name;
            set => _userDto.Name = value;
        }

        [RegularExpression(@"^[a-zA-Z0-9]+\@[a-z]{2,15}\.[a-z]{2,5}$")]
        public string Email 
        {
            get => _userDto.Email;
            set => _userDto.Email = value; 
        }

        [RegularExpression(@"/(?:\+|\d)[\d\-\(\) ]{9,}\d/g")]
        public string NumberPhone
        {
            get => _userDto.NumberPhone;
            set => _userDto.NumberPhone = value;
        }
        public string Password 
        { 
            get => _userDto.Password; 
        }


        public static class Mapper
        {
            public static User Map(UserDto dto) => new User(dto);

            public static UserDto Map(User domain) => domain._userDto;
        }

    }
}
