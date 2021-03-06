﻿using System;
using System.Collections.Generic;
using DaemonCharacter.Domain.Entities;
using DaemonCharacter.Domain.Interfaces.Repository;
using DaemonCharacter.Infra.Data.Context;
using System.Linq;

namespace DaemonCharacter.Infra.Data.Repository
{
    public class AttributeRepository : Repository<Attributes>, IAttributeRepository
    {
        public AttributeRepository(DaemonCharacterContext context) : base(context)
        {
        }

        public void Remove(Guid? id)
        {
            Remove(id);
        }

        public IEnumerable<Attributes> GetBonusAttribute(Guid? id)
        {
            return Search(t => t.ParentAttribute.Contains(
                            db.Attributes.Where(tt => tt.AttributeId == id).FirstOrDefault()
                        )
                    )
                    .ToList();
        }

        public Attributes GetByName(string name)
        {
            return Search(t => t.AttributeName.ToUpper().Trim() == name.ToUpper().Trim())
                .FirstOrDefault();
        }

        public Attributes GetUpdateable(Guid id, string name)
        {
            return Search(t => t.AttributeName.ToUpper().Trim() == name.ToUpper().Trim() && (t.AttributeId != id))
                .FirstOrDefault();
        }

        public IEnumerable<Attributes> ListWithBonus(Guid? id)
        {
            return Search(t => t.AttributeType != AttributeType.Characteristic && t.AttributeId != id).ToList();
        }

        public void RemoveChilds(Guid? id)
        {
            IEnumerable<Attributes> ListChild = GetBonusAttribute(id);

            foreach (var item in ListChild)
            {
                RemoveParent(item.AttributeId);
            }
        }

        public void RemoveParent(Guid? id)
        {
            var obj = Get(id);

            obj.ParentAttribute.Clear();
            Update(obj);
        }

        public IEnumerable<Attributes> SearchByName(string name)
        {
            return Search(t => t.AttributeName == name);
        }
    }
}
