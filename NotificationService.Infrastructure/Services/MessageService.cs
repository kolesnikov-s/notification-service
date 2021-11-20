﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotificationService.Application;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Interfaces.MessageServices;

namespace NotificationService.Infrastructure.Services
{
    public class MessageService: IMessageService
    {
        private readonly ITelegramMessageService _telegramMessageService;
        private readonly ISmsMessageService _smsMessageService;
        private readonly IEmailMessageService _emailMessageService;

        public MessageService(
            ITelegramMessageService telegramMessageService, 
            ISmsMessageService smsMessageService,
            IEmailMessageService emailMessageService)
        {
            _telegramMessageService = telegramMessageService;
            _smsMessageService = smsMessageService;
            _emailMessageService = emailMessageService;
        }
        
        public async Task SendMessage(string type, string contact, string message)
        {
            var provider = GetProviderByMessageType(type);
            
            await provider.SendMessage(contact, message);
        }

        private IMessageSender GetProviderByMessageType(string type)
        {
            IMessageSender provider = type.ToLower() switch
            {
                MessageType.Sms => _smsMessageService,
                MessageType.Email => _emailMessageService,
                MessageType.Telegram => _telegramMessageService,
                _ => throw new Exception($"message type {type} not found")
            };

            return provider;
        }
    }
}