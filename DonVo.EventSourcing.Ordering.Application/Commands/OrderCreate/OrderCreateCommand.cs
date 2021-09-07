﻿using MediatR;
using DonVo.EventSourcing.Ordering.Application.Responses;
using System;

namespace DonVo.EventSourcing.Ordering.Application.Commands.OrderCreate
{
    public class OrderCreateCommand : IRequest<OrderResponse>
    {
        public string AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
