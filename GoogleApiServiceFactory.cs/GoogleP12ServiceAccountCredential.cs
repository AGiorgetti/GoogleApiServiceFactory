﻿using Google.Apis.Auth.OAuth2;
using System.Security.Cryptography.X509Certificates;

namespace GoogleApiServiceFactory
{
    internal class GoogleP12ServiceAccountCredential : IGoogleServiceAccount
    {
        public ServiceAccountCredential BuildCredential(GoogleServiceParameter parameters)
        {
            var credential = new X509Certificate2(parameters.AccountFilePath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            var serviceAccountCredential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(parameters.AccountEmail)
            {
                Scopes = parameters.ApiScopes,
                User = string.IsNullOrEmpty(parameters.UserToImpersonate) ? parameters.AccountAdminUser : parameters.UserToImpersonate
            }.FromCertificate(credential));
            return serviceAccountCredential;
        }
    }
}
