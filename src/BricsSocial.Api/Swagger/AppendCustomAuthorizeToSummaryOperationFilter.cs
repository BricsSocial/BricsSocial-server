
using BricsSocial.Application.Common.Security;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

namespace BricsSocial.Api.Swagger
{
    public class AppendCustomAuthorizeToSummaryOperationFilter : IOperationFilter
    {
        private readonly AppendCustomAuthorizeToSummaryOperationFilter<AuthorizeAttribute> filter;

        public AppendCustomAuthorizeToSummaryOperationFilter()
        {
            var policySelector = new PolicySelectorWithLabel<AuthorizeAttribute>
            {
                Label = "policies",
                Selector = authAttributes =>
                    authAttributes
                        .Where(a => !string.IsNullOrEmpty(a.Policy))
                        .Select(a => a.Policy)
            };

            var rolesSelector = new PolicySelectorWithLabel<AuthorizeAttribute>
            {
                Label = "roles",
                Selector = authAttributes =>
                    authAttributes
                        .Where(a => !string.IsNullOrEmpty(a.Roles))
                        .Select(a => a.Roles)
            };

            filter = new AppendCustomAuthorizeToSummaryOperationFilter<AuthorizeAttribute>(new[] { policySelector, rolesSelector }.AsEnumerable());
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            filter.Apply(operation, context);
        }
    }

    public class AppendCustomAuthorizeToSummaryOperationFilter<T> : IOperationFilter where T : Attribute
    {
        private readonly IEnumerable<PolicySelectorWithLabel<T>> policySelectors;

        /// <summary>
        /// Constructor for AppendAuthorizeToSummaryOperationFilter
        /// </summary>
        /// <param name="policySelectors">Used to select the authorization policy from the attribute e.g. (a => a.Policy)</param>
        public AppendCustomAuthorizeToSummaryOperationFilter(IEnumerable<PolicySelectorWithLabel<T>> policySelectors)
        {
            this.policySelectors = policySelectors;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authorizeAttributes = context.GetActionMediatRRequestAttributes<T>();

            if (authorizeAttributes.Any())
            {
                var authorizationDescription = new StringBuilder(" (Auth");

                foreach (var policySelector in policySelectors)
                {
                    AppendPolicies(authorizeAttributes, authorizationDescription, policySelector);
                }

                operation.Summary += authorizationDescription.ToString().TrimEnd(';') + ")";
            }
        }

        private void AppendPolicies(IEnumerable<T> authorizeAttributes, StringBuilder authorizationDescription, PolicySelectorWithLabel<T> policySelector)
        {
            var policies = policySelector.Selector(authorizeAttributes)
                .OrderBy(policy => policy)
                .Select(policy => policy.Replace(",", ", "));

            if (policies.Any())
            {
                authorizationDescription.Append($" {policySelector.Label}: {string.Join(", ", policies)};");
            }
        }
    }
}
