using System.Collections.Generic;

namespace LearningDDDwithCSharp.Infraestructure.Common.Domain
{
    public abstract class EntityBase<T>
    {
        public T Id { get; set; }

        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public override bool Equals(object entity)
        {

            return entity != null && entity is EntityBase<T>
                   && this == (EntityBase<T>) entity;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<T> entity1, EntityBase<T> entity2)
        {
            if ((object) entity1 == null && (object) entity2 == null)
            {
                return true;
            }
            if ((object) entity1 == null || (object) entity2 == null)
            {
                return false;
            }
            if (entity1.Id.ToString() == entity2.Id.ToString())
            {
                return true;
            }

            return false;

        }

        public static bool operator !=(EntityBase<T> entity1, EntityBase<T> entity2)
        {
            return (!(entity1 == entity2));
        }

        protected abstract void Validate();

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

    }
}
