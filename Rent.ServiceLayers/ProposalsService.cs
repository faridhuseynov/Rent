using AutoMapper;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ViewModels.ProposalViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    //this service not written yet, the reason is because it is not planned to have separate 
    public interface IProposalsService 
    {
        Task<int> InsertProposal(NewProposalViewModel newProposalViewModel);
        Task UpdateProposalDetails(EditProposalDetailsViewModel proposalDetailsViewModel);
        Task DeleteProposal(int ProposalID);
        IEnumerable<ProposalDetailsViewModel> GetProposals();
        IEnumerable<ProposalDetailsViewModel> GetProposalsByUserId(string UserId);
        Task<ProposalDetailsViewModel> GetProposalByProposalId(int ProposalID);
        Task AcceptOrRejectProposal(int proposalId, int statusId, DateTime responseDate); 
    }
    public class ProposalsService:IProposalsService
    {
        private readonly IProposalsRepository proposalsRepository;

        public ProposalsService(IProposalsRepository proposalsRepository)
        {
            this.proposalsRepository = proposalsRepository;
        }

        public async Task<int> InsertProposal(NewProposalViewModel newProposalViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewProposalViewModel, Proposal>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Proposal newProposal = mapper.Map<NewProposalViewModel, Proposal>(newProposalViewModel);
            return await proposalsRepository.AddProposal(newProposal);
        }

        public async Task UpdateProposalDetails(EditProposalDetailsViewModel proposalDetailsViewModel) {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditProposalDetailsViewModel, Proposal>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Proposal editedProposal = mapper.Map<EditProposalDetailsViewModel, Proposal>(proposalDetailsViewModel);
            await proposalsRepository.UpdateProposalDetails(editedProposal);
        }

        public async Task DeleteProposal(int ProposalId)
        {
            await proposalsRepository.DeleteProposal(ProposalId);
        }

        public IEnumerable<ProposalDetailsViewModel> GetProposals()
        {
            var proposalsList = proposalsRepository.GetProposals();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Proposal, ProposalDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var proposals = mapper.Map<IEnumerable<Proposal>, IEnumerable<ProposalDetailsViewModel>>(proposalsList.Result);
            return proposals;
        }

        public IEnumerable<ProposalDetailsViewModel> GetProposalsByUserId(string UserId)
        {
            var proposalsList = proposalsRepository.GetProposalsByUserId(UserId);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Proposal, ProposalDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var proposals = mapper.Map<IEnumerable<Proposal>, IEnumerable<ProposalDetailsViewModel>>(proposalsList.Result);
            return proposals;
        }


        public async Task<ProposalDetailsViewModel> GetProposalByProposalId(int ProposalID)
        {
            var proposalFromRepo = await proposalsRepository.GetProposalByProposalID(ProposalID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Proposal, ProposalDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var proposal = mapper.Map<Proposal, ProposalDetailsViewModel>(proposalFromRepo);
            return proposal;
        }

        public async Task AcceptOrRejectProposal(int proposalId, int statusId, DateTime responseDate)
        {
            await proposalsRepository.AcceptOrRejectProposal(proposalId, statusId, responseDate);
        }
    }
}
