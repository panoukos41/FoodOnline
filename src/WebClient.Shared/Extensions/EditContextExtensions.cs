using System;
using System.Linq.Expressions;

namespace Microsoft.AspNetCore.Components.Forms
{
    public static class EditContextExtensions
    {
        public static bool IsInvalid<TField>(this EditContext editContext, Expression<Func<TField>> accessor) =>
           editContext.FieldCssClass(accessor) == "invalid";
    }
}