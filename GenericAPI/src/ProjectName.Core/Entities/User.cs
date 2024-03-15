using System;
using System.Collections.Generic;
using ProjectName.Core.DTOs;

namespace ProjectName.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate, string password, string role)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.UtcNow;
            Active = true;
            Password = password;
            Role = role;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public static NotificationInfoDTO ToDTO(User user)
        {
            return new NotificationInfoDTO(user.Id, user.FullName, user.Email);
        }

    }
}
