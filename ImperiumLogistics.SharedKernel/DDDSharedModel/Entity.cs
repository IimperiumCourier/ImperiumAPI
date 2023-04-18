using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.SharedKernel.DDDSharedModel
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public TId Id { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
            {
                throw new ArgumentException("The ID cannot be the type's default value.", "id");
            }
            Id = id;
        }        // EF requires an empty constructor
        protected Entity()
        {
        }        // For simple entities, this may suffice

        public override bool Equals(object? otherObject)
        {
            var entity = otherObject as Entity<TId>;
            if (entity != null)
            {
                return this.Equals(entity);
            }
            return base.Equals(otherObject);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public bool Equals(Entity<TId>? other)
        {
            if (other == null)
            {
                return false;
            }
            return Id.Equals(other.Id);
        }
    }


}
