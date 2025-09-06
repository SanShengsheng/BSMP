using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MQKJ.BSMP.Authentication.JwtBearer
{
    public struct ExternalJwtRegisteredClaimNames
    {
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Actort = "actort";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Typ = "typ";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Sub = "sub";
        //
        // 摘要:
        //     http://openid.net/specs/openid-connect-frontchannel-1_0.html#OPLogout
        public const string Sid = "sid";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Prn = "prn";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Nbf = "nbf";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Nonce = "nonce";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string NameId = "nameid";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Jti = "jti";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Iss = "iss";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Iat = "iat";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string GivenName = "given_name";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string FamilyName = "family_name";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Gender = "gender";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Exp = "exp";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Email = "email";
        //
        // 摘要:
        //     http://openid.net/specs/openid-connect-core-1_0.html#CodeIDToken
        public const string AtHash = "at_hash";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string CHash = "c_hash";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Birthdate = "birthdate";
        //
        // 摘要:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string Azp = "azp";
        //
        // 摘要:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string AuthTime = "auth_time";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Aud = "aud";
        //
        // 摘要:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string Amr = "amr";
        //
        // 摘要:
        //     http://openid.net/specs/openid-connect-core-1_0.html#IDToken
        public const string Acr = "acr";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string UniqueName = "unique_name";
        //
        // 摘要:
        //     http://tools.ietf.org/html/rfc7519#section-4
        public const string Website = "website";

        public const string PlayerId = "PlayerId";

        public const string AgentId = "AgentId";

        public const string UnionId = "UnionId";

        public const string UserId = "UserId";

        public const string OpenId = "OpenId";
    }
}
