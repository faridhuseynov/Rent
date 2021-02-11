using AutoMapper;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ViewModels.ProposalTypeViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    public interface IProposalTypesService
    {
        Task<IEnumerable<ProposalTypeDetailsViewModel>> GetProposalTypes();
        Task<ProposalTypeDetailsViewModel> GetProposalType(int proposalTypeId);
    }
    public class ProposalTypesService:IProposalTypesService
    {
        private readonly IProposalTypesRepository proposalTypesRepository;

        public ProposalTypesService(IProposalTypesRepository proposalTypesRepository)
        {
            this.proposalTypesRepository = proposalTypesRepository;
        }

        public async Task<ProposalTypeDetailsViewModel> GetProposalType(int proposalTypeId)
        {
            var proposalType = await proposalTypesRepository.GetProposalType(proposalTypeId);
            ProposalTypeDetailsViewModel ptdvm = new ProposalTypeDetailsViewModel();
            if (proposalType!=null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProposalType, ProposalTypeDetailsViewModel>();
                    cfg.IgnoreUnmapped();
                });
                IMapper mapper = config.CreateMapper();
                ptdvm = mapper.Map<ProposalType, ProposalTypeDetailsViewModel>(proposalType);
            }
                return ptdvm;

        }

        public async Task<IEnumerable<ProposalTypeDetailsViewModel>> GetProposalTypes()
        {
            var propTypesList = await proposalTypesRepository.GetProposalTypes();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProposalType, ProposalTypeDetailsViewModel>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            var proposalTypeViewModelList = mapper.Map<IEnumerable<ProposalType>, IEnumerable<ProposalTypeDetailsViewModel>>(propTypesList);
            return proposalTypeViewModelList;
        }
    }
}
