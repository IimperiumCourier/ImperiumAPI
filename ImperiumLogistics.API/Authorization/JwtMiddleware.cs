using ImperiumLogistics.Domain.CompanyAggregate;
using ImperiumLogistics.Infrastructure.Abstract;
using ImperiumLogistics.Infrastructure.UserRetrievalHandler;
using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

namespace ImperiumLogistics.API.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITokenGenerator tokenGenerator)
        {
            var companyRepo = context.RequestServices.GetRequiredService<ICompanyRepository>();
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            UserRetrievalFactory retrievalFactory = new UserRetrievalFactory(companyRepo);
            UserHandler userHandler = retrievalFactory.GetUser();

            var claims = tokenGenerator.ValidateToken(token);
            if (claims != null)
            {
                string userName = claims[JwtRegisteredClaimNames.Name];
                await userHandler.GetUserByUsername(userName);
                // attach user to context on successful jwt validation
                context.Items["User"] = userHandler.User;
                context.Items["UserType"] = userHandler.UserType;
            }

            await _next(context);
        }
    }
}
