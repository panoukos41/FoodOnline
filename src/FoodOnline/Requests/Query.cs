using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Requests;

public abstract record Query<T> : IQuery<T>
{
}
