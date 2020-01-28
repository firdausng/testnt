import { Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable, Subscription } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { OidcSecurityService, OidcConfigService, ConfigResult } from 'angular-auth-oidc-client';


/**
 * reference : 
 * https://github.com/damienbod/angular-auth-oidc-client/blob/master/projects/sample-code-flow/src/app/app.component.ts
 * 
 */

@Component({
  selector: 'tnt-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {

  isAuthorizedSubscription: Subscription;
  isAuthenticated: boolean;
  isConfigurationLoaded: boolean;
  userData: any;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, public oidcSecurityService: OidcSecurityService, private oidcConfigService: OidcConfigService) {
    if (this.oidcSecurityService.moduleSetup) {
      this.doCallbackLogicIfRequired();
    } else {
      this.oidcSecurityService.onModuleSetup.subscribe(() => {
        this.doCallbackLogicIfRequired();
      });
    }
  }

  ngOnInit() {
    this.oidcConfigService.onConfigurationLoaded.subscribe((value: ConfigResult) => {
      this.isConfigurationLoaded = true;
    });

    this.oidcSecurityService.getIsAuthorized().subscribe(auth => {
      this.isAuthenticated = auth;
    });

    this.oidcSecurityService.getUserData().subscribe(userData => {
      this.userData = userData;
      console.log(`userdata = ${JSON.stringify(this.userData)}`)
    });
  }

  ngOnDestroy(): void {
    this.isAuthorizedSubscription.unsubscribe();
  }

  login() {
    this.oidcSecurityService.authorize();
  }

  refreshSession() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }

  private doCallbackLogicIfRequired() {
    // Will do a callback, if the url has a code and state parameter.
    this.oidcSecurityService.authorizedCallbackWithCode(window.location.toString());
  }
}
