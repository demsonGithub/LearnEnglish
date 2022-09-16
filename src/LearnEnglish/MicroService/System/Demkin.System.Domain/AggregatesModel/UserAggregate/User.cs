﻿using Demkin.System.Domain.Events;

namespace Demkin.System.Domain.AggregatesModel.UserAggregate
{
    public class User : Entity<long>, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string Password { get; private set; }

        public Address? Address { get; private set; }

        private readonly List<UserRoleRelation> _userRoleRelations;
        public IReadOnlyCollection<UserRoleRelation> UserRoleRelations => _userRoleRelations;

        private User()
        {
            _userRoleRelations = new List<UserRoleRelation>();
        }

        public User(string userName, string password, Address? address)
        {
            Id = IdGenerateHelper.Instance.GenerateId();
            UserName = userName;
            Password = password;
            Address = address;

            AddDomainEvent(new UserCreatedDomainEvent(this));
        }
    }
}