using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IAnimalRepository AnimalRepository { get; }
        void Save();
    }
}
