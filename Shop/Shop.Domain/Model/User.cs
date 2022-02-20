using PhoneNumbers;
using Shop.Domain.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
            set
            {
                if (!IsEmail(value))
                    throw new InvalidOperationException();

                _userDto.Email = value;
            }
        }

        [RegularExpression(@"/(?:\+|\d)[\d\-\(\) ]{9,}\d/g")]
        public string NumberPhone
        {
            get => _userDto.NumberPhone;

            set
            {
                if (value == null || !IsNumberPhone(value, out string cellPhone))
                    throw new ArgumentException("no correct numberphone for Maker" + Id.ToString());

                _userDto.NumberPhone = cellPhone;
            }// _userDto.NumberPhone = value;

        }
        public string Password
        {
            get => _userDto.Password;
        }

        private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
        public static bool IsNumberPhone(string cellPhone, out string formattedPhone)
        {
            try
            {
                var phoneNumber = phoneNumberUtil.Parse(cellPhone, "ru");
                formattedPhone = phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.INTERNATIONAL);
                return true;
            }
            catch (NumberParseException)
            {
                formattedPhone = null;
                return false;
            }
        }

        public static bool IsEmail(string email)
        {
            if (email == null)
                return false;

            return Regex.IsMatch(email, @"^[a-zA-Z0-9]+\@[a-z]{2,10}\.[a-z]{2,5}$");
        }
        public static class Mapper
        {
            public static User Map(UserDto dto) => new User(dto);

            public static UserDto Map(User domain) => domain._userDto;
        }

    }
}
