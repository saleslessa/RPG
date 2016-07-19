using DaemonCharacter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DaemonCharacter.Domain.Interfaces.Service
{
    public interface IItemService : IDisposable
    {

        Item Add(Item model);

        Item Get(Guid? id);

        IEnumerable<Item> ListAll();

        Item Update(Item model);

        void Remove(Guid id);
    }
}
