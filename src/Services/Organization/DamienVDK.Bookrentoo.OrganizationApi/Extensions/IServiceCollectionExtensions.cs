namespace DamienVDK.Bookrentoo.OrganizationApi.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection @this, IEnumerable<SecurityKey> keys)
    {
         @this.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
             options.Authority = "https://securetoken.google.com/bookrentoo";
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidIssuer = "https://securetoken.google.com/bookrentoo",
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidAudience = "bookrentoo",
                 ValidateLifetime = true,
                 IssuerSigningKeys = keys
             };
         });

        return @this;
    }

    public static IServiceCollection AddFirebaseAuthorization(this IServiceCollection @this)
    {
        @this.AddAuthorization(auth =>
        {
            auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                .RequireAuthenticatedUser().Build());
        });

        return @this;
    }
}
