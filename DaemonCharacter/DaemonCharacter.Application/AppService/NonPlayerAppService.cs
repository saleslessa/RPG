using DaemonCharacter.Application.Interfaces;
using System;
using System.Collections.Generic;
using DaemonCharacter.Infra.Data.Interfaces;
using DaemonCharacter.Application.ViewModels.NonPlayer;
using DaemonCharacter.Domain.Interfaces.Service;
using AutoMapper;
using DaemonCharacter.Domain.Entities;

namespace DaemonCharacter.Application.AppService
{
    public class NonPlayerAppService : ApplicationService, INonPlayerAppService
    {
        private readonly INonPlayerService _nonPlayerService;

        public NonPlayerAppService(INonPlayerService nonPlayerService, IUnitOfWork uow) : base(uow)
        {
            _nonPlayerService = nonPlayerService;
        }

        public NonPlayerViewModel Add(NonPlayerViewModel model)
        {
            var obj = Mapper.Map<NonPlayerViewModel, NonPlayer>(model);

            obj = _nonPlayerService.Add(obj);
            Commit();

            return Mapper.Map<NonPlayer, NonPlayerViewModel>(obj);
        }

        public void Dispose()
        {
            _nonPlayerService.Dispose();
            GC.SuppressFinalize(this);
        }

        public NonPlayerViewModel GetByName(string name)
        {
            return Mapper.Map<NonPlayer, NonPlayerViewModel>(_nonPlayerService.GetByName(name));
        }

        public IEnumerable<NonPlayerViewModel> ListAll()
        {
            return Mapper.Map<IEnumerable<NonPlayer>, IEnumerable<NonPlayerViewModel>>(_nonPlayerService.ListAll());
        }

        public void Remove(Guid id)
        {
            _nonPlayerService.Remove(id);
        }

        public NonPlayerViewModel Update(NonPlayerViewModel model)
        {
            var obj = Mapper.Map<NonPlayerViewModel, NonPlayer>(model);

            obj = _nonPlayerService.Update(obj);
            Commit();

            return Mapper.Map<NonPlayer, NonPlayerViewModel>(obj);
        }
    }
}
