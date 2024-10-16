 import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44320/',
  redirectUri: baseUrl,
  clientId: 'MultiLanguage_App',
  responseType: 'code',
  scope: 'offline_access MultiLanguage',
  requireHttps: true,
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'MultiLanguage',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44320',
      rootNamespace: 'MultiLanguage',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
