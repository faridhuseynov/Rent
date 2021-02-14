using AutoMapper;
using Rent.DomainModels.Models;
using Rent.Repositories;
using Rent.ViewModels.MessageViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rent.ServiceLayers
{
    public interface IMessagesService
    {
        Task<int> AddNewMessage(NewMessageViewModel newMessage);
        Task<IEnumerable<MessageReviewViewModel>> GetMessagesForUserByUserId(string userId);
    }
    public class MessagesService : IMessagesService
    {
        private readonly IMessageRepository messageRepository;

        public MessagesService(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public async Task<int> AddNewMessage(NewMessageViewModel newMessage)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NewMessageViewModel, Message>();
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            Message message = mapper.Map<NewMessageViewModel, Message>(newMessage);
            return await messageRepository.AddMessage(message);
        }

        public async Task<IEnumerable<MessageReviewViewModel>> GetMessagesForUserByUserId(string userId)
        {
            var messages = await messageRepository.GetMessagesForUser(userId);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Message, MessageReviewViewModel>()
                .ForMember(destination=>destination.RecipientUsername,map=>map.MapFrom(
                    source=>source.Recipient.UserName))
                .ForMember(destination=> destination.RecipientMainPhotoUrl,map=>map.MapFrom(
                    source=>source.Recipient.MainProfilePicture))
                .ForMember(destination=>destination.SenderUsername,map=>map.MapFrom(
                    source=>source.Sender.UserName))
                .ForMember(destination=>destination.SenderMainPhotoUrl,map=>map.MapFrom(
                    source=>source.Sender.MainProfilePicture));
                cfg.IgnoreUnmapped();
            });
            IMapper mapper = config.CreateMapper();
            var messageList = mapper.Map<IEnumerable<Message>, IEnumerable<MessageReviewViewModel>>(messages);
            return messageList;
        }
    }
}
