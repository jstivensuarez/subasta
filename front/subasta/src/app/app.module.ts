import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTableModule } from 'angular-6-datatable';
import { HttpErrorInterceptor } from './services/autenticacion/errorInterceptor';
import { MaterialModule } from './material/material.module';
import { AppRoutingModule } from './app-routing.module';
import { ClientesModule } from './clientes/clientes.module';
import { ModalMessageComponent } from './modal-message/modal-message.component';
import { DetallesComponent } from './detalles/detalles.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtInterceptorService } from './services/autenticacion/jwt-interceptor.service';
import { JwtModule, JwtModuleOptions } from '@auth0/angular-jwt';

export function getToken(){
  return localStorage.getItem('token');
}

const JWT_Module_Options: JwtModuleOptions = {
  config: {
      tokenGetter: getToken
  }
};

@NgModule({
  declarations: [
    AppComponent, ModalMessageComponent, DetallesComponent, LoginComponent
  ],
  imports: [
  BrowserModule,
  DataTableModule, FormsModule, HttpClientModule, MaterialModule,
    RouterModule,
    NgbModule,
    AppRoutingModule,
    ClientesModule,
    MaterialModule,
    ReactiveFormsModule, 
    BrowserModule, 
    BrowserAnimationsModule,
    JwtModule.forRoot(JWT_Module_Options)
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
