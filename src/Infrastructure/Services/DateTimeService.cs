using FoodOnline.Application.Common.Interfaces;
using System;

namespace FoodOnline.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}