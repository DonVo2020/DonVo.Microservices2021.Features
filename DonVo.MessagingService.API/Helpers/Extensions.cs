﻿using FluentValidation;
using DonVo.MessagingService.API.Models.Message;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.MessagingService.API.Helpers
{
    public static class Extensions
    {
        public static KeyValuePair<bool, string> IsValid<T>(this AbstractValidator<T> validator, T obj)
        {
            var validationResult = validator.Validate(obj);
            if (validationResult.IsValid)
            {
                return new KeyValuePair<bool, string>(true, string.Empty);
            }
            else
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();
                var errorMessage = $"Invalid {typeof(T)} Request : {string.Join(',', errors)}. Request : '{System.Text.Json.JsonSerializer.Serialize(obj)}'";
                return new KeyValuePair<bool, string>(false, errorMessage);
            }
        }

        public static MessageDTO AsDTO(this Domain.Model.MessageModel messageEntity)
        {
            return new MessageDTO
            {
                Id = messageEntity.Id.ToString(),
                Message = messageEntity.Message,
                SenderUser = messageEntity.SenderUser,
                ReceiverUser = messageEntity.ReceiverUser,
            };
        }

    }


}
