﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using DonVo.EventSourcing.Ordering.Application.Mapper;
using DonVo.EventSourcing.Ordering.Application.PipelineBehaviours;
using System.Reflection;

namespace DonVo.EventSourcing.Ordering.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), ServiceLifetime.Transient);
            services.AddMediatR(Assembly.GetExecutingAssembly());

            #region Pipeline Behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            #endregion

            #region Configure Mapper
            var config = new MapperConfiguration(configure =>
            {
                configure.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                configure.AddProfile<OrderMappingProfile>();
            });
            var mapper = config.CreateMapper();
            #endregion
        }
    }
}
