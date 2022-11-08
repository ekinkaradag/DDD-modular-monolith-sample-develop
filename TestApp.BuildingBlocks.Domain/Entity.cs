using System;
using System.Collections.Generic;

namespace TestApp.BuildingBlocks.Domain
{
    public abstract class Entity
    {
        private Guid Id { get; }

        private readonly List<IDomainEvent> _domainEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected Entity(Guid id)
        {
            Id = id;
            _domainEvents = new List<IDomainEvent>();
        }

        protected Entity() : this(Guid.NewGuid())
        {
        }

        protected void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent">Domain event.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other)) return false;
            
            if (ReferenceEquals(this, other))
                return true;

            if (GetUnproxiedType(this) != GetUnproxiedType(other))
                return false;

            if (Id.Equals(default) || other.Id.Equals(default))
                return false;

            return Id.Equals(other.Id);

        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }

        /// <summary>
        /// <see cref="object.GetType()"/> return the type of the EntityFramework proxy, not the domain entity
        /// type. Use this method to get the type of the actual domain object.
        /// </summary>
        /// <param name="obj">the <see cref="Entity"/></param>
        /// <returns>the unproxied type</returns>
        public static Type GetUnproxiedType(object obj)
        {
            const string efCoreProxyPrefix = "Castle.Proxies.";

            var type = obj.GetType();
            var typeString = type.ToString();

            return (typeString.Contains(efCoreProxyPrefix) ? type.BaseType : type);
        }
    }
}