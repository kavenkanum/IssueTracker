import { Log, UserManager } from 'oidc-client';
import { useDispatch } from 'react-redux'
import slice from '../features/users/slice';

export const IDENTITY_CONFIG = {
  authority: process.env.REACT_APP_AUTH_URL,
  client_id: 'IssueTrackerSpa',
  redirect_uri: `${process.env.REACT_APP_URL}login/callback`,
  silent_redirect_uri: `${process.env.REACT_APP_URL}silent-renew`,
  post_logout_redirect_uri: `${process.env.REACT_APP_URL}logout/callback`,
  response_type: 'code',
  scope: 'openid profile IssueTrackerApi'
}

interface UserLoadedEvent {
  access_token: string;
  id_token: string;
  profile: {
    family_name: string;
    given_name: string;
  }
}


export class AuthService {
  dispatch = useDispatch();
  public userManager: UserManager;
  

  constructor() {
    this.userManager = new UserManager(IDENTITY_CONFIG);
    Log.logger = console;
    Log.level = Log.INFO;

    this.userManager.events.addUserLoaded((user: UserLoadedEvent) => {
      console.log("USER LOADED", user);
      this.dispatch(slice.actions.login(user));
        // this.dispatch(login(user.profile.given_name, user.profile.family_name, user.profile.role,
        //   user.access_token, user.id_token));
        localStorage.setItem('accessToken', user.access_token);
    });

    this.userManager.events.addUserUnloaded((user) => {
      console.log("USER UNLOADED", user);
      this.dispatch(slice.actions.logout());
      //this.dispatch(logout());
      localStorage.removeItem('accessToken');
    });

    this.userManager.events.addSilentRenewError((e: Error) => {
      console.log("SILENT RENEW ERROR", e);
      //TODO: Do something?!
    });

    this.userManager.events.addAccessTokenExpired(() => {
      console.log("ACCESS TOKEN EXPIRED");
      //TODO: Refresh token
    })
  }


  public createSignInRequest = async () => {
    await this.userManager.createSigninRequest();
  };

  public signInRedirect = async () => {
    await this.userManager.signinRedirect();
  };

  public signInRedirectCallback = async () => {
    await this.userManager.signinRedirectCallback()
      .catch((error) => {
        console.log("ERROR", error);
      });
  };

  public signInSilentCallback = async () => {
    await this.userManager.signinSilentCallback();
  };

  public logout = async () => {
    await this.userManager.signoutRedirect();
  };

  public signOutRedirectCallback = async () => {
    await this.userManager.signoutRedirectCallback();
  }
}
