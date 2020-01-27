import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AuthModule, ConfigResult, OidcConfigService, OidcSecurityService, OpenIdConfiguration } from 'angular-auth-oidc-client';



const oidc_configuration = 'assets/auth.clientConfiguration.json';
// if your config is on server side
// const oidc_configuration = ${window.location.origin}/api/ClientAppSettings

export function loadConfig(oidcConfigService: OidcConfigService) {
    return () => oidcConfigService.load(oidc_configuration);
}

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthModule.forRoot(),
  ],
  providers: [
    OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: loadConfig,
            deps: [OidcConfigService],
            multi: true,
        },
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private oidcSecurityService: OidcSecurityService, private oidcConfigService: OidcConfigService) {
      this.oidcConfigService.onConfigurationLoaded.subscribe((configResult: ConfigResult) => {
  
          // Use the configResult to set the configurations
    
          const config: OpenIdConfiguration = {
              stsServer: configResult.customConfig.stsServer,
              redirect_url: 'http://localhost:7000',
              client_id: 'testnt.main.spa.client',
              scope: 'testnt.main.api openid profile email',
              response_type: 'code',
              silent_renew: true,
              silent_renew_url: 'http://localhost:7000/silent-renew.html',
              log_console_debug_active: true,
              // all other properties you want to set
          };

          this.oidcSecurityService.setupModule(config, configResult.authWellknownEndpoints);
      });
  }
}
