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
        void UpdateProposalDetails(EditProposalDetailsViewModel proposalDetailsViewModel);
        void DeleteProposal(int ProposalID);
        IEnumerable<ProposalDetailsViewModel> GetProposals();
        IEnumerable<ProposalDetailsViewModel> GetProposalsByUserId(string UserId);
        ProposalDetailsViewModel GetProposalByProposalId(int ProposalID);
        Task AcceptOrRejectProposal(int proposalId, int statusId); 
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

        public void UpdateProposalDetails(EditProposalDetailsViewModel proposalDetailsViewModel) {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditProposalDetailsViewModel, Proposal>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Proposal editedProposal = mapper.Map<EditProposalDetailsViewModel, Proposal>(proposalDetailsViewModel);
            proposalsRepository.UpdateProposalDetails(editedProposal);
        }

        public void DeleteProposal(int ProposalId)
        {
            proposalsRepository.DeleteProposal(ProposalId);
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


        public ProposalDetailsViewModel GetProposalByProposalId(int ProposalID)
        {
            var proposalFromRepo = proposalsRepository.GetProposalByProposalID(ProposalID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Proposal, ProposalDetailsViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            var proposal = mapper.Map<Proposal, ProposalDetailsViewModel>(proposalFromRepo.Result);
            return proposal;
        }

        public async Task AcceptOrRejectProposal(int proposalId, int statusId)
        {
            await proposalsRepository.AcceptOrRejectProposal(proposalId, statusId);
        }
    }
}
