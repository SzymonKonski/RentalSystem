
import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';
import { environment } from "../environments/environment";

const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;


export const b2cPolicies = {
  names: {
    signUpSignIn: "",
  },
  authorities: {
    signUpSignIn: {
      authority: "",
    }
  },
  authorityDomain: ""
};


export const msalConfig: Configuration = {
  auth: {
    clientId: '', // This is the ONLY mandatory field that you need to supply.
    authority: b2cPolicies.authorities.signUpSignIn.authority, // Defaults to "https://login.microsoftonline.com/common"
    knownAuthorities: [b2cPolicies.authorityDomain], // Mark your B2C tenant's domain as trusted.
    redirectUri: environment.urlAddress + '/', // Points to window.location.origin. You must register this URI on Azure portal/App Registration.
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage, // Configures cache location. "sessionStorage" is more secure, but "localStorage" gives you SSO between tabs.
    storeAuthStateInCookie: isIE, // Set this to "true" if you are having issues on IE11 or Edge
  },
  system: {
    loggerOptions: {
      loggerCallback(logLevel: LogLevel, message: string) {
        console.log(message);
      },
      logLevel: LogLevel.Verbose,
      piiLoggingEnabled: false
    }
  }
}

export const protectedResources = {
  carListApi: {
    endpoint: environment.urlAddress + '/api/cars',
    scopes: [""],
  },
  reservationApi: {
    endpoint: environment.urlAddress + '/api/reservations',
    scopes: [""],
  },
  authorizationApi: {
    endpoint: environment.urlAddress + '/api/authorization',
    scopes: [""],
  },
  dealersApi: {
    endpoint: environment.urlAddress + '/api/dealers',
    scopes: [""],
  },
  adminReservationsApi: {
    endpoint: environment.urlAddress + '/api/reservations/admin',
    scopes: [""],
  },
}

export const loginRequest = {
  scopes: []
};
