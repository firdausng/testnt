import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import {MatNativeDateModule} from '@angular/material/core';

import { FormsModule } from '@angular/forms';

import { AuthModule, ConfigResult, OidcConfigService, OidcSecurityService, OpenIdConfiguration } from 'angular-auth-oidc-client';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MaterialModule } from './material/material.module';
import { NavComponent } from './nav/nav.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AutoLoginComponent } from './auto-login/auto-login.component';
import { AuthorizationGuard } from './core/authorization.guard';
import { AuthService } from './core/auth.service';
import { TokenInterceptor } from './core/token.interceptor';
import { AppComponent } from './core/app.component';
import { AppRoutingModule } from './core/app-routing.module';


const oidc_configuration = 'assets/auth.clientConfiguration.json';
// if your config is on server side
// const oidc_configuration = ${window.location.origin}/api/ClientAppSettings

export function loadConfig(oidcConfigService: OidcConfigService) {
    return () => oidcConfigService.load(oidc_configuration);
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    DashboardComponent,
    
    UnauthorizedComponent,
    AutoLoginComponent,
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AuthModule.forRoot(),
    BrowserAnimationsModule,
    FormsModule,

    MaterialModule,
    MatNativeDateModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule
  ],
  entryComponents: [
    
  ],
  providers: [
    OidcConfigService,
        {
            provide: APP_INITIALIZER,
            useFactory: loadConfig,
            deps: [OidcConfigService],
            multi: true,
        },
        AuthorizationGuard,
        AuthService,
        {
          provide: HTTP_INTERCEPTORS,
          useClass: TokenInterceptor,
          multi: true
        }
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
              scope: 'testnt.main.api openid profile Tenant',
              response_type: 'code',
              post_logout_redirect_uri: 'http://localhost:7000/unauthorized',
              start_checksession: false,
              silent_renew: false,
              silent_renew_url: 'http://localhost:7000/silent-renew.html',
              post_login_route: '/dashboard',
              forbidden_route: '/forbidden',
              unauthorized_route: '/unauthorized',
              log_console_warning_active: true,
              log_console_debug_active: true,
              max_id_token_iat_offset_allowed_in_seconds: 30,
              history_cleanup_off: true
          };

          this.oidcSecurityService.setupModule(config, configResult.authWellknownEndpoints);
      });
  }
}
