using System.ComponentModel.DataAnnotations;

namespace IntegrationLibrary.Core.Model
{
    public abstract class EntityClass
    {
        protected int _id;

        [Key]
        public int Id { get => _id; set => _id = value; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var entity = (EntityClass)obj;

            return Id == entity.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                return hash * 23 + Id.GetHashCode();
            }
        }

        public static bool operator ==(EntityClass a, EntityClass b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityClass a, EntityClass b)
        {
            return !(a == b);
        }
    }
}
